namespace qsc;

internal static class Postfix
{
    public static void Run()
    {
        Parser parser = new();
        parser.Expr();
        Console.WriteLine();
    }
}
