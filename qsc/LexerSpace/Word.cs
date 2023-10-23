namespace qsc.LexerSpace;

internal class Word : Token
{
    public Word(int tag, string lexeme) : base(tag)
        => Lexeme = lexeme;

    public string Lexeme { get; }
    public override string ToString()
        => Lexeme;
    public static Word
        LE = new(LexerSpace.Tag.LE, "<="),
        GE = new(LexerSpace.Tag.GE, ">="),
        EQ = new(LexerSpace.Tag.EQ, "=="),
        NE = new(LexerSpace.Tag.NE, "!="),
        AND = new(LexerSpace.Tag.AND, "&&"),
        OR = new(LexerSpace.Tag.OR, "||"),
        MINUS = new(LexerSpace.Tag.MINUS, "minus"),
        TRUE = new(LexerSpace.Tag.TRUE, "true"),
        FALSE = new(LexerSpace.Tag.FALSE, "fase"),
        TEMP = new(LexerSpace.Tag.TEMP, "t");
}
