class Program
{
    static void Main(string[] args)
    {
        string defaultExePath = @"D:\Steam\steamapps\common\Black Myth Wukong Benchmark Tool\b1\Binaries\Win64\b1-Win64-Shipping.exe";
        string defaultConfigPath = @"D:\Steam\steamapps\common\Black Myth Wukong Benchmark Tool\b1\Saved\Config\Windows\GameUserSettings.ini";
        string logDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp\b1\BenchMarkHistory\Tool\";

        // string exePath = args.Length > 0 ? args[0] : defaultExePath;
        // string configPath = args.Length > 1 ? args[1] : defaultConfigPath;

        // TODO : custom config path and exe path

        string exePath = defaultExePath;
        string configPath = defaultConfigPath;

        ConfigEditor.SetCPUConfig(configPath);

        BenchmarkRunner benchmarkRunner = new BenchmarkRunner();
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