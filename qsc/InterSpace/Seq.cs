namespace qsc.InterSpace;

internal sealed class Seq : Stmt
{
    public Seq(Stmt s1, Stmt s2)
    {
        Stmt1 = s1;
        Stmt2 = s2;
    }

    public Stmt Stmt1 { get; }
    public Stmt Stmt2 { get; }

    public void Gen(int before, int after)
    {
        if (Stmt1 == Stmt.Null)
            Stmt2.Gen(before, after);
        else if (Stmt2 == Stmt.Null)
            Stmt2.Gen(before, after);
        else
        {
            int label = NewLabel();
            Stmt1.Gen(before, label);
            EmitLabel(label);
            Stmt2.Gen(label, after);
        }
    }
}
