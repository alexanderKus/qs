namespace qsc.SourceSpace;

public sealed class ExampleSource : ISource
{
    public char[] Text { get; } = @"
#Test file <this comment will be skiped by lexer>
int x = 10
if(x<10) {
    bool b=true
    float y=5.56
    stirng s=""testfile""
}".ToCharArray();
    public int Position { get; set; } = 0;
}
