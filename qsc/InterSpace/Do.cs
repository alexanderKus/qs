using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal sealed class Do : Stmt
{
    public Do()
    {
    }

    public Stmt Stmt { get; private set; } = null!;
    public Expr Expr { get; private set; } = null!;

    public void Init(Stmt s, Expr x)
    {
        Stmt = s;
        Expr = x;
        if (Expr.Type != Type.BOOL)
            Expr.Error("Boolean required in do");
    }

    public void Gen(int before, int after)
    {
        After = after;
        int label = NewLabel();
        Stmt.Gen(before, label);
        EmitLabel(label);
        Expr.Jumping(before, 0);
    }
}
