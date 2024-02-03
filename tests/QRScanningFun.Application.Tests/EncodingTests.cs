using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRScanningFun.Application.Tests
{
    public class EncodingTests
    {
        [Theory]
        [InlineData("8675309", "110110001110000100101001")]
        [InlineData("000", "0000")]
        [InlineData("0000", "00000000")]
        public void NumericEncoding(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, QRCodeGenerator.GetDataBits(input));
        }

        [Theory]
        [InlineData("HELLO WORLD", "0110000101101111000110100010111001011011100010011010100001101")]
        public void AlphanumericEncoding(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, QRCodeGenerator.GetDataBits(input));
        }
    }
}
