class BenchmarkResult
{
    public float FPSAvg { get; set; }
    public float FPSMin { get; set; }
    public float FPSMax { get; set; }
    public float VideoMem { get; set; }


    public int QualityLevel { get; set; }
    public int ImageQuality { get; set; }
    public int ViewDistance { get; set; }
    public int AntiAliasing { get; set; }
    public int PostProcessing { get; set; }
    public int ShadowQuality { get; set; }
    public int TextureQuality { get; set; }
    public int MaterialQuality { get; set; }
    public int VegetationQuality { get; set; }
    public int MotionBlur { get; set; }
    public int Rtx { get; set; }
    public int Dlss { get; set; }
    public int InsertFrame { get; set; }
    public int Dx12 { get; set; }

    public string? CPUModel { get; set; }
    public string? GPUModel { get; set; }
    public string? SysMem { get; set; }
    public string? ScreenResolution { get; set; }
    public string? VideoMemSize { get; set; }

    public override string ToString()
    {
        return $"PC Info:\nCPU: {CPUModel}\nGPU: {GPUModel}\nRAM: {SysMem}\nVideo Memory Size: {VideoMemSize}\nScreen resolution: {ScreenResolution}\n\n" +
               $"Benchmark Results:\nAvgFPS: {FPSAvg}\nMinFPS: {FPSMin}\nMaxFPS: {FPSMax}\nMaximum Video Memory: {VideoMem}\n" +
               $"Settings:\nQualityLevel: {QualityLevel}\nImageQuality: {ImageQuality}\nViewDistance: {ViewDistance}\nAntiAliasing: {AntiAliasing}\nPostProcessing: {PostProcessing}\nShadowQuality: {ShadowQuality}\nTextureQuality: {TextureQuality}\nMaterialQuality: {MaterialQuality}\nVegetationQuality: {VegetationQuality}\nMotionBlur: {MotionBlur}\nRtx: {Rtx}\nDlss: {Dlss}\nInsertFrame: {InsertFrame}\nDx12: {Dx12}";
    }
}