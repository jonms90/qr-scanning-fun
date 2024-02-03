namespace QRScanningFun.Application.Tests
{
    public class DetermineSmallestVersionTests
    {
        [Theory]
        [InlineData("HELLO WORLD", ErrorCorrectionLevel.L, CodeVersion.V1)]
        [InlineData("HELLO WORLD", ErrorCorrectionLevel.M, CodeVersion.V1)]
        [InlineData("HELLO WORLD", ErrorCorrectionLevel.Q, CodeVersion.V1)]
        [InlineData("HELLO WORLD", ErrorCorrectionLevel.H, CodeVersion.V2)]
        [InlineData("HELLO WORLD. I AM PLAYING GNORP WHILE CODING THIS", ErrorCorrectionLevel.L, CodeVersion.V3)]
        [InlineData("HELLO WORLD. I AM PLAYING GNORP WHILE CODING THIS", ErrorCorrectionLevel.M, CodeVersion.V3)]
        [InlineData("HELLO WORLD. I AM PLAYING GNORP WHILE CODING THIS", ErrorCorrectionLevel.Q, CodeVersion.V4)]
        [InlineData("HELLO WORLD. I AM PLAYING GNORP WHILE CODING THIS", ErrorCorrectionLevel.H, CodeVersion.V4)]
        public void SmallestVersionIsSelectedGivenInputAndErrorCorrectionLevel(string input, ErrorCorrectionLevel correctionLevel, CodeVersion expectedVersion)
        {
            Assert.Equal(expectedVersion, QRCodeGenerator.DetermineSmallestVersion(input, correctionLevel));
        }

        [Fact]
        public void UnsupportedInputLengthThrowsArgumentOutOfRangeException()
        {
            // Only implemented support for up to Version 4.
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                QRCodeGenerator.DetermineSmallestVersion(
                    "This is too long to fit inside a quick response code of version 4",
                    ErrorCorrectionLevel.H));
        }

        [Theory]
        [InlineData('1', Capacity.UpperLimitNumeric)]
        [InlineData('A', Capacity.UpperLimitAlphanumeric)]
        [InlineData('a', Capacity.UpperLimitByte)]
        public void InputBeyondUpperLimitsThrowsArgumentOutOfRangeException(char input, int upperLimit)
        {
            // Only implemented support for up to Version 4.
            var data = string.Join("", Enumerable.Repeat(input, upperLimit + 1));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                QRCodeGenerator.DetermineSmallestVersion(data, ErrorCorrectionLevel.L));
        }
    }
}
