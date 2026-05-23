namespace LeetcodePractice.Problems.Trees;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class MaximumDepthOfBinaryTreeTemplate
{
    public void Solve()
    {
        Test("Example 1", TreeHelper.CreateTree([1, 2, 3, null, null, 4]), 3);
        Test("Example 2", TreeHelper.CreateTree([]), 0);
    }

    private void Test(string name, TreeNode root, int expected)
    {
        int result = MaxDepthSolution(root);

        Console.WriteLine($"{name}: {(result == expected ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {TreeHelper.TreeToString(root)}");
        if (result != expected) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    public int MaxDepthSolution(TreeNode root)
    {
        // Your solution here
        return 0;
    }
}
