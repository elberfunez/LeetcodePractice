namespace LeetcodePractice.Problems.Trees;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class KthSmallestIntegerInBSTTemplate
{
    public void Solve()
    {
        Test("Example 1", TreeHelper.CreateTree([2, 1, 3]), 1, 1);
        Test("Example 2", TreeHelper.CreateTree([4, 3, 5, 2, null]), 4, 5);
    }

    private void Test(string name, TreeNode root, int k, int expected)
    {
        int result = KthSmallestSolution(root, k);

        Console.WriteLine($"{name}: {(result == expected ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {TreeHelper.TreeToString(root)}, k = {k}");
        if (result != expected) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    public int KthSmallestSolution(TreeNode root, int k)
    {
        // Your solution here
        return -1;
    }
}
