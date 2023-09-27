namespace qsc.LexerSpace;

internal sealed class Real : Token
{
    public Real(float value) : base(LexerSpace.Tag.REAL)
    {
        Value = value;
    }

    public float Value { get; }
    public override string ToString()
    {
        return Value.ToString();
    }
}

