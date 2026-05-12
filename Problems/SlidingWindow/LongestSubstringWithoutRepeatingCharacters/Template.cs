namespace LeetcodePractice.Problems.SlidingWindow;

// Template - Do not inherit from ProblemBase. To use: rename class to LongestSubstringWithoutRepeatingCharacters and add : ProblemBase
public class LongestSubstringWithoutRepeatingCharactersTemplate
{
    public void Solve()
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
        // Your solution here
        return 0;
    }
}
