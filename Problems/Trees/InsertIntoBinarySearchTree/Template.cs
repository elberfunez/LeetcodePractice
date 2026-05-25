namespace LeetcodePractice.Problems.Trees;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class InsertIntoBinarySearchTreeTemplate
{
    public void Solve()
    {
        Test("Example 1", TreeHelper.CreateTree([5, 3, 9, 1, 4]), 6,
            new int?[] { 5, 3, 9, 1, 4, 6 });

        Test("Example 2", TreeHelper.CreateTree([5, 3, 6, null, 4, null, 10, null, null, 7]), 9,
            new int?[] { 5, 3, 6, null, 4, null, 10, null, null, 7, null, null, 9 });
    }

    private void Test(string name, TreeNode root, int val, int?[] expected)
    {
        TreeNode result = InsertIntoBSTSolution(root, val);
        string resultStr = TreeHelper.TreeToString(result);
        string expectedStr = "[" + string.Join(",", expected.Select(x => x?.ToString() ?? "null")) + "]";

        bool passed = resultStr == expectedStr;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    root = {TreeHelper.TreeToString(root)}, val = {val}");
        if (!passed)
        {
            Console.WriteLine($"  Expected: {expectedStr}");
            Console.WriteLine($"  Got:      {resultStr}");
        }
        Console.WriteLine();
    }

    public TreeNode InsertIntoBSTSolution(TreeNode root, int val)
    {
        // Your solution here
        return root;
    }
}
