namespace LeetcodePractice.Problems.Trees;

// Problem statement: InsertIntoBinarySearchTree.md
[Problem(701, "Insert into a Binary Search Tree", Category.Trees, Difficulty.Medium)]
public class InsertIntoBinarySearchTree : ProblemBase
{
    protected override void Solve()
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
        if (root == null)
        {
            return new TreeNode(val);
        }

        TreeNode cur = root;
        if (val > cur.val)
        {
            cur.right = InsertIntoBSTSolution(cur.right, val);
        }
        else if (val < cur.val)
        {
            cur.left = InsertIntoBSTSolution(cur.left, val);
        }

        return cur;
    }
}
