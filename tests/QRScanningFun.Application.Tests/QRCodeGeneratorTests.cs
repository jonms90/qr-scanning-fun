namespace QRScanningFun.Application.Tests
{
    public class SelectEncodingByInputTests
    {
        [Theory]
        [InlineData("0",EncodingMode.Numeric)]
        [InlineData("1",EncodingMode.Numeric)]
        [InlineData("999",EncodingMode.Numeric)]
        [InlineData("421532312",EncodingMode.Numeric)]
        [InlineData("HTTPS://EXAMPLE.COM",EncodingMode.Alphanumeric)]
        [InlineData("https://example.com", EncodingMode.Byte)]
        [InlineData("-1", EncodingMode.Alphanumeric)]

        public void SelectEncodingByInput(string s, EncodingMode expectedResult)
        {
            Assert.Equal(expectedResult, QRCodeGenerator.SelectEncodingByInput(s));
        }

        [Theory]
        [InlineData(EncodingMode.Numeric, "0001")]
        [InlineData(EncodingMode.Alphanumeric, "0010")]
        [InlineData(EncodingMode.Byte, "0100")]
        public void GetModeIndicatorByEncodingMode(EncodingMode mode, string expectedResult)
        {
            Assert.Equal(expectedResult, QRCodeGenerator.GetModeIndicatorByEncodingMode(mode));
        }

        [Fact]
        public void GetCharacterCountIndicator()
        {
            Assert.Equal("000001110", QRCodeGenerator.GetCharacterCountIndicator("HELLO CC WORLD"));
        }

        [Fact]
        public void GetDataBits()
        {
            Assert.Equal("01100001011011110001101000101110001000101000110011101001000101001101110111110", QRCodeGenerator.GetDataBits("HELLO CC WORLD"));
        }
    }
}