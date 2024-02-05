using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRScanningFun.Application.Tests
{
    public class ModulePlacementTests
    {
        [Fact]
        public void FinderPatternTopLeft()
        {
            var rawData = QRCodeGenerator.GetRawDataBits("HELLO WORLD", ErrorCorrectionLevel.M);
            var errorCodes =
                QRCodeGenerator.GetErrorCorrectionCodes(rawData, CodeVersion.V1,
                    ErrorCorrectionLevel.M);
            var data = QRCodeGenerator.GetFinalMessage(rawData, errorCodes);
            var result = QRCodeGenerator.CreateModulePlacement(data, CodeVersion.V1);
            Assert.NotNull(result);
            var expected = new Bitmap("images/finderpatterns.bmp");
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Assert.Equal(expected.GetPixel(i, j), result.GetPixel(i, j));
                }
            }
        }

        [Fact]
        public void FinderPatternTopRight()
        {
            var rawData = QRCodeGenerator.GetRawDataBits("HELLO WORLD", ErrorCorrectionLevel.M);
            var errorCodes =
                QRCodeGenerator.GetErrorCorrectionCodes(rawData, CodeVersion.V1,
                    ErrorCorrectionLevel.M);
            var data = QRCodeGenerator.GetFinalMessage(rawData, errorCodes);
            var result = QRCodeGenerator.CreateModulePlacement(data, CodeVersion.V1);
            Assert.NotNull(result);
            var expected = new Bitmap("images/finderpatterns.bmp");
            for (int i = 20; i > 21-7; i--)
            {
                for (int j = 0; j < 7; j++)
                {
                    Assert.Equal(expected.GetPixel(i, j), result.GetPixel(i, j));
                }
            }
        }

        [Fact]
        public void FinderPatternBottomLeft()
        {
            var rawData = QRCodeGenerator.GetRawDataBits("HELLO WORLD", ErrorCorrectionLevel.M);
            var errorCodes =
                QRCodeGenerator.GetErrorCorrectionCodes(rawData, CodeVersion.V1,
                    ErrorCorrectionLevel.M);
            var data = QRCodeGenerator.GetFinalMessage(rawData, errorCodes);
            var result = QRCodeGenerator.CreateModulePlacement(data, CodeVersion.V1);
            Assert.NotNull(result);
            var expected = new Bitmap("images/finderpatterns.bmp");
            for (int i = 0; i < 7; i++)
            {
                for (int j = 21- 7; j < 21; j++)
                {
                    Assert.Equal(expected.GetPixel(i, j), result.GetPixel(i, j));
                }
            }
        }

        [Fact]
        public void TimingPatternsHorizontal()
        {
            var rawData = QRCodeGenerator.GetRawDataBits("HELLO WORLD", ErrorCorrectionLevel.M);
            var errorCodes =
                QRCodeGenerator.GetErrorCorrectionCodes(rawData, CodeVersion.V1,
                    ErrorCorrectionLevel.M);
            var data = QRCodeGenerator.GetFinalMessage(rawData, errorCodes);
            var result = QRCodeGenerator.CreateModulePlacement(data, CodeVersion.V1);
            Assert.NotNull(result);
            var expected = new Bitmap("images/timingpatterns.bmp");
            for (int i = 8; i <= 12; i++)
            {
                Assert.Equal(expected.GetPixel(i, 6), result.GetPixel(i, 6));
            }
        }
    }
}
