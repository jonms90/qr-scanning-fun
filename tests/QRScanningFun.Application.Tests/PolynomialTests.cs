namespace QRScanningFun.Application.Tests
{
    public class PolynomialTests
    {
        [Theory]
        [InlineData(15, 10, 25)]
        [InlineData(14, 10, 24)]
        [InlineData(13, 10, 23)]
        [InlineData(12, 10, 22)]
        [InlineData(11, 10, 21)]
        [InlineData(10, 10, 20)]
        public void MultiplyByExponent(int exponent, int multiplier, int expectedResult)
        {
            var startingCoefficient = 1;
            var polynomial = new Polynomial(startingCoefficient, exponent);
            var result = polynomial.MultiplyWithExponent(multiplier);
            Assert.Equal(expectedResult, result.Exponent);
            Assert.Equal(startingCoefficient, result.Coefficient);
        }

        [Theory]
        [InlineData(32, 5)]
        [InlineData(1, 255)]
        [InlineData(2, 1)]
        [InlineData(4, 2)]
        [InlineData(8, 3)]
        [InlineData(16, 4)]
        public void GetAlphaNotation(int coefficient, int expectedAlphaValue)
        {
            var result = new Polynomial(coefficient, 1).ConvertToAlphaNotation();
            Assert.Equal(expectedAlphaValue, result.Alpha);
        }

        [Theory]
        [InlineData(0, 5, 5)]
        [InlineData(251, 5, 1)]
        [InlineData(67, 5,72)]
        [InlineData(46, 5, 51)]
        [InlineData(61, 5, 66)]
        [InlineData(118, 5, 123)]
        public void MultiplyWithAlpha(int alpha, int multiplier, int expectedAlphaValue)
        {
            var result =
                new Polynomial(1, 1, alpha).MultiplyWithAlpha(new Polynomial(1, 1, multiplier));
            Assert.Equal(expectedAlphaValue, result.Alpha);
        }

        [Theory]
        [InlineData(32, 32, 0)]
        [InlineData(91, 2, 89)]
        [InlineData(11, 101, 110)]
        [InlineData(120, 10, 114)]
        [InlineData(209, 97, 176)]
        [InlineData(114, 197, 183)]
        public void XOR(int coefficient, int coefficient2, int expectedResult)
        {
            var result =
                new Polynomial(coefficient, 1, 0).XOR(new Polynomial(coefficient2, 1, 0));
            Assert.Equal(expectedResult, result.Coefficient);
        }
    }
}
