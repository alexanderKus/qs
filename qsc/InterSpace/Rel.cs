using qsc.LexerSpace;
using Type = qsc.SymbolsSpace.Type;
using Array = qsc.SymbolsSpace.Array;

namespace qsc.InterSpace;

internal sealed class Rel : Logical
{
    public Rel(Token token, Expr expr1, Expr expr2)
        : base(token, expr1, expr2)
    {
    }

    public static Type? Check(Type p1, Type p2)
    {
        if (p1 is Array || p1 is Array)
            return null;
        else if (p1 == p2)
            return Type.BOOL;
        return null;
    }

    public void Jumping(int t, int f)
    {
        Expr a = Expr1.Reduce();
        Expr b = Expr2.Reduce();
        var text = $"{a} {Op} {b}";
        EmitJumps(text, t, f);
    }
}

