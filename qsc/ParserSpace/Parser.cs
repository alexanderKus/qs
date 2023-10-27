using qsc.InterSpace;
using qsc.LexerSpace;
using qsc.SymbolsSpace;
using Array = qsc.SymbolsSpace.Array;
using Type = qsc.SymbolsSpace.Type;

namespace qsc
{
    internal sealed class Parser
    {
        private readonly Lexer _lex;
        private Token _look;
        private Env _top = null!;
        private int _used = 0;

        public Parser(Lexer l)
        {
            _lex = l;
            Move();
        }

        public void Program()
        {
            Stmt s = Block();
            int begin = s.NewLabel();
            int after = s.NewLabel();
            s.EmitLabel(begin);
            s.Gen(begin, after);
            s.EmitLabel(after);
        }

        private void Move()
            => _look = _lex.Scan();
        private static void Error(string msg)
            => throw new Exception($"Near line {Lexer.Line} {msg}");
        private void Match(int t)
        {
#if DEBUG
            Console.WriteLine($"Match({t}) _look.Tag={_look.Tag}");
#endif
            if (_look.Tag == t)
                Move();
            else
                Error("Syntax Error");
        }
        private Stmt Block()
        {
            Match('{');
            Env savedEnv = _top;
            _top = new(_top);
            Decls();
            Stmt s = Stmts();
            Match('}');
            _top = savedEnv;
            return s;
        }

        private void Decls()
        {
            while (_look.Tag == Tag.BASIC)
            {
                Type p = GetType();
                Token tok = _look;
                Match(Tag.ID);
                Match(';');
                Id id = new((Word)tok, p, _used);
                _top.Put(tok, id);
                _used += p.Width;
            }
        }

        private Type GetType()
        {
            Type p = (Type)_look; // Want to get _look.Tag == Tag.BASIC
            Match(Tag.BASIC);
            if (_look.Tag != '[') // T -> basic
                return p;
            return Dims(p); // Return array type
        }

        private Type Dims(Type p)
        {
            Match('[');
            Token tok = _look;
            Match(Tag.NUM);
            Match(']');
            if (_look.Tag == '[')
                p = Dims(p);
            return new Array(((Num)tok).Value, p);
        }

        private Stmt Stmts()
        {
            if (_look.Tag == '}')
                return Stmt.Null;
            return new Seq(GetStmt(), Stmts());
        }

        private Stmt GetStmt()
        {
            Expr x;
            Stmt s, s1, s2;
            Stmt savedStmt; // waiting save if case of break instruction;

            switch (_look.Tag)
            {
                case ';':
                    Move();
                    return Stmt.Null;
                case Tag.IF:
                    Match(Tag.IF);
                    Match('(');
                    x = Bool();
                    Match(')');
                    s1 = GetStmt();
                    if (_look.Tag != Tag.ELSE)
                        return new If(x, s1);
                    Match(Tag.ELSE);
                    s2 = GetStmt();
                    return new Else(x, s1, s2);
                case Tag.WHILE:
                    While whileNode = new();
                    savedStmt = Stmt.Enclosing;
                    Stmt.Enclosing = whileNode;
                    Match(Tag.WHILE);
                    Match('(');
                    x = Bool();
                    Match(')');
                    s1 = GetStmt();
                    whileNode.Init(x, s1);
                    Stmt.Enclosing = savedStmt; // Get back Stmt.Enclosing
                    return whileNode;
                case Tag.DO:
                    Do doNode = new();
                    savedStmt = Stmt.Enclosing;
                    Stmt.Enclosing = doNode;
                    Match(Tag.DO);
                    s1 = GetStmt();
                    Match(Tag.WHILE);
                    Match('(');
                    x = Bool();
                    Match(')');
                    Match(';');
                    doNode.Init(s1, x);
                    Stmt.Enclosing = savedStmt; // Get back Stmt.Enclosing
                    return doNode;
                case Tag.BREAK:
                    Match(Tag.BREAK);
                    Match(';');
                    return new Break();
                case '{':
                    return Block();
                default:
                    return Assign();
            }
        }

        private Stmt Assign()
        {
            Stmt stmt;
            Token t = _look;
            Match(Tag.ID);
            Id? id = _top.Get(t);
            if (id is null)
                Error($"{t} undeclared");
            if (_look.Tag == '=') // S -> id = E
            {
                Move();
                stmt = new Set(id!, Bool());
            }
            else  // S -> L = E
            {
                Access x = Offset(id!);
                Match('=');
                stmt = new SetElem(x, Bool());
            }
            Match(';');
            return stmt;
        }

        private Expr Bool()
        {
            Expr x = Join();
            while (_look.Tag == Tag.OR)
            {
                Token t = _look;
                Move();
                x = new Or(t, x, Join());
            }
            return x;
        }
        private Expr Join()
        {
            Expr x = Equality();
            while (_look.Tag == Tag.AND)
            {
                Token t = _look;
                Move();
                x = new And(t, x, Equality());
            }
            return x;

        }
        private Expr Equality()
        {
            Expr x = Expr();
            while (_look.Tag == Tag.EQ || _look.Tag == Tag.NE)
            {
                Token t = _look;
                Move();
                x = new Rel(t, x, Rel());
            }
            return x;
        }
        private Expr Rel()
        {
            Expr x = Expr();
            switch (_look.Tag)
            {
                case '<':
                case Tag.LE:
                case Tag.GE:
                case '>':
                    Token t = _look;
                    Move();
                    return new Rel(t, x, Expr());
                default:
                    return x;
            }
        }
        private Expr Expr()
        {
            Expr x = Term();
            while (_look.Tag == '+' || _look.Tag == '-')
            {
                Token t = _look;
                Move();
                x = new Arith(t, x, Rel());
            }
            return x;
        }
        private Expr Term()
        {
            Expr x = Unary();
            while (_look.Tag == '*' || _look.Tag == '/')
            {
                Token t = _look;
                Move();
                x = new Arith(t, x, Unary());
            }
            return x;
        }
        private Expr Unary()
        {
            if (_look.Tag == '-')
            {
                Move();
                return new Unary(Word.MINUS, Unary());
            }
            else if (_look.Tag == '!')
            {
                Token t = _look;
                Move();
                return new Not(t, Unary());
            }
            return Factor();
        }
        private Expr Factor()
        {
            Expr x = null;
            switch (_look.Tag)
            {
                case '(':
                    Move();
                    x = Bool();
                    Match(')');
                    return x;
                case Tag.NUM:
                    x = new Constant(_look, Type.INT);
                    Move();
                    return x;
                case Tag.REAL:
                    x = new Constant(_look, Type.FLOAT);
                    Move();
                    return x;
                case Tag.TRUE:
                    x = Constant.TRUE;
                    Move();
                    return x;
                case Tag.FALSE:
                    x = Constant.FALSE;
                    Move();
                    return x;
                case Tag.ID:
                    var s = _look.ToString();
                    Id id = _top.Get(_look);
                    if (id is null)
                        Error($"{_look} undeclared");
                    Move();
                    if (_look.Tag != '[')
                        return id!;
                    return Offset(id!);
                default:
                    Error("Syntax error");
                    return x!;
            }
        }
        private Access Offset(Id a) // I -> [E] | [E] I
        {
            Expr i, w, t1, t2, loc;
            Type type = a.Type;
            Match('[');
            i = Bool();
            Match(']');
            type = ((Array)type).Of;
            w = new Constant(type.Width);
            t1 = new Arith(new Token('*'), i, w);
            loc = t1;
            while (_look.Tag == '[')
            {
                Match('[');
                i = Bool();
                Match(']');
                type = ((Array)type).Of;
                w = new Constant(type.Width);
                t1 = new Arith(new Token('*'), i, w);
                t2 = new Arith(new Token('+'), loc, t1);
                loc = t2;
            }
            return new Access(a, loc, type);
        }
    }
}
