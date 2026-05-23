namespace LeetcodePractice.Problems.Trees;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class SubtreeOfAnotherTreeTemplate
{
    public void Solve()
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
        // Your solution here
        return false;
    }
}
