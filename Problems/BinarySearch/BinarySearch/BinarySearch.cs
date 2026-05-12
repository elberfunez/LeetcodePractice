namespace LeetcodePractice.Problems.BinarySearch;

// Problem statement: BinarySearch.md
[Problem(704, "Binary Search", Category.BinarySearch, Difficulty.Easy)]
public class BinarySearch : ProblemBase
{
    protected override void Solve()
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
        int l = 0;
        int r = nums.Length - 1;
        
        while (l <= r) {
            int m = l + (r - l) / 2;
            
            if (nums[m] == target) {
                return m;
            } else if (target > nums[m]) {
                l = m + 1;
            } else {
                r = m - 1;
            }
        }
        
        return -1;
    }
}
