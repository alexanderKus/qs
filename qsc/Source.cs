namespace qsc;

public sealed class Source
{
	public Source(string filename)
	{
		Text = File.ReadAllText(filename).ToCharArray();
	}

	public char[] Text { get; }
	public int Position { get; set; } = 0;
}
