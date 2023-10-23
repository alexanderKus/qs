using qsc.LexerSpace;
using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal class Logical : Expr
{
    public Logical(Token token, Expr expr1, Expr expr2)
        : base(token, null!)
    {
        Expr1 = expr1;
        Expr2 = expr2;
        Type = Check(expr1.Type, expr2.Type)!;
        if (Type is null)
            Error("Type error");
    }

    public Expr Expr1 { get; }
    public Expr Expr2 { get; }

    public static Type? Check(Type p1, Type p2)
        => p1 == Type.BOOL && p2 == Type.BOOL ? Type.BOOL : null;

    public Expr Gen()
    {
        int f = NewLabel();
        int a = NewLabel();
        Temp temp = new(Type);
        Jumping(0, f);
        Emit($"{temp} = true");
        Emit($"goto L{a}");
        EmitLabel(f);
        Emit($"{temp} = false");
        EmitLabel(a);
        return temp;
    }
}
