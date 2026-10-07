class LogParcer
{
    public static string? ReadLastLogFile(string logDir)
    {
        string? latestFile = Directory
        .GetFiles(logDir)
        .OrderByDescending(f => long.Parse(Path.GetFileName(f)))
        .FirstOrDefault();

    if (latestFile != null)
    {
        string content = File.ReadAllText(latestFile);
        return content;
    }
    return null;
    }

    public static BenchmarkResult? ParseLog(string json)
    {
        try
        {
            BenchmarkResult? result = System.Text.Json.JsonSerializer.Deserialize<BenchmarkResult>(json);
            return result;
        }
        catch (System.Text.Json.JsonException ex)
        {
            Console.WriteLine($"Error parsing JSON: {ex.Message}");
            return null;
        }
    }

    public static BenchmarkResult? GetBenchmarkResult(string logDir)
    {
        string? lastLogContent = ReadLastLogFile(logDir);
        if (lastLogContent != null)
        {
            return ParseLog(lastLogContent);
        }
        return null;
    }
}