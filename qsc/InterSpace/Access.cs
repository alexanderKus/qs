using qsc.LexerSpace;
using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal sealed class Access : Op
{
    public Access(Id a, Expr i, Type p)
        : base(new Word(Tag.INDEX, "[]"), p)
    {
        Array = a;
        Index = i;
    }

    public Id Array { get; }
    public Expr Index { get; }

    public Expr Gen()
        => new Access(Array, Index.Reduce(), Type);

    public void Jumping(int t, int f)
        => EmitJumps(Reduce().ToString(), t, f);
    public override string ToString()
        => $"{Array} [ {Index} ]";
}
