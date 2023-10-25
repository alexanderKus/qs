using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal sealed class Else : Stmt
{
    public Else(Expr x, Stmt s1, Stmt s2)
    {
        Expr = x;
        Stmt1 = s1;
        Stmt2 = s2;
        if (Expr.Type != Type.BOOL)
            Expr.Error("Boolean required in if/else");
    }

    public Expr Expr { get; }
    public Stmt Stmt1 { get; }
    public Stmt Stmt2 { get; }

    public void Gen(int before, int after)
    {
        int label1 = NewLabel();
        int label2 = NewLabel();

        Expr.Jumping(0, label2);
        EmitLabel(label1);
        Stmt1.Gen(label1, after);
        Emit($"goto L{after}");

        EmitLabel(label2);
        Stmt2.Gen(label2, after);
    }
}
