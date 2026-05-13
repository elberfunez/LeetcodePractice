namespace LeetcodePractice.Problems.SlidingWindow;

// Problem statement: LongestRepeatingCharacterReplacement.md
[Problem(424, "Longest Repeating Character Replacement", Category.SlidingWindow, Difficulty.Medium)]
public class LongestRepeatingCharacterReplacement : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", "XYYX", 2, 4);
        Test("Example 2", "AAABABB", 1, 5);
    }

    private void Test(string name, string s, int k, int expected)
    {
        int result = CharacterReplacementSolution(s, k);

        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    s = \"{s}\", k = {k}");
        if (!passed) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    public int CharacterReplacementSolution(string s, int k)
    {
        Dictionary<char, int> d = new();
        int l = 0;
        int result = 0;
        for (int r = 0; r < s.Length; r++)
        {
            // Step 1: add it to the dictionary <char, freq>
            if (!d.ContainsKey(s[r]))
            {
                d[s[r]] = 1;
            }
            else
            {
                d[s[r]]++;
            }
            // Step 2: well take the window length and minus the max seen so far
            while ((r - l + 1) - d.Values.Max() > k) // check if the difference is greater than k
            {
                d[s[l]]--; // decrement the current count of that letter since were about to remove it from the window
                l++;
            }
            // Step 3: Update the result
            result = Math.Max(result, (r - l + 1));
        }

        return result;
    }
}
