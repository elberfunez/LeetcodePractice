namespace LeetcodePractice.Problems.ArraysAndHashing;

// Template - Do not inherit from ProblemBase. To use: rename class to TopKFrequentElements and add : ProblemBase
public class TopKFrequentElementsTemplate
{
    public void Solve()
    {
        Test("Example 1", [1, 2, 2, 3, 3, 3], 2, [2, 3]);
        Test("Example 2", [7, 7], 1, [7]);
    }

    private void Test(string name, int[] nums, int k, int[] expected)
    {
        int[] result = TopKFrequentSolution(nums, k);

        // Convert to sorted sets for comparison (order doesn't matter)
        var resultSet = new SortedSet<int>(result);
        var expectedSet = new SortedSet<int>(expected);

        bool passed = resultSet.SequenceEqual(expectedSet);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    nums = [{string.Join(", ", nums)}], k = {k}");
        if (!passed) Console.WriteLine($"  Expected: [{string.Join(", ", expected)}]");
        Console.WriteLine();
    }

    public int[] TopKFrequentSolution(int[] nums, int k)
    {
        // Your solution here
        return [];
    }
}
