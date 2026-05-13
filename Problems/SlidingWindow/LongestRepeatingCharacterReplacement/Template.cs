namespace LeetcodePractice.Problems.SlidingWindow;

// Template - Do not inherit from ProblemBase. To use: rename class to LongestRepeatingCharacterReplacement and add : ProblemBase
public class LongestRepeatingCharacterReplacementTemplate
{
    public void Solve()
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
        // Your solution here
        return 0;
    }
}
