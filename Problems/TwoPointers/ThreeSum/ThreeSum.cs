namespace LeetcodePractice.Problems.TwoPointers;

// Problem statement: ThreeSum.md
[Problem(15, "3Sum", Category.TwoPointers, Difficulty.Medium)]
public class ThreeSum : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", [-1, 0, 1, 2, -1, -4], [[-1, -1, 2], [-1, 0, 1]]);
        //Test("Example 2", [0, 1, 1], []);
        //Test("Example 3", [0, 0, 0], [[0, 0, 0]]);
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
    private List<List<int>> ThreeSumSolution(int[] nums) {
        if (nums.Length < 3)
        {
            return new List<List<int>>();
        }
        Array.Sort(nums);
        var result = new List<List<int>>();

        for (int i = 0; i < nums.Length - 2; i++)
        {
            // Skip duplicates at start
            if (i > 0 && nums[i] == nums[i - 1]) continue;
            
            int needed = 0 - nums[i];
            int l = i + 1;
            int r = nums.Length - 1;
            while (l < r)
            {
                int sum = nums[l] + nums[r];
                if (sum == needed)
                {
                    result.Add(new List<int> { nums[i], nums[l], nums[r] });
                    // Skip forward duplicates
                    while (l < r && nums[l] == nums[l + 1]) l++;
                    
                    // Skip backward duplicates
                    while (l < r && nums[r] == nums[r - 1]) r--;
                    
                    l++;
                    r--;
                }
                else if (sum < needed)
                {
                    l++;
                }
                else 
                {
                    r--;
                }

            }
        }
        return result;
    }
}
