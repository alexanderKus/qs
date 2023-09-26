namespace qsc;

public sealed class Real : Token
{
	public Real(float value) : base(qsc.Tag.REAL)
	{
        Value = value;
    }

    public float Value { get; }
    public override string ToString()
    {
        return Value.ToString();
    }
}

