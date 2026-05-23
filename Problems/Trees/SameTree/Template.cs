namespace LeetcodePractice.Problems.Trees;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class SameTreeTemplate
{
    public void Solve()
    {
        Test("Example 1", TreeHelper.CreateTree([1, 2, 3]), TreeHelper.CreateTree([1, 2, 3]), true);
        Test("Example 2", TreeHelper.CreateTree([4, 7]), TreeHelper.CreateTree([4, null, 7]), false);
        Test("Example 3", TreeHelper.CreateTree([1, 2, 3]), TreeHelper.CreateTree([1, 3, 2]), false);
    }

    private void Test(string name, TreeNode p, TreeNode q, bool expected)
    {
        bool result = IsSameTreeSolution(p, q);

        Console.WriteLine($"{name}: {(result == expected ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    p = {TreeHelper.TreeToString(p)}, q = {TreeHelper.TreeToString(q)}");
        if (result != expected) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    public bool IsSameTreeSolution(TreeNode p, TreeNode q)
    {
        // Your solution here
        return false;
    }
}
