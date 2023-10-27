using qsc;
using qsc.LexerSpace;
using qsc.SourceSpace;

var argv = Environment.GetCommandLineArgs();

if (argv.Length < 2)
{
    Console.WriteLine("Usage: qsc <file>");
}

var sourceFile = argv[1];
var sourceCode = File.ReadAllText(sourceFile).ToCharArray();
Console.WriteLine(sourceCode);
ISource source = new SourceCode(sourceCode);
Lexer lexer = new(source);
Parser parser = new(lexer);
parser.Program();
