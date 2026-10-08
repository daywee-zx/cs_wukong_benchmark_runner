public static class BenchmarkPresets
{
    internal static BenchmarkConfig CPUConfig = new()
        {
            ResolutionWidth = "1920",
            ResolutionHeight = "1080",
            Fullscreen = "1",
            VSync = "0",
            ResolutionQuality = "100",
            ViewDistanceQuality = "3",
            AntiAliasingQuality = "3",
            ShadowQuality = "0",
            GlobalIlluminationQuality = "0",
            RayTracingQuality = "0",
            ReflectionQuality = "0",
            PostProcessQuality = "0",
            TextureQuality = "0",
            EffectsQuality = "3",
            FoliageQuality = "3",
            ShadingQuality = "3",
            RayTracingMode = "False"
        };

    internal static BenchmarkConfig GPUConfig = new()
        {
            ResolutionWidth = "1920",
            ResolutionHeight = "1080",
            Fullscreen = "1",
            VSync = "0",
            ResolutionQuality = "100",
            ViewDistanceQuality = "3",
            AntiAliasingQuality = "3",
            ShadowQuality = "3",
            GlobalIlluminationQuality = "3",
            RayTracingQuality = "3",
            ReflectionQuality = "3",
            PostProcessQuality = "3",
            TextureQuality = "3",
            EffectsQuality = "3",
            FoliageQuality = "3",
            ShadingQuality = "3",
            RayTracingMode = "True"
        };
}