namespace LeetcodePractice.Problems.ArraysAndHashing;

[Problem(1, "Two Sum", Category.ArraysAndHashing, Difficulty.Easy)]
public class P0001_TwoSum : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", [2, 7, 11, 15], 9, [0, 1]);
        Test("Example 2", [3, 2, 4], 6, [1, 2]);
        Test("Example 3", [3, 3], 6, [0, 1]);
    }

    private void Test(string name, int[] nums, int target, int[] expected)
    {
        int[] result = TwoSum(nums, target);
        bool passed = result.SequenceEqual(expected);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    nums=[{string.Join(", ", nums)}], target={target}");
        Console.WriteLine($"  Output:   [{string.Join(", ", result)}]");
        if (!passed) Console.WriteLine($"  Expected: [{string.Join(", ", expected)}]");
        Console.WriteLine();
    }

    private int[] TwoSum(int[] nums, int target)
    {
        var seen = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (seen.TryGetValue(target - nums[i], out int j))
                return [j, i];
            seen[nums[i]] = i;
        }
        return [];
    }
}
