using qsc.LexerSpace;

namespace qsc.InterSpace;

internal sealed class Or : Logical
{
    public Or(Token token, Expr expr1, Expr expr2)
        : base(token, expr1, expr2)
    {
    }

    public void Jumping(int t, int f)
    {
        int label = t != 0 ? t : NewLabel();
        Expr1.Jumping(label, 0);
        Expr2.Jumping(t, f);
        if (t == 0)
            EmitLabel(label);
    }
}
