namespace LeetcodePractice.Problems.Trees;

// Problem statement: BinaryTreePreorderTraversal.md
[Problem(144, "Binary Tree Preorder Traversal", Category.Trees, Difficulty.Easy)]
public class BinaryTreePreorderTraversal : ProblemBase
{
    private PreorderRecursive recursiveSolution = new PreorderRecursive();
    private PreorderIterative iterativeSolution = new PreorderIterative();

    protected override void Solve()
    {
        var testCases = new[]
        {
            ("Example 1", TreeHelper.CreateTree([1, 2, 3, 4, 5, 6, 7]), new List<int> { 1, 2, 4, 5, 3, 6, 7 }),
            ("Example 2", TreeHelper.CreateTree([1, 2, 3, null, 4, 5, null]), new List<int> { 1, 2, 4, 3, 5 }),
            ("Example 3", TreeHelper.CreateTree([]), new List<int> { }),
        };

        Console.WriteLine("=== Recursive Solution ===");
        foreach (var (name, root, expected) in testCases)
        {
            Test(name, root, expected, recursiveSolution.Preorder);
        }

        Console.WriteLine("\n=== Iterative Solution ===");
        foreach (var (name, root, expected) in testCases)
        {
            Test(name, root, expected, iterativeSolution.Preorder);
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
