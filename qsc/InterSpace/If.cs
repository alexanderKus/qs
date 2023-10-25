using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal sealed class If : Stmt
{
    public If(Expr x, Stmt s)
    {
        Expr = x;
        Stmt = s;
        if (Expr.Type != Type.BOOL)
            Expr.Error("Boolean required in if");
    }

    public Expr Expr { get; }
    public Stmt Stmt { get; }
}
