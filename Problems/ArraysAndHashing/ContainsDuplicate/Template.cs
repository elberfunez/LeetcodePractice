namespace LeetcodePractice.Problems.ArraysAndHashing;

// Template - Do not inherit from ProblemBase. To use: rename class to ContainsDuplicate and add : ProblemBase
public class ContainsDuplicateTemplate
{
    public void Solve()
    {
        Test("Example 1", [1, 2, 3, 3], true);
        Test("Example 2", [1, 2, 3, 4], false);
        Test("Example 3", [], false);
    }

    private void Test(string name, int[] nums, bool expected)
    {
        bool result = HasDuplicateSolution(nums);
        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    [{string.Join(", ", nums)}]");
        Console.WriteLine($"  Output:   {result}");
        if (!passed) Console.WriteLine($"  Expected: {expected}");
        Console.WriteLine();
    }

    private bool HasDuplicateSolution(int[] nums)
    {
        // Your solution here
        return false;
    }
}
