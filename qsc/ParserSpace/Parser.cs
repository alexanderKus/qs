using qsc.LexerSpace;

namespace qsc
{
    internal sealed class Parser
    {
        public Parser(Lexer l)
        {
            Lex = l;
        }

        public Lexer Lex { get; }
    }
}