namespace LeetcodePractice.Problems.ArraysAndHashing;

// Problem statement: ContainsDuplicate.md
[Problem(217, "Contains Duplicate", Category.ArraysAndHashing, Difficulty.Easy)]
public class ContainsDuplicate : ProblemBase
{
    protected override void Solve()
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
        if (nums.Length < 2) return false;
        HashSet<int> seen = new();
        foreach (int num in nums)
        {
            if (!seen.Add(num)) 
            {
                return true;
            }
        }
        return false;
    }
}
