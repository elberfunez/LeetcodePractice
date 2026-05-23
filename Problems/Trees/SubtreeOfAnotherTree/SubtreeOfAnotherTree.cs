namespace LeetcodePractice.Problems.Trees;

// Problem statement: SubtreeOfAnotherTree.md
[Problem(572, "Subtree of Another Tree", Category.Trees, Difficulty.Easy)]
public class SubtreeOfAnotherTree : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", TreeHelper.CreateTree([1, 2, 3, 4, 5]), TreeHelper.CreateTree([2, 4, 5]), true);
        Test("Example 2", TreeHelper.CreateTree([1, 2, 3, 4, 5, null, null, 6]), TreeHelper.CreateTree([2, 4, 5]), false);
    }

    private void Test(string name, TreeNode root, TreeNode subRoot, bool expected)
    {
        bool result = IsSubtreeSolution(root, subRoot);

        Console.WriteLine($"{name}: {(result == expected ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    root = {TreeHelper.TreeToString(root)}, subRoot = {TreeHelper.TreeToString(subRoot)}");
        if (result != expected) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    public bool IsSubtreeSolution(TreeNode root, TreeNode subRoot)
    {
        if (subRoot == null) return true;
        if (root == null) return false;

        if (SameTree(root, subRoot)) return true;
        else
        {
            return IsSubtreeSolution(root.left, subRoot) || IsSubtreeSolution(root.right, subRoot);
        }

        return false;
    }

    public bool SameTree(TreeNode p, TreeNode q)
    {
        if (p == null && q == null) return true;
        if (p != null && q == null) return false;
        if (p.val != q.val) return false;
        
        bool leftSideSame = SameTree(p.left, q.left);
        bool rightSideSame = SameTree(p.right, q.right);
        
        return leftSideSame && rightSideSame;
    }
}
