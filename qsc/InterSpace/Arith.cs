using qsc.LexerSpace;
using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal sealed class Arith : Op
{
    public Arith(Token token, Expr expr1, Expr expr2)
        : base(token, null!)
    {
        Expr1 = expr1;
        Expr2 = expr2;
        var type = Type.Max(expr1.Type, expr2.Type);
        if (type is null)
            Error("Type error");
    }

    public Expr Expr1 { get; }
    public Expr Expr2 { get; }

    public override Expr Gen()
        => new Arith(Op, Expr1.Reduce(), Expr2.Reduce());

    public override string ToString()
        => $"{Expr1} {Op} {Expr2}";
}

