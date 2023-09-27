namespace qsc.LexerSpace;

internal sealed class Num : Token
{
    public Num(int value) : base(LexerSpace.Tag.NUM)
        => Value = value;

    public int Value { get; }
    public override string ToString()
    {
        return Value.ToString();
    }
}
