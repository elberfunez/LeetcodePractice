namespace LeetcodePractice.Problems.Trees;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class LowestCommonAncestorBSTTemplate
{
    public void Solve()
    {
        TreeNode root1 = TreeHelper.CreateTree([5, 3, 8, 1, 4, 7, 9, null, 2]);
        TreeNode p1 = FindNode(root1, 3);
        TreeNode q1 = FindNode(root1, 8);
        Test("Example 1", root1, p1, q1, 5);

        TreeNode root2 = TreeHelper.CreateTree([5, 3, 8, 1, 4, 7, 9, null, 2]);
        TreeNode p2 = FindNode(root2, 3);
        TreeNode q2 = FindNode(root2, 4);
        Test("Example 2", root2, p2, q2, 3);
    }

    private void Test(string name, TreeNode root, TreeNode p, TreeNode q, int expectedVal)
    {
        TreeNode result = LowestCommonAncestorSolution(root, p, q);

        bool passed = result != null && result.val == expectedVal;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    root = {TreeHelper.TreeToString(root)}, p = {p.val}, q = {q.val}");
        if (!passed) Console.WriteLine($"  Expected: {expectedVal}, Got: {(result != null ? result.val : "null")}");
        Console.WriteLine();
    }

    private TreeNode FindNode(TreeNode root, int val)
    {
        if (root == null) return null;
        if (root.val == val) return root;

        TreeNode left = FindNode(root.left, val);
        if (left != null) return left;

        return FindNode(root.right, val);
    }

    public TreeNode LowestCommonAncestorSolution(TreeNode root, TreeNode p, TreeNode q)
    {
        // Your solution here
        return root;
    }
}
