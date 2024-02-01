using System.Text;

namespace QRScanningFun.Application
{
    public class QRCodeGenerator
    {
        private const int NumericEncodingMode = 1;
        private const int AlphaNumericEncodingMode = 2;
        private const int AlphaNumericMaxBitLength = 11; // 44*45 + 44 = 2024 => fits into 11 bits.
        private const int AlphaNumericSingleBitLength = 6; // 44 => fits into 6 bits.

        public int SelectEncodingByInput(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(s);
            }

            return s.All(char.IsNumber) ? NumericEncodingMode : AlphaNumericEncodingMode;
        }

        public string GetModeIndicatorByEncodingMode(int mode)
        {
            return mode switch
            {
                1 => "0001",
                2 => "0010",
                _ => throw new ArgumentOutOfRangeException(nameof(mode))
            };
        }

        public string GetCharacterCountIndicator(string input)
        {
            var indicatorLength = SelectEncodingByInput(input) == NumericEncodingMode ? 10 : 9;
            return Convert.ToString(input.Length, 2).PadLeft(indicatorLength, '0');
        }

        public string GetDataBits(string s)
        {
            return GetCodedText(s, GetAlphaNumericValues());
        }

        private string GetCodedText(string input, Dictionary<char, int> alphaNumericValues)
        {
            var result = "";
            while (input.Length >= 2)
            {
                var nextSection = input[..2];
                var base45 = (alphaNumericValues[nextSection[0]] * 45) + alphaNumericValues[nextSection[1]];
                result += Convert.ToString(base45, 2).PadLeft(AlphaNumericMaxBitLength, '0');
                input = input[2..];
            }

            if (input.Length > 0)
            {
                result += Convert.ToString(alphaNumericValues[input[0]], 2)
                    .PadLeft(AlphaNumericSingleBitLength, '0');
            }

            return result;
        }

        private static Dictionary<char, int> GetAlphaNumericValues()
        {
            return new Dictionary<char, int>()
            {
                {'0', 0},
                {'1', 1},
                {'2', 2},
                {'3', 3},
                {'4', 4},
                {'5', 5},
                {'6', 6},
                {'7', 7},
                {'8', 8},
                {'9', 9},
                {'A', 10},
                {'B', 11},
                {'C', 12},
                {'D', 13},
                {'E', 14},
                {'F', 15},
                {'G', 16},
                {'H', 17},
                {'I', 18},
                {'J', 19},
                {'K', 20},
                {'L', 21},
                {'M', 22},
                {'N', 23},
                {'O', 24},
                {'P', 25},
                {'Q', 26},
                {'R', 27},
                {'S', 28},
                {'T', 29},
                {'U', 30},
                {'V', 31},
                {'W', 32},
                {'X', 33},
                {'Y', 34},
                {'Z', 35},
                {' ', 36},
                {'$', 37},
                {'%', 38},
                {'*', 39},
                {'+', 40},
                {'-', 41},
                {'.', 42},
                {'/', 43},
                {':', 44}
            };
        }
    }
}
