using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal sealed class Set : Stmt
{
    public Set(Id i, Expr x)
    {
        Id = i;
        Expr = x;
        if (Check(Id.Type, x.Type) is null)
            Error("Type error");
    }

    public Id Id { get; }
    public Expr Expr { get; }

    public static Type? Check(Type p1, Type p2)
    {
        if (Type.Numeric(p1) && Type.Numeric(p2))
            return p2;
        else if (p1 == Type.BOOL && p2 == Type.BOOL)
            return p2;
        return null;
    }

    public void Gen(int before, int after)
    {
        Emit($"{Id} = {Expr.Gen()}");
    }
}
