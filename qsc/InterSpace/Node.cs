using qsc.LexerSpace;

namespace qsc.InterSpace;

internal class Node
{
    private int _lexLine = 0;
    public Node()
        => _lexLine = Lexer.Line;

    public static int Labels { get; private set; } = 0;

    public void Error(string msg)
        => throw new Exception($"Near line {_lexLine}: {msg}");
    public static int NewLabel()
        => ++Labels;
    public static void EmitLabel(int i)
        => Console.Write($"L{i}:");
    public static void Emit(string s)
        => Console.WriteLine($"\t{s}");
}
