namespace qsc.LexerSpace;

internal class Token
{
    public Token(int tag)
        => Tag = tag;

    public int Tag { get; }

    public override string ToString()
    {
        var c = (char)Tag;
        return c.ToString();
    }
}
