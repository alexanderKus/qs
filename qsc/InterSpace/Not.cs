using qsc.LexerSpace;

namespace qsc.InterSpace;

internal sealed class Not : Logical
{
    public Not(Token token, Expr expr2)
        : base(token, expr2, expr2)
    {
    }

    public void Jumping(int t, int f)
    {
        Expr2.Jumping(f, t);
    }

    public override string ToString()
        => $"{Op} {Expr2}";
}
