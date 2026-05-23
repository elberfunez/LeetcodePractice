namespace LeetcodePractice.Problems.Trees;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class ValidBinarySearchTreeTemplate
{
    public void Solve()
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
        // Your solution here
        return true;
    }
}
