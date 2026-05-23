namespace LeetcodePractice.Problems.Trees;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class BinaryTreeLevelOrderTraversalTemplate
{
    public void Solve()
    {
        Test("Example 1", TreeHelper.CreateTree([1, 2, 3, 4, 5, 6, 7]),
            new List<List<int>> { new List<int> { 1 }, new List<int> { 2, 3 }, new List<int> { 4, 5, 6, 7 } });

        Test("Example 2", TreeHelper.CreateTree([1]),
            new List<List<int>> { new List<int> { 1 } });

        Test("Example 3", TreeHelper.CreateTree([]),
            new List<List<int>> { });
    }

    private void Test(string name, TreeNode root, List<List<int>> expected)
    {
        List<List<int>> result = LevelOrderSolution(root);

        bool passed = AreListsEqual(result, expected);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {TreeHelper.TreeToString(root)}");
        if (!passed)
        {
            Console.WriteLine($"  Expected: {ListsToString(expected)}");
            Console.WriteLine($"  Got:      {ListsToString(result)}");
        }
        Console.WriteLine();
    }

    private bool AreListsEqual(List<List<int>> a, List<List<int>> b)
    {
        if (a.Count != b.Count) return false;
        for (int i = 0; i < a.Count; i++)
        {
            if (a[i].Count != b[i].Count) return false;
            for (int j = 0; j < a[i].Count; j++)
            {
                if (a[i][j] != b[i][j]) return false;
            }
        }
        return true;
    }

    private string ListsToString(List<List<int>> lists)
    {
        var levels = new List<string>();
        foreach (var list in lists)
        {
            levels.Add("[" + string.Join(", ", list) + "]");
        }
        return "[" + string.Join(", ", levels) + "]";
    }

    public List<List<int>> LevelOrderSolution(TreeNode root)
    {
        // Your solution here
        return new List<List<int>>();
    }
}
