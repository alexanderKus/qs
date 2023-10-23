using qsc.LexerSpace;
using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal sealed class Temp : Expr
{
    private int _number = 0;

    public Temp(Type type)
        : base(Word.TEMP, type)
        => _number = ++Count;

    public static int Count { get; set; } = 0;

    public override string ToString()
        => $"t {_number}";
}

