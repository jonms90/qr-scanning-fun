using QRScanningFun.Application;

namespace QRScanningFun.Console
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var dataRaw = QRCodeGenerator.GetRawDataBits("HELLO WORLD", ErrorCorrectionLevel.M);
            var errorCodes =
                QRCodeGenerator.GetErrorCorrectionCodes(dataRaw, CodeVersion.V1,
                    ErrorCorrectionLevel.M);
            var dataString = QRCodeGenerator.GetFinalMessage(dataRaw, errorCodes);
            var qr = QRCodeGenerator.CreateModulePlacement(dataString, CodeVersion.V1);
            var tempLocation = Directory.CreateTempSubdirectory();
            qr.Save(Path.Join(tempLocation.ToString(), "qr.png"));
            System.Console.WriteLine($"Saved QR at {tempLocation.ToString()}/qr.png");
        }
    }
}
