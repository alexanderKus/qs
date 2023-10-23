using qsc.LexerSpace;
using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal class Constant : Expr
{

    public Constant(Token token, Type type)
        : base(token, type)
    {
    }

    public Constant(int i)
        : base(new Num(i), Type.INT)
    {
    }

    public static Constant
        TRUE = new(Word.TRUE, Type.BOOL),
        FALSE = new(Word.FALSE, Type.BOOL);

    public void Jumping(int t, int f)
    {
        if (this == TRUE && t != 0)
            Emit($"goto L{t}");
        else if (this == FALSE && f != 0)
            Emit($"goto L{f}");
    }
}
