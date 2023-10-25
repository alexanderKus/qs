namespace qsc.InterSpace;

internal sealed class Break : Stmt
{
    public Break()
    {
        if (Stmt.Enclosing is null) 
            Error("Unenclosed break");
        Stmt = Stmt.Enclosing!;
    }

    public Stmt Stmt { get; set; }

    public void Gen(int before, int after)
    {
        Emit($"goto L{Stmt.After}");
    }
}
