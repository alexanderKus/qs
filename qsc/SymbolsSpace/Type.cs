using qsc.LexerSpace;

namespace qsc.SymbolsSpace;

internal class Type : Word
{
    public Type(string s, int tag, int width) : base(tag, s)
        => Width = width;

    public int Width { get; } // Used for memory allocation
    public static Type
        INT = new("int", LexerSpace.Tag.BASIC, 4),
        FLOAT = new("float", LexerSpace.Tag.BASIC, 8),
        CHAR = new("char", LexerSpace.Tag.BASIC, 1),
        BOOL = new("bool", LexerSpace.Tag.BASIC, 1);

    public static bool Numeric(Type type)
     => type == CHAR || type == INT || type == FLOAT;

    public static Type? Max(Type type1, Type type2)
    {
        if (!Numeric(type1) || !Numeric(type2))
            return null;
        else if (type1 == FLOAT || type2 == FLOAT)
            return FLOAT;
        else if (type1 == INT || type2 == INT)
            return INT;
        else
            return CHAR;
    }
}
