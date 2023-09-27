using System.Text;
using qsc.SourceSpace;

namespace qsc.LexerSpace;

internal sealed class Lexer
{
    private readonly ISource _source;
    private readonly Dictionary<string, Word> _words = new();
    private char _peek = ' ';

    public Lexer(ISource source)
    {
        _source = source;
        Reserve(new Word(Tag.IF, "if"));
        Reserve(new Word(Tag.ELSE, "else"));
        Reserve(new Word(Tag.DO, "do"));
        Reserve(new Word(Tag.WHILE, "while"));
        Reserve(new Word(Tag.BREAK, "break"));
        Reserve(Word.TRUE);
        Reserve(Word.FALSE);
        // TODO: reserve words for types
    }

    public int Line { get; set; }

    private void Reserve(Word token)
        => _words.Add(token.Lexeme, token);

    public Token Scan()
    {
        for (; ; Readch())
        {
            if (_peek == ' ' || _peek == '\t')
                continue;
            else if (_peek == '#')
            {
                for (; ; Readch())
                {
                    if (_peek == '\n')
                    {
                        Line += 1;
                        break;
                    }
                }
            }
            else if (_peek == '\n')
                Line += 1;
            else
                break;
        }
        switch (_peek)
        {
            case '=':
                {
                    if (Reachch('='))
                        return Word.EQ;
                    else
                        return new Token('=');
                }
            case '!':
                {
                    if (Reachch('='))
                        return Word.NE;
                    else
                        return new Token('!');
                }
            case '>':
                {
                    if (Reachch('='))
                        return Word.GE;
                    else
                        return new Token('>');
                }
            case '<':
                {
                    if (Reachch('='))
                        return Word.LE;
                    else
                        return new Token('<');
                }
        }
        if (char.IsDigit(_peek))
        {
            var v = 0;
            do
            {
                v = 10 * v + (int)char.GetNumericValue(_peek);
                Readch();
            } while (char.IsDigit(_peek));
            if (_peek != '.')
                return new Num(v);
            float x = v;
            float d = 10;
            for (; ; )
            {
                Readch();
                if (!char.IsDigit(_peek))
                    break;
                x += (int)char.GetNumericValue(_peek) / d;
                d *= 10;
            }
            return new Real(x);
        }
        if (char.IsLetter(_peek))
        {
            StringBuilder builder = new();
            do
            {
                builder.Append(_peek);
                Readch();
            } while (char.IsLetterOrDigit(_peek));
            var str = builder.ToString();
            if (_words.ContainsKey(str))
                return _words[str];
            Word word = new(Tag.ID, str);
            _words.Add(word.Lexeme, word);
            return word;
        }
        Token token = new(_peek);
        _peek = ' ';
        return token;
    }

    private bool Reachch(char c)
    {
        Readch();
        if (_peek != c)
            return false;
        _peek = ' ';
        return true;
    }

    private void Readch()
        => _peek = _source.Text[_source.Position++];
}
