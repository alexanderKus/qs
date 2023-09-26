namespace qsc;

public sealed class Word : Token
{
	public Word(int tag, string lexeme) : base (tag)
		=> Lexeme = lexeme;

	public string Lexeme { get; }
	public override string ToString()
		=> Lexeme;
	public static Word
		LE = new Word(qsc.Tag.LE, "<="),
		GE = new Word(qsc.Tag.GE, ">="),
		EQ = new Word(qsc.Tag.EQ, "=="),
		NE = new Word(qsc.Tag.NE, "!=");
}
