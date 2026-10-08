using System.Diagnostics;
using System.ComponentModel;


class BenchmarkRunner
{
    private int ResolutionX = 1920;
    private int ResolutionY = 1080;

    public void SetResolution(int x, int y)
    {
        ResolutionX = x;
        ResolutionY = y;
    }

    private static List<Process> FindBenchmarkProcesses(string executablePath)
    {
        string fullExecutablePath = Path.GetFullPath(executablePath);
        string processName = Path.GetFileNameWithoutExtension(fullExecutablePath);
        List<Process> matches = new();

        foreach (Process candidate in Process.GetProcessesByName(processName))
        {
            try
            {
                string? candidatePath = candidate.MainModule?.FileName;
                if (candidatePath != null &&
                    string.Equals(
                        Path.GetFullPath(candidatePath),
                        fullExecutablePath,
                        StringComparison.OrdinalIgnoreCase))
                {
                    matches.Add(candidate);
                }
                else
                {
                    candidate.Dispose();
                }
            }
            catch (Win32Exception exception)
            {
                Console.Error.WriteLine(
                    $"Could not inspect process {candidate.Id} while locating the benchmark: {exception.Message}");
                candidate.Dispose();
            }
            catch (InvalidOperationException)
            {
                candidate.Dispose();
            }
        }

        return matches;
    }

    private static void StopProcess(Process process)
    {
        try
        {
            if (!process.HasExited)
            {
                process.Kill(entireProcessTree: true);
            }
        }
        catch (InvalidOperationException) when (process.HasExited)
        {
        }

        process.WaitForExit();
    }

    public void RunBenchmark(string binPath)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = binPath,
            UseShellExecute = false
        };

        float firstModX = .05f;
        float firstModY = .45f;

        float secondModX = .4f;
        float secondModY = .58f;

        int launchDelay = 45 * 1000; // 45 seconds
        int startBenchmarkDelay = 2 * 1000; // 2 seconds
        int confirmDelay = 1 * 1000; // 1 second
        int benchmarkDuration = 4 * 60 * 1000; // 4 minutes

        HashSet<int> existingProcessIds = new();
        foreach (Process existingProcess in FindBenchmarkProcesses(binPath))
        {
            using (existingProcess)
                existingProcessIds.Add(existingProcess.Id);
        }

        using Process process = Process.Start(startInfo)
            ?? throw new InvalidOperationException($"Failed to start benchmark executable '{binPath}'.");

        try
        {
            // Wait for the process to start
            Thread.Sleep(launchDelay);

            InputManager.Click();
            Thread.Sleep(startBenchmarkDelay);

            InputManager.MoveAndClick((int)(firstModX * ResolutionX), (int)(firstModY * ResolutionY));
            Thread.Sleep(confirmDelay);

            InputManager.MoveAndClick((int)(secondModX * ResolutionX), (int)(secondModY * ResolutionY));

            Thread.Sleep(benchmarkDuration);
        }
        finally
        {
            StopProcess(process);

            while (true)
            {
                List<Process> remainingProcesses = FindBenchmarkProcesses(binPath)
                    .Where(candidate => !existingProcessIds.Contains(candidate.Id))
                    .ToList();

                if (remainingProcesses.Count == 0)
                    break;

                foreach (Process candidate in remainingProcesses)
                {
                    using (candidate)
                        StopProcess(candidate);
                }
            }
        }
    }

    public void RunCPU(string binPath, string configPath)
    {
        ConfigEditor.SetCPUConfig(configPath);
        SetResolution(
            int.Parse(BenchmarkPresets.CPUConfig.ResolutionWidth),
            int.Parse(BenchmarkPresets.CPUConfig.ResolutionHeight)
        );
        RunBenchmark(binPath);
    }

    public void RunGPU(string binPath, string configPath)
    {
        ConfigEditor.SetGPUConfig(configPath);
        SetResolution(
            int.Parse(BenchmarkPresets.GPUConfig.ResolutionWidth),
            int.Parse(BenchmarkPresets.GPUConfig.ResolutionHeight)
        );
        RunBenchmark(binPath);
    }
}