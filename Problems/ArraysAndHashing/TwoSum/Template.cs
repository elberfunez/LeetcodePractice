namespace LeetcodePractice.Problems.ArraysAndHashing;

// Template - Do not inherit from ProblemBase. To use: rename class to TwoSum and add : ProblemBase
public class TwoSumTemplate
{
    public void Solve()
    {
        Test("Example 1", [3, 4, 5, 6], 7, [0, 1]);
        Test("Example 2", [4, 5, 6], 10, [0, 2]);
        Test("Example 3", [5, 5], 10, [0, 1]);
    }

    private void Test(string name, int[] nums, int target, int[] expected)
    {
        int[] result = TwoSumMethod(nums, target);
        bool passed = result.SequenceEqual(expected);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    nums=[{string.Join(", ", nums)}], target={target}");
        Console.WriteLine($"  Output:   [{string.Join(", ", result)}]");
        if (!passed) Console.WriteLine($"  Expected: [{string.Join(", ", expected)}]");
        Console.WriteLine();
    }

    private int[] TwoSumMethod(int[] nums, int target)
    {
        // Your solution here
        return [];
    }
}
