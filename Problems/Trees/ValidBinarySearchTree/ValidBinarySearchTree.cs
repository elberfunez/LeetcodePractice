namespace LeetcodePractice.Problems.Trees;

// Problem statement: ValidBinarySearchTree.md
[Problem(98, "Valid Binary Search Tree", Category.Trees, Difficulty.Medium)]
public class ValidBinarySearchTree : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", TreeHelper.CreateTree([2, 1, 3]), true);
        Test("Example 2", TreeHelper.CreateTree([1, 2, 3]), false);
    }

    private void Test(string name, TreeNode root, bool expected)
    {
        bool result = IsValidBSTSolution(root);

        Console.WriteLine($"{name}: {(result == expected ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {TreeHelper.TreeToString(root)}");
        if (result != expected) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    public bool IsValidBSTSolution(TreeNode root)
    {
        return Validate(root, long.MinValue, long.MaxValue);
    }

    private bool Validate(TreeNode node, long min, long max) // recursive Helper Function
    {
        if (node == null) return true;
        if (node.val <= min || node.val >= max) return false;
        
        // Left side subtree
        bool leftValid = Validate(node.left, min, node.val);
        // Right side subtree
        bool rightValid = Validate(node.right, node.val, max);
        return leftValid && rightValid;
    }
}
