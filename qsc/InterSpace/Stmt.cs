namespace qsc.InterSpace;

internal class Stmt : Node
{
    public Stmt()
    {
    }

    public int After { get; set; } = 0;

    public static Stmt Null = new();
    public static Stmt Enclosing = Stmt.Null; // Used for break instruction
    public virtual void Gen(int begin, int after)
    {
    }
}
