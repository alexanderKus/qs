using qsc.LexerSpace;
using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal class Op : Expr
{
    public Op(Token token, Type type) : base(token, type)
    {
    }

    public override Expr Reduce()
    {
        Expr x = Gen();
        Temp t = new(Type);
        Emit($"{t} = {x}");
        return t;
    }
}
