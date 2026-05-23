namespace LeetcodePractice.Problems.Trees;

// Problem statement: SameTree.md
[Problem(100, "Same Tree", Category.Trees, Difficulty.Easy)]
public class SameTree : ProblemBase
{
    protected override void Solve()
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
        if (p == null && q == null) return true;
        if (p == null || q == null) return false;
        if (p.val != q.val) return false;
        // Call recursively
        return IsSameTreeSolution(p.left, q.left) && IsSameTreeSolution(p.right, q.right);
    }
}
