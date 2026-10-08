class Program
{
    static void Main(string[] args)
    {
        string defaultExePath = @"D:\Steam\steamapps\common\Black Myth Wukong Benchmark Tool\b1\Binaries\Win64\b1-Win64-Shipping.exe";
        string defaultConfigPath = @"D:\Steam\steamapps\common\Black Myth Wukong Benchmark Tool\b1\Saved\Config\Windows\GameUserSettings.ini";
        string logDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp\b1\BenchMarkHistory\Tool\";

        if (args.Length > 2)
        throw new ArgumentException("Usage: WukongBenchmark [exe-path] [config-path]");

        string exePath = Path.GetFullPath(args.Length > 0 ? args[0] : defaultExePath);
        string configPath = Path.GetFullPath(args.Length > 1 ? args[1] : defaultConfigPath);

        if (!File.Exists(exePath))
            throw new FileNotFoundException("Benchmark executable not found.", exePath);
        if (!File.Exists(configPath))
            throw new FileNotFoundException("Game settings file not found.", configPath);

        ConfigEditor.SetCPUConfig(configPath);

        BenchmarkRunner benchmarkRunner = new BenchmarkRunner();

        Console.WriteLine("Running CPU Benchmark...");
        benchmarkRunner.RunCPU(exePath, configPath);

        BenchmarkResult? benchmarkResult = LogParcer.GetBenchmarkResult(logDir);

        Console.WriteLine("CPU Benchmark Result:");
        if (benchmarkResult != null)
        {
            Console.WriteLine(benchmarkResult.ToString());
        }
        else
        {
            Console.WriteLine("No benchmark result found.");
        }

        Console.WriteLine("\n\nRunning GPU Benchmark...");
        benchmarkRunner.RunGPU(exePath, configPath);

        benchmarkResult = LogParcer.GetBenchmarkResult(logDir);

        Console.WriteLine("\n\nGPU Benchmark Result:");
        if (benchmarkResult != null)
        {
            Console.WriteLine(benchmarkResult.ToString());
        }
        else
        {
            Console.WriteLine("No benchmark result found.");
        }
    }
}