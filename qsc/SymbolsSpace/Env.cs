using qsc.LexerSpace;

namespace qsc.SymbolsSpace;

internal sealed class Env
{
    private readonly Dictionary<Token, object> _table = new(); // TODO: object -> class Id
    private Env? _prev;

    public Env(Env? n = null)
        => _prev = n;

    public void Put(Token w, object i) // TODO: object -> class Id
        => _table.Add(w, i);

    public object? Get(Token w)
    {
        for (Env e = this; e != null; e = e._prev!)
        {
            if (e._table.ContainsKey(w))
                return e._table[w];
        }
        return null;
    }
}
