using qsc.LexerSpace;
using qsc.SourceSpace;

ISource source = new ExampleSource();
Lexer lexer = new(source);
while (true)
{
    try
    {
        Token token = lexer.Scan();
        Console.WriteLine($"token: {token}");
    }
    catch
    {
        break;
    }
}
