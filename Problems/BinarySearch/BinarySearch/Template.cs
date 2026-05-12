namespace LeetcodePractice.Problems.BinarySearch;

// Template - Do not inherit from ProblemBase. To use: rename class to BinarySearch and add : ProblemBase
public class BinarySearchTemplate
{
    public void Solve()
    {
        Test("Example 1", [-1, 0, 2, 4, 6, 8], 4, 3);
        Test("Example 2", [-1, 0, 2, 4, 6, 8], 3, -1);
    }

    private void Test(string name, int[] nums, int target, int expected)
    {
        int result = SearchSolution(nums, target);

        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    nums = [{string.Join(", ", nums)}], target = {target}");
        if (!passed) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    public int SearchSolution(int[] nums, int target)
    {
        // Your solution here
        return -1;
    }
}
