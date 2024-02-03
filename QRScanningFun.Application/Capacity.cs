namespace QRScanningFun.Application;

public class Capacity(CodeVersion version, ErrorCorrectionLevel correctionLevel, EncodingMode encodingMode, int maxCapacity)
{
    public EncodingMode EncodingMode { get; } = encodingMode;
    public ErrorCorrectionLevel CorrectionLevel { get; } = correctionLevel;
    public CodeVersion Version { get; } = version;
    public int MaxCapacity { get; } = maxCapacity;
}