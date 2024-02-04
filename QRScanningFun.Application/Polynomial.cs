﻿namespace QRScanningFun.Application;

public class Polynomial
{
    private static Dictionary<int, int> AntilogValues = GetAntilogValues();

    public Polynomial(int coefficient, int exponent, int alpha = 0)
    {
        Coefficient = coefficient;
        Exponent = exponent;
        Alpha = alpha;
    }

    public int Coefficient { get; }
    public int Alpha { get; }
    public int Exponent { get; }

    public Polynomial MultiplyWithExponent(int multiplier)
    {
        return new Polynomial(Coefficient, Exponent + multiplier, Alpha);
    }

    public Polynomial ConvertToAlphaNotation()
    {
        if (!AntilogValues.TryGetValue(Coefficient, out int alphaValue))
        {
            throw new NotImplementedException($"Missing Log entry for key {Coefficient}");
        }

        return new Polynomial(0, 0, alphaValue);
    }

    private static Dictionary<int, int> GetAntilogValues()
    {
        return new Dictionary<int, int>()
        {
            {1, 255},
            {2, 1},
            {3, 25},
            {4, 2},
            {5, 50},
            {6, 26},
            {7, 198},
            {8, 3},
            {9, 223},
            {10, 51},
            {11, 238},
            {12, 27},
            {13, 104},
            {14, 199},
            {15, 75},
            {16, 4},
            {17, 100},
            {18,224},
            {19,14},
            {20,52},
            {21, 141},
            {22, 239},
            {23, 129},
            {24, 28},
            {25, 193},
            {26, 105},
            {27, 248},
            {28, 200},
            {29, 8},
            {30, 76},
            {31, 113},
            {32, 5},
            {33, 138},
            {34, 101},
            {35, 47},
            {36, 225},
            {37, 36},
            {38, 15},
            {39, 33},
            {40, 53},
            {41, 147},
            {42, 142},
            {43, 218},
            {44, 240},
            {45, 18},
            {46, 130},
            {47, 69},
            {48, 29},
            {49, 181},
            {50, 194},
            {51, 125},
            {52, 106},
            {53, 39},
            {54, 249},
            {55, 185},
            {56, 201},
            {57, 154},
            {58, 9},
            {59, 120},
            {60, 77},
            {61, 228},
            {62, 114},
            {63, 166},
            {64, 6},
            {65, 191},
            {66, 139},
            {67, 98},
            {68, 102},
            {69, 221},
            {70, 48},
            {71, 253},
            {72, 226},
            {73, 152},
            {74, 37},
            {75, 179},
            {76, 16},
            {77, 145},
            {78, 34},
            {79, 136},
            {80, 54},
            {81, 208},
            {82, 148},
            {83, 206},
            {84, 143},
            {85, 150},
            {86, 219},
            {87, 189},
            {88, 241},
            {89, 210},
            {90, 19},
            {91, 92},
            {92, 131},
            {93, 56},
            {94, 70},
            {95, 64},
            {96, 30},
            {97, 66},
            {98, 182},
            {99, 163},
            {100, 195},
            {101, 72},
            {102, 126},
            {103, 110},
            {104, 107},
            {105, 58},
            {106, 40},
            {107, 84},
            {108, 250},
            {109, 133},
            {110, 186},
            {111, 61},
            {112, 202},
            {113, 94},
            {114, 155},
            {115, 159},
            {116, 10},
            {117, 21},
            {118, 121},
            {119, 43},
            {120, 78},
            {121, 212},
            {122, 229},
            {123, 172},
            {124, 115},
            {125, 243},
            {126, 167},
            {127, 87},
            {128, 7},
            {129, 112},
            {130, 192},
            {131, 247},
            {132, 140},
            {133, 128},
            {134, 99},
            {135, 13},
            {136, 103},
            {137, 74},
            {138, 222},
            {139, 237},
            {140, 49},
            {141, 197},
            {142, 254},
            {143, 24},
            {144, 227},
            {145, 165},
            {146, 153},
            {147, 119},
            {148, 38},
            {149, 184},
            {150, 180},
            {151, 124},
            {152, 17},
            {153, 68},
            {154, 146},
            {155, 217},
            {156, 35},
            {157, 32},
            {158, 137},
            {159, 46},
            {160, 55},
            {161, 63},
            {162, 209},
            {163, 91},
            {164, 149},
            {165, 188},
            {166, 207},
            {167, 205},
            {168, 144},
            {169, 135},
            {170, 151},
            {171, 178},
            {172, 220},
            {173, 252},
            {174, 190},
            {175, 97},
            {176, 242},
            {177, 86},
            {178, 211},
            {179, 171},
            {180, 20},
            {181, 42},
            {182, 93},
            {183, 158},
            {184, 132},
            {185, 60},
            {186, 57},
            {187, 83},
            {188, 71},
            {189, 109},
            {190, 65},
            {191, 162},
            {192, 31},
            {193, 45},
            {194, 67},
            {195, 216},
            {196, 183},
            {197, 123},
            {198, 164},
            {199, 118},
            {200, 196},
            {201, 23},
            {202, 73},
            {203, 236},
            {204, 127},
            {205, 12},
            {206, 111},
            {207, 246},
            {208, 108},
            {209, 161},
            {210, 59},
            {211, 82},
            {212, 41},
            {213, 157},
            {214, 85},
            {215, 170},
            {216, 251},
            {217, 96},
            {218, 134},
            {219, 177},
            {220, 187},
            {221, 204},
            {222, 62},
            {223, 90},
            {224, 203},
            {225, 89},
            {226, 95},
            {227, 176},
            {228, 156},
            {229, 169},
            {230, 160},
            {231, 81},
            {232, 11},
            {233, 245},
            {234, 22},
            {235, 235},
            {236, 122},
            {237, 117},
            {238, 44},
            {239, 215},
            {240, 79},
            {241, 174},
            {242, 213},
            {243, 233},
            {244, 230},
            {245, 231},
            {246, 173},
            {247, 232},
            {248, 116},
            {249, 214},
            {250, 244},
            {251, 234},
            {252, 168},
            {253, 80},
            {254, 88},
            {255, 175}
        };
    }

    public Polynomial MultiplyWithAlpha(Polynomial multiplier)
    {
        var alpha = Alpha + multiplier.Alpha;
        if (alpha > 255)
        {
            alpha %= 255;
        }

        return new Polynomial(Coefficient, Exponent, alpha);
    }

    public Polynomial ConvertToIntegerNotation()
    {
        if (Alpha == 0)
        {
            return new Polynomial(1, Exponent, Alpha);
        }

        if (!AntilogValues.ContainsValue(Alpha))
        {
            throw new NotImplementedException($"Missing Antilog entry for value {Alpha}");
        }

        var coefficient = AntilogValues.First(a => a.Value == Alpha).Key;

        return new Polynomial(coefficient, Exponent, Alpha);
    }

    public Polynomial XOR(Polynomial other)
    {
        var coefficient = Coefficient ^ other.Coefficient;
        return new Polynomial(coefficient, Exponent, other.Alpha);
    }
}