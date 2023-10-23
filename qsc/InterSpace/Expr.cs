using qsc.LexerSpace;
using Type = qsc.SymbolsSpace.Type;

namespace qsc.InterSpace;

internal class Expr : Node
{
    public Expr(Token token, Type type)
        => (Op, Type) = (token, type);

    public Token Op { get; set; }
    public Type Type { get; set; }

    public virtual Expr Gen()
        => this;
    public virtual Expr Reduce()
        => this;
    public virtual void Jumping(int t, int f)
        => EmitJumps(ToString(), t, f);
    public void EmitJumps(string test, int t, int f)
    {
        if (t != 0 && f != 0)
        {
            Emit($"if {test} goto L{t}");
            Emit($"goto L{f}");
        }
        else if (t != 0)
        {
            Emit($"if {test} goto L{t}");
        }
        else if (f != 0)
        {
            Emit($"ifflase {test} goto L{f}");
        }
        // fall through
    }

    public override string ToString()
        => Op.ToString();
}
