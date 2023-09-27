namespace qsc
{
    internal sealed class Parser
    {
        private static int _lookahead;

        public Parser()
        {
            _lookahead = Console.Read();
        }

        public void Expr()
        {
            Term();
            while (true)
            {
                if (_lookahead == '+')
                {
                    Match('+');
                    Term();
                    Console.Write('+');
                }
                else if (_lookahead == '-')
                {
                    Match('-');
                    Term();
                    Console.Write('-');
                }
                else
                {
                    return;
                }
            }
        }

        private void Term()
        {
            if (char.IsDigit((char)_lookahead))
            {
                Console.Write((char)_lookahead);
                Match(_lookahead);
            }
            else
            {
                throw new Exception("Syntax error!");
            }
        }

        private void Match(int t)
        {
            if (_lookahead == t)
                _lookahead = Console.Read();
            else
                throw new Exception("Syntax error!");
        }
    }
}