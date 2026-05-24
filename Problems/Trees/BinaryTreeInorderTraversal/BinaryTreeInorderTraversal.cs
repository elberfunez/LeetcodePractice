namespace LeetcodePractice.Problems.Trees;

// Problem statement: BinaryTreeInorderTraversal.md
[Problem(94, "Binary Tree Inorder Traversal", Category.Trees, Difficulty.Easy)]
public class BinaryTreeInorderTraversal : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", TreeHelper.CreateTree([1, 2, 3, 4, 5, 6, 7]),
            new List<int> { 4, 2, 5, 1, 6, 3, 7 });

        Test("Example 2", TreeHelper.CreateTree([1, 2, 3, null, 4, 5, null]),
            new List<int> { 2, 4, 1, 5, 3 });

        Test("Example 3", TreeHelper.CreateTree([]),
            new List<int> { });
    }

    private void Test(string name, TreeNode root, List<int> expected)
    {
        List<int> result = InorderTraversalSolution(root);

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

    public List<int> InorderTraversalSolution(TreeNode root)
    {
        List<int> result = new();

        Traverse(root, result);

        return result;
    }

    private void Traverse(TreeNode node, List<int> res)
    {
        // base case
        if (node == null) return;
        
        // 1. Left side
        Traverse(node.left, res);
        // 2. Root
        res.Add(node.val);
        // 3. Right side
        Traverse(node.right, res);
    }
}
