namespace QRScanningFun.Application.Tests
{
    public class SelectEncodingByInputTests
    {
        [Theory]
        [InlineData("1",1)]
        [InlineData("https://example.com",2)]
        public void SelectEncodingByInput(string s, int expectedResult)
        {
            Assert.Equal(expectedResult, QRCodeGenerator.SelectEncodingByInput(s));
        }

        [Theory]
        [InlineData(1, "0001")]
        [InlineData(2, "0010")]
        public void GetModeIndicatorByEncodingMode(int mode, string expectedResult)
        {
            Assert.Equal(expectedResult, QRCodeGenerator.GetModeIndicatorByEncodingMode(mode));
        }

        [Fact]
        public void GetCharacterCountIndicator()
        {
            Assert.Equal("000001110", new QRCodeGenerator().GetCharacterCountIndicator("HELLO CC WORLD"));
        }

        [Fact]
        public void GetDataBits()
        {
            Assert.Equal("01100001011011110001101000101110001000101000110011101001000101001101110111110", new QRCodeGenerator().GetDataBits("HELLO CC WORLD"));
        }
    }
}