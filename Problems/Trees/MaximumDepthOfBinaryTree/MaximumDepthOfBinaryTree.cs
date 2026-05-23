namespace LeetcodePractice.Problems.Trees;

// Problem statement: MaximumDepthOfBinaryTree.md
[Problem(104, "Maximum Depth of Binary Tree", Category.Trees, Difficulty.Easy)]
public class MaximumDepthOfBinaryTree : ProblemBase
{
    private BFS bfsSolution = new BFS();
    private DFS dfsSolution = new DFS();

    protected override void Solve()
    {
        var testCases = new[]
        {
            ("Example 1", TreeHelper.CreateTree([1, 2, 3, null, null, 4]), 3),
            ("Example 2", TreeHelper.CreateTree([]), 0),
        };

        Console.WriteLine("=== BFS Solution ===");
        foreach (var (name, root, expected) in testCases)
        {
            Test(name, root, expected, bfsSolution.MaxDepth);
        }

        Console.WriteLine("\n=== DFS Solution ===");
        foreach (var (name, root, expected) in testCases)
        {
            Test(name, root, expected, dfsSolution.MaxDepth);
        }
    }

    private void Test(string name, TreeNode root, int expected, Func<TreeNode, int> solution)
    {
        int result = solution(root);

        Console.WriteLine($"{name}: {(result == expected ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {TreeHelper.TreeToString(root)}");
        if (result != expected) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }
}
