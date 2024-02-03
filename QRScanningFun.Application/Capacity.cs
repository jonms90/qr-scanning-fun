namespace QRScanningFun.Application;

public class Capacity(CodeVersion version, ErrorCorrectionLevel correctionLevel, EncodingMode encodingMode, int maxCapacity)
{
    public const int UpperLimitNumeric = 7089;
    public const int UpperLimitAlphanumeric = 4296;
    public const int UpperLimitByte = 2953;

    public EncodingMode EncodingMode { get; } = encodingMode;
    public ErrorCorrectionLevel CorrectionLevel { get; } = correctionLevel;
    public CodeVersion Version { get; } = version;
    public int MaxCapacity { get; } = maxCapacity;
}