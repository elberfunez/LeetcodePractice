namespace LeetcodePractice;

public abstract class ProblemBase : IProblem
{
    public void Run()
    {
        Solve();
    }

    protected abstract void Solve();
}
