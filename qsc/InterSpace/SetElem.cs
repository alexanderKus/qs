using Type = qsc.SymbolsSpace.Type;
using Array = qsc.SymbolsSpace.Array;

namespace qsc.InterSpace;

internal sealed class SetElem : Stmt
{
    public SetElem(Access x, Expr y)
    {
        Array = x.Array;
        Expr = y;
        Index = x.Index;
        if (Check(x.Type, y.Type) is null)
            Error("Type error");
    }

    public Id Array { get; }
    public Expr Expr { get; }
    public Expr Index { get; }

    public static Type? Check(Type p1, Type p2)
    {
        if (p1 is Array || p1 is Array)
            return null;
        else if (p1 == p2)
            return p2;
        else if (Type.Numeric(p1) && Type.Numeric(p2))
            return p2;
        return null;
    }

    public void Gen(int before, int after)
    {
        var s1 = Index.Reduce().ToString();
        var s2 = Expr.Reduce().ToString();
        Emit($"{Array} [ {s1} ] = {s2}");
    }
}
