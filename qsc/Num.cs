namespace qsc;

public sealed class Num : Token
{
	public Num(int value) : base (qsc.Tag.NUM)
		=> Value = value;

	public int Value { get; }
	public override string ToString()
    {
        return Value.ToString();
    }
}
