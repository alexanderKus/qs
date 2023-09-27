namespace qsc.SourceSpace;

public interface ISource
{
    public char[] Text { get; }
    public int Position { get; set; }
}

