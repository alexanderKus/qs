using qsc.LexerSpace;

namespace qsc.InterSpace;

internal sealed class And : Logical
{
    public And(Token token, Expr expr1, Expr expr2)
        : base(token, expr1, expr2)
    {
    }

    public void Jumping(int t, int f)
    {
        int label = f != 0 ? f : NewLabel();
        Expr1.Jumping(0, label);
        Expr2.Jumping(t, f);
        if (f == 0)
            EmitLabel(label);
    }
}
