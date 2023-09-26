using qsc;

var filename = Environment.GetCommandLineArgs()[1];
Lexer lexer = new(filename);
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
