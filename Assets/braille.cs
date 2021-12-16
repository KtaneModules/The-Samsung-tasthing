public static class BrailleArrays
{
    public static readonly bool[][][] brailleLetters = new bool[5][][]
    {
        new bool[10][]
        {
            new[] { true, true, false, false, false, false },
            new[] { true, true, false, true, true, false },
            new[] { true, false, true, true, true, false },
            new[] { true, false, true, false, true, false },
            new[] { true, true, true, false, false, true },
            new[] { true, false, true, true, false, true },
            new[] { true, true, true, true, true, true },
            new[] { true, true, false, true, false, true },
            new[] { false, true, true, false, true, true },
            new[] { false, false, true, false, true, false }
        },
        new bool[10][]
        {
            new[] { true, false, false, false, false, false },
            new[] { true, false, true, false, false, false },
            new[] { true, true, true, true, true, false },
            new[] { false, true, true, true, true, false },
            new[] { true, true, true, true, false, true },
            new[] { false, true, true, false, false, false },
            new[] { true, false, false, false, false, true },
            new[] { false, true, false, false, false, true },
            new[] { true, true, false, false, true, true },
            new[] { false, false, true, true, false, false }
        },
        new bool[10][]
        {
            new[] { true, false, false, false, true, false },
            new[] { false, true, false, true, false, false },
            new[] { true, false, true, true, false, false },
            new[] { false, true, true, true, false, false },
            new[] { true, false, true, false, false, true },
            new[] { true, false, true, true, true, true },
            new[] { false, true, false, false, true, false },
            new[] { false, true, false, false, false, false },
            new[] { true, true, false, true, true, true },
            new[] { false, true, false, true, false, true }
        },
        new bool[10][]
        {
            new[] { true, false, false, true, false, false },
            new[] { true, true, false, true, false, false },
            new[] { true, true, true, false, false, false },
            new[] { true, true, true, true, true, false },
            new[] { true, false, true, false, true, true },
            new[] { false, true, true, true, false, true },
            new[] { false, false, true, true, true, false },
            new[] { false, false, true, true, false, true },
            new[] { true, false, false, true, false, true },
            new[] { true, false, false, true, true, true }
        },
        new bool[10][]
        {
            new[] { true, false, false, true, true, false },
            new[] { true, true, false, false, true, false },
            new[] { false, true, false, true, true, false },
            new[] { true, true, true, false, true, false },
            new[] { false, true, false, true, true, true },
            new[] { false, true, true, true, true, true },
            new[] { false, true, true, false, true, false },
            new[] { true, true, false, false, false, true },
            new[] { true, true, true, false, true, true },
            new[] { true, false, false, false, true, true }
        }
    };
}
