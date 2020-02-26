public static class BrailleArrays
{
    public static readonly bool[][][] brailleLetters = new bool[5][][]
    {
        new bool[10][]
        {
            new bool[6] { true, true, false, false, false, false },
            new bool[6] { true, true, false, true, true, false },
            new bool[6] { true, false, true, true, true, false },
            new bool[6] { true, false, true, false, true, false },
            new bool[6] { true, true, true, false, false, true },
            new bool[6] { true, false, true, true, false, true },
            new bool[6] { true, true, true, true, true, true },
            new bool[6] { true, true, false, true, false, true },
            new bool[6] { false, true, true, false, true, true },
            new bool[6] { false, false, true, false, true, false }
        },
        new bool[10][]
        {
            new bool[6] { true, false, false, false, false, false },
            new bool[6] { true, false, true, false, false, false },
            new bool[6] { true, true, true, true, true, false },
            new bool[6] { false, true, true, true, true, false },
            new bool[6] { true, true, true, true, false, true },
            new bool[6] { false, true, true, false, false, false },
            new bool[6] { true, false, false, false, false, true },
            new bool[6] { false, true, false, false, false, true },
            new bool[6] { true, true, false, false, true, true },
            new bool[6] { false, false, true, true, false, false }
        },
        new bool[10][]
        {
            new bool[6] { true, false, false, false, true, false },
            new bool[6] { false, true, false, true, false, false },
            new bool[6] { true, false, true, true, false, false },
            new bool[6] { false, true, true, true, false, false },
            new bool[6] { true, false, true, false, false, true },
            new bool[6] { true, false, true, true, true, true },
            new bool[6] { false, true, false, false, true, false },
            new bool[6] { false, true, false, false, false, false },
            new bool[6] { true, true, false, true, true, true },
            new bool[6] { false, true, false, true, false, true }
        },
        new bool[10][]
        {
            new bool[6] { true, false, false, true, false, false },
            new bool[6] { true, true, false, true, false, false },
            new bool[6] { true, true, true, false, false, false },
            new bool[6] { true, true, true, true, true, false },
            new bool[6] { true, false, true, false, true, true },
            new bool[6] { false, true, true, true, false, true },
            new bool[6] { false, false, true, true, true, false },
            new bool[6] { false, false, true, true, false, true },
            new bool[6] { true, false, false, true, false, true },
            new bool[6] { true, false, false, true, true, true }
        },
        new bool[10][]
        {
            new bool[6] { true, false, false, true, true, false },
            new bool[6] { true, true, false, false, true, false },
            new bool[6] { false, true, false, true, true, false },
            new bool[6] { true, true, true, false, true, false },
            new bool[6] { false, true, false, true, true, true },
            new bool[6] { false, true, true, true, true, true },
            new bool[6] { false, true, true, false, true, false },
            new bool[6] { true, true, false, false, false, true },
            new bool[6] { true, true, true, false, true, true },
            new bool[6] { true, false, false, false, true, true }
        }
    };
}
