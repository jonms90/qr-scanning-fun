namespace QRScanningFun.Application.Tests
{
    public class ErrorCorrectionCodesTests
    {
        [Fact]
        public void GetErrorCorrectionCodes()
        {
            var result = QRCodeGenerator.GetErrorCorrectionCodes(
                QRCodeGenerator.GetRawDataBits("HELLO WORLD", ErrorCorrectionLevel.M),
                CodeVersion.V1, ErrorCorrectionLevel.M);
            
            List<int> expected = [196, 35, 39, 119, 235, 215, 231, 226, 93, 23];
            Assert.Equal(expected, result);


        }
    }
}
