using qsc.LexerSpace;
using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal sealed class Unary : Op
{
    public Unary(Token token, Expr expr)
        : base(token, null)
    {
        Expr = expr;
        var type = Type.Max(Type.INT, expr.Type);
        if (type is null)
            Error("Type error");
    }

    public Expr Expr { get; }

    public override Expr Gen()
        => new Unary(Op, Expr.Reduce());

    public override string ToString()
        => $"{Op} {Expr}";
}
