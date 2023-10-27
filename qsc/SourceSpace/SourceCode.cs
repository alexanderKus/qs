namespace qsc.SourceSpace;


public class SourceCode : ISource
{
    public SourceCode(char[] text)
      => Text = text;

    public char[] Text { get; }

    public int Position { get; set; } = 0;
}
