namespace LeetcodePractice.Problems.TwoPointers;

// Template - Do not inherit from ProblemBase. To use: rename class to TwoIntegerSumII and add : ProblemBase
public class TwoIntegerSumIITemplate
{
    public void Solve()
    {
        Test("Example 1", [1, 2, 3, 4], 3, [1, 2]);
        Test("Example 2", [2, 7, 11, 15], 9, [1, 2]);
        Test("Example 3", [-1, 0], -1, [1, 2]);
    }

    private void Test(string name, int[] numbers, int target, int[] expected)
    {
        int[] result = TwoSumSolution(numbers, target);
        bool passed = result.SequenceEqual(expected);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    numbers=[{string.Join(", ", numbers)}], target={target}");
        Console.WriteLine($"  Output:   [{string.Join(", ", result)}]");
        if (!passed) Console.WriteLine($"  Expected: [{string.Join(", ", expected)}]");
        Console.WriteLine();
    }

    private int[] TwoSumSolution(int[] numbers, int target)
    {
        // Your solution here
        return [];
    }
}
