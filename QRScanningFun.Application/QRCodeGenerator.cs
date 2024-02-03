using System.Text;

namespace QRScanningFun.Application
{
    public static class QRCodeGenerator
    {
        private const int AlphaNumericMaxBitLength = 11; // 44*45 + 44 = 2024 => fits into 11 bits.
        private const int AlphaNumericSingleBitLength = 6; // 44 => fits into 6 bits.

        private static readonly Dictionary<char, int> AlphaNumericValues = GetAlphaNumericValues();

        private static readonly List<Capacity> Capacities = GetCapacities();

        public static EncodingMode SelectEncodingByInput(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(s);
            }

            if (s.All(char.IsNumber))
            {
                return EncodingMode.Numeric;
            }

            return s.All(AlphaNumericValues.ContainsKey) ? EncodingMode.Alphanumeric : EncodingMode.Byte;
        }

        public static string GetModeIndicatorByEncodingMode(EncodingMode mode)
        {
            return mode switch
            {
                EncodingMode.Numeric => "0001",
                EncodingMode.Alphanumeric => "0010",
                EncodingMode.Byte => "0100",
                EncodingMode.Kanji => throw new NotImplementedException("Kanji is not implemented as an encoding mode."),
                _ => throw new ArgumentOutOfRangeException(nameof(mode))
            };
        }

        public static string GetCharacterCountIndicator(string input)
        {
            var indicatorLength = SelectEncodingByInput(input) == EncodingMode.Numeric ? 10 : 9;
            return Convert.ToString(input.Length, 2).PadLeft(indicatorLength, '0');
        }

        public static string GetDataBits(string s)
        {
            var mode = SelectEncodingByInput(s);
            if (mode == EncodingMode.Numeric)
            {
                return GetNumericEncodedText(s);
            }

            return GetCodedText(s, GetAlphaNumericValues());
        }

        private static string GetNumericEncodedText(string input)
        {
            var sb = new StringBuilder();
            while (input.Length >= 3)
            {
                var nextSection = input[..3];
                var number = int.Parse(nextSection);
                sb.Append(Convert.ToString(number, 2).PadLeft(4, '0'));
                input = input[3..];
            }

            if (input.Length > 0)
            {
                sb.Append(Convert.ToString(int.Parse(input), 2).PadLeft(4, '0'));
            }

            return sb.ToString();
        }

        private static string GetCodedText(string input, Dictionary<char, int> alphaNumericValues)
        {
            var sb = new StringBuilder();
            while (input.Length >= 2)
            {
                var nextSection = input[..2];
                var base45 = (alphaNumericValues[nextSection[0]] * 45) + alphaNumericValues[nextSection[1]];
                sb.Append(Convert.ToString(base45, 2).PadLeft(AlphaNumericMaxBitLength, '0'));
                input = input[2..];
            }

            if (input.Length > 0)
            {
                sb.Append(Convert.ToString(alphaNumericValues[input[0]], 2)
                    .PadLeft(AlphaNumericSingleBitLength, '0'));
            }

            return sb.ToString();
        }

        public static CodeVersion DetermineSmallestVersion(string input, ErrorCorrectionLevel correctionLevel)
        {
            var encodingMode = SelectEncodingByInput(input);
            var capacity = Capacities
                .Where(c => c.EncodingMode == encodingMode && c.CorrectionLevel == correctionLevel)
                .OrderBy(c => c.MaxCapacity)
                .FirstOrDefault(c => c.MaxCapacity > input.Length);

            return capacity?.Version ?? throw new ArgumentOutOfRangeException(nameof(input),
                "Not able to find a large enough version based on input.");
        }

        public static int GetCharacterCountIndicatorLength(CodeVersion version, EncodingMode mode)
        {
            return version switch
            {
                <= CodeVersion.V9 => GetSmallCharacterCountLength(mode),
                <= CodeVersion.V26 => GetMediumCharacterCountLength(mode),
                _ => GetLargeCharacterCountLength(mode)
            };
        }

        private static int GetLargeCharacterCountLength(EncodingMode mode)
        {
            return mode switch
            {
                EncodingMode.Numeric => 14,
                EncodingMode.Alphanumeric => 13,
                EncodingMode.Byte => 16,
                EncodingMode.Kanji => throw new ArgumentException("Kanji is not supported.", nameof(mode)),
                _ => throw new ArgumentException("EncodingMode is not supported.", nameof(mode))
            };
        }

        private static int GetMediumCharacterCountLength(EncodingMode mode)
        {
            return mode switch
            {
                EncodingMode.Numeric => 12,
                EncodingMode.Alphanumeric => 11,
                EncodingMode.Byte => 16,
                EncodingMode.Kanji => throw new ArgumentException("Kanji is not supported.", nameof(mode)),
                _ => throw new ArgumentException("EncodingMode is not supported.", nameof(mode))
            };
        }

        private static int GetSmallCharacterCountLength(EncodingMode mode)
        {
            return mode switch
            {
                EncodingMode.Numeric => 10,
                EncodingMode.Alphanumeric => 9,
                EncodingMode.Byte => 8,
                EncodingMode.Kanji => throw new ArgumentException("Kanji is not supported.", nameof(mode)),
                _ => throw new ArgumentException("EncodingMode is not supported.", nameof(mode))
            };
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
        private static List<Capacity> GetCapacities()
        {
            return
            [
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.L, EncodingMode.Numeric, 41),
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.L, EncodingMode.Alphanumeric, 25),
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.L, EncodingMode.Byte, 17),
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.M, EncodingMode.Numeric, 34),
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.M, EncodingMode.Alphanumeric, 20),
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.M, EncodingMode.Byte, 14),
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.Q, EncodingMode.Numeric, 27),
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.Q, EncodingMode.Alphanumeric, 16),
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.Q, EncodingMode.Byte, 11),
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.H, EncodingMode.Numeric, 17),
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.H, EncodingMode.Alphanumeric, 10),
                new Capacity(CodeVersion.V1, ErrorCorrectionLevel.H, EncodingMode.Byte, 7),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.L, EncodingMode.Numeric, 77),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.L, EncodingMode.Alphanumeric, 47),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.L, EncodingMode.Byte, 32),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.M, EncodingMode.Numeric, 63),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.M, EncodingMode.Alphanumeric, 38),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.M, EncodingMode.Byte, 26),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.Q, EncodingMode.Numeric, 48),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.Q, EncodingMode.Alphanumeric, 29),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.Q, EncodingMode.Byte, 20),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.H, EncodingMode.Numeric, 34),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.H, EncodingMode.Alphanumeric, 20),
                new Capacity(CodeVersion.V2, ErrorCorrectionLevel.H, EncodingMode.Byte, 14),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.L, EncodingMode.Numeric, 127),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.L, EncodingMode.Alphanumeric, 77),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.L, EncodingMode.Byte, 53),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.M, EncodingMode.Numeric, 101),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.M, EncodingMode.Alphanumeric, 61),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.M, EncodingMode.Byte, 42),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.Q, EncodingMode.Numeric, 77),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.Q, EncodingMode.Alphanumeric, 47),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.Q, EncodingMode.Byte, 32),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.H, EncodingMode.Numeric, 58),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.H, EncodingMode.Alphanumeric, 35),
                new Capacity(CodeVersion.V3, ErrorCorrectionLevel.H, EncodingMode.Byte, 24),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.L, EncodingMode.Numeric, 187),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.L, EncodingMode.Alphanumeric, 114),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.L, EncodingMode.Byte, 78),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.M, EncodingMode.Numeric, 149),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.M, EncodingMode.Alphanumeric, 90),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.M, EncodingMode.Byte, 62),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.Q, EncodingMode.Numeric, 111),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.Q, EncodingMode.Alphanumeric, 67),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.Q, EncodingMode.Byte, 46),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.H, EncodingMode.Numeric, 82),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.H, EncodingMode.Alphanumeric, 50),
                new Capacity(CodeVersion.V4, ErrorCorrectionLevel.H, EncodingMode.Byte, 34)
            ];
        }
    }
}
