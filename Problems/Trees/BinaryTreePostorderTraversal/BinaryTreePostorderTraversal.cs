namespace LeetcodePractice.Problems.Trees;

// Problem statement: BinaryTreePostorderTraversal.md
[Problem(145, "Binary Tree Postorder Traversal", Category.Trees, Difficulty.Easy)]
public class BinaryTreePostorderTraversal : ProblemBase
{
    private PostorderRecursive recursiveSolution = new PostorderRecursive();
    private PostorderIterative iterativeSolution = new PostorderIterative();

    protected override void Solve()
    {
        var testCases = new[]
        {
            ("Example 1", TreeHelper.CreateTree([1, 2, 3, 4, 5, 6, 7]), new List<int> { 4, 5, 2, 6, 7, 3, 1 }),
            ("Example 2", TreeHelper.CreateTree([1, 2, 3, null, 4, 5, null]), new List<int> { 4, 2, 5, 3, 1 }),
            ("Example 3", TreeHelper.CreateTree([]), new List<int> { }),
        };

        Console.WriteLine("=== Recursive Solution ===");
        foreach (var (name, root, expected) in testCases)
        {
            Test(name, root, expected, recursiveSolution.Postorder);
        }

        Console.WriteLine("\n=== Iterative Solution ===");
        foreach (var (name, root, expected) in testCases)
        {
            Test(name, root, expected, iterativeSolution.Postorder);
        }
    }

    private void Test(string name, TreeNode root, List<int> expected, Func<TreeNode, List<int>> solution)
    {
        List<int> result = solution(root);

        bool passed = AreListsEqual(result, expected);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {TreeHelper.TreeToString(root)}");
        if (!passed)
        {
            Console.WriteLine($"  Expected: [{string.Join(", ", expected)}]");
            Console.WriteLine($"  Got:      [{string.Join(", ", result)}]");
        }
        Console.WriteLine();
    }

    private bool AreListsEqual(List<int> a, List<int> b)
    {
        if (a.Count != b.Count) return false;
        for (int i = 0; i < a.Count; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }
}
