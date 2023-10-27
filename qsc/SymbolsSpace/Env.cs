using qsc.InterSpace;
using qsc.LexerSpace;

namespace qsc.SymbolsSpace;

internal sealed class Env
{
    private readonly Dictionary<Token, Id> _table = new();
    private Env? _prev;

    public Env(Env? n = null)
        => _prev = n;

    public void Put(Token w, Id i)
        => _table.Add(w, i);

    public Id? Get(Token w)
    {
        for (Env e = this; e != null; e = e._prev!)
        {
            if (e._table.ContainsKey(w))
                return e._table[w];
        }
        return null;
    }
}
