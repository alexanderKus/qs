using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal sealed class While : Stmt
{
    public While()
    {
    }

    public Expr Expr { get; private set; } = null!;
    public Stmt Stmt { get; private set; } = null!;

    public void Init(Expr x, Stmt s)
    {
        Expr = x;
        Stmt = s;
        if (Expr.Type != Type.BOOL)
            Expr.Error("Boolean required in while");
    }

    public void Gen(int before, int after)
    {
        After = after;
        Expr.Jumping(0, after);
        int label = NewLabel();
        EmitLabel(label);
        Stmt.Gen(label, before);
        Emit($"goto L{before}");
    }
}
