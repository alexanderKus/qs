using qsc.LexerSpace;
using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal sealed class Id : Expr
{
    public Id(Token token, Type type, int offset)
        : base(token, type)
        => Offset = offset;

    public int Offset { get; } // relative address
}
