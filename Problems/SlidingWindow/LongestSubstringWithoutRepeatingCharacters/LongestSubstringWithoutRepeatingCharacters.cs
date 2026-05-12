namespace LeetcodePractice.Problems.SlidingWindow;

// Problem statement: LongestSubstringWithoutRepeatingCharacters.md
[Problem(3, "Longest Substring Without Repeating Characters", Category.SlidingWindow, Difficulty.Medium)]
public class LongestSubstringWithoutRepeatingCharacters : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", "zxyzxyz", 3);
        Test("Example 2", "xxxx", 1);
    }

    private void Test(string name, string s, int expected)
    {
        int result = LengthOfLongestSubstringSolution(s);

        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    \"{s}\"");
        if (!passed) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    public int LengthOfLongestSubstringSolution(string s)
    {
        var charSet = new HashSet<char>();
        int l = 0;
        int maxLen = 0;
        for (int r = 0; r < s.Length; r++)
        {
            while (charSet.Contains(s[r]))
            {
                charSet.Remove(s[l]);
                l++;
            }
            charSet.Add(s[r]);

            maxLen = Math.Max(maxLen, r - l + 1);
        }
        return maxLen;
    }
}
