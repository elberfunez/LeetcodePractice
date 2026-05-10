namespace LeetcodePractice.Problems.TwoPointers;

// Template - Do not inherit from ProblemBase. To use: rename class to ThreeSum and add : ProblemBase
public class ThreeSumTemplate
{
    public void Solve()
    {
        Test("Example 1", [-1, 0, 1, 2, -1, -4], [[-1, -1, 2], [-1, 0, 1]]);
        Test("Example 2", [0, 1, 1], []);
        Test("Example 3", [0, 0, 0], [[0, 0, 0]]);
    }

    private void Test(string name, int[] nums, int[][] expected)
    {
        List<List<int>> result = ThreeSumSolution(nums);

        // Convert to sorted lists for comparison
        var resultSorted = result.Select(x => string.Join(",", x.OrderBy(n => n))).OrderBy(x => x).ToList();
        var expectedSorted = expected.Select(x => string.Join(",", x.OrderBy(n => n))).OrderBy(x => x).ToList();

        bool passed = resultSorted.SequenceEqual(expectedSorted);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    [{string.Join(", ", nums)}]");
        Console.WriteLine($"  Output:   {FormatOutput(result)}");
        if (!passed) Console.WriteLine($"  Expected: {FormatOutput(expected.Select(x => new List<int>(x)).ToList())}");
        Console.WriteLine();
    }

    private string FormatOutput(List<List<int>> triplets)
    {
        return "[" + string.Join(", ", triplets.Select(t => "[" + string.Join(", ", t) + "]")) + "]";
    }

    private List<List<int>> ThreeSumSolution(int[] nums)
    {
        // Your solution here
        return new List<List<int>>();
    }
}
