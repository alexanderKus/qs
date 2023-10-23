namespace qsc.SymbolsSpace;

internal sealed class Array : Type
{
    public Array(int size, Type type)
        : base("[]", LexerSpace.Tag.INDEX, size * type.Width)
        => (Size, Of) = (size, type);

    public int Size { get; }
    public Type Of { get; }

    public override string ToString()
    {
        return "[" + Size + "] " + Of.ToString();
    }
}
