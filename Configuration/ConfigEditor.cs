using System.Text;
using System.Text.RegularExpressions;

class ConfigEditor
{
    public static void SetValue(string filePath, string section, string key, string value)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File '{filePath}' not found.");

        var text = File.ReadAllText(filePath, Encoding.UTF8);

        var sectionPattern = $@"(?ms)^\[{Regex.Escape(section)}\]\r?\n(.*?)(?=^\[|\z)";
        var sectionMatch = Regex.Match(text, sectionPattern);

        if (!sectionMatch.Success)
            throw new InvalidOperationException($"Section [{section}] not found.");

        var sectionText = sectionMatch.Groups[1].Value;

        var keyPattern = $@"(?m)^(?<prefix>{Regex.Escape(key)}=).*$";

        if (!Regex.IsMatch(sectionText, keyPattern))
            throw new InvalidOperationException(
                $"Key '{key}' not found in section [{section}].");

        sectionText = Regex.Replace(
            sectionText,
            keyPattern,
            $"${{prefix}}{value}"
        );

        text = text.Remove(
            sectionMatch.Groups[1].Index,
            sectionMatch.Groups[1].Length
        ).Insert(
            sectionMatch.Groups[1].Index,
            sectionText
        );

        File.WriteAllText(filePath, text, Encoding.UTF8);
    }

    public static void SwapConfig(string configPath, BenchmarkConfig config)
    {
        SetValue(configPath, "/Script/GSGameSettings.GSGameUserSettings", "ResolutionSizeX", config.ResolutionWidth);
        SetValue(configPath, "/Script/GSGameSettings.GSGameUserSettings", "ResolutionSizeY", config.ResolutionHeight);
        SetValue(configPath, "/Script/GSGameSettings.GSGameUserSettings", "FullscreenMode", config.Fullscreen);
        SetValue(configPath, "/Script/GSGameSettings.GSGameUserSettings", "bUseVSync", config.VSync);
        SetValue(configPath, "ScalabilityGroups", "sg.ResolutionQuality", config.ResolutionQuality);
        SetValue(configPath, "ScalabilityGroups", "sg.ViewDistanceQuality", config.ViewDistanceQuality);
        SetValue(configPath, "ScalabilityGroups", "sg.AntiAliasingQuality", config.AntiAliasingQuality);
        SetValue(configPath, "ScalabilityGroups", "sg.ShadowQuality", config.ShadowQuality);
        SetValue(configPath, "ScalabilityGroups", "sg.GlobalIlluminationQuality", config.GlobalIlluminationQuality);
        SetValue(configPath, "ScalabilityGroups", "sg.RayTracingQuality", config.RayTracingQuality);
        SetValue(configPath, "ScalabilityGroups", "sg.ReflectionQuality", config.ReflectionQuality);
        SetValue(configPath, "ScalabilityGroups", "sg.PostProcessQuality", config.PostProcessQuality);
        SetValue(configPath, "ScalabilityGroups", "sg.TextureQuality", config.TextureQuality);
        SetValue(configPath, "ScalabilityGroups", "sg.EffectsQuality", config.EffectsQuality);
        SetValue(configPath, "ScalabilityGroups", "sg.FoliageQuality", config.FoliageQuality);
        SetValue(configPath, "ScalabilityGroups", "sg.ShadingQuality", config.ShadingQuality);
        SetValue(configPath, "RayTracing", "r.RayTracing.EnableInGame", config.RayTracingMode);
    }

    public static void SetCPUConfig(string configPath)
    {
        SwapConfig(configPath, BenchmarkPresets.CPUConfig);
    }

    public static void SetGPUConfig(string configPath)
    {
        SwapConfig(configPath, BenchmarkPresets.GPUConfig);
    }
}