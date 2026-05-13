namespace LeetcodePractice.Problems.SlidingWindow;

// Problem statement: MinimumWindowSubstring.md
[Problem(76, "Minimum Window Substring", Category.SlidingWindow, Difficulty.Hard)]
public class MinimumWindowSubstring : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", "OUZODYXAZV", "XYZ", "YXAZ");
        Test("Example 2", "xyz", "xyz", "xyz");
        Test("Example 3", "x", "xy", "");
    }

    private void Test(string name, string s, string t, string expected)
    {
        string result = MinWindowSolution(s, t);

        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    s = \"{s}\", t = \"{t}\"");
        if (!passed) Console.WriteLine($"  Expected: \"{expected}\", Got: \"{result}\"");
        Console.WriteLine();
    }

    public string MinWindowSolution(string s, string t)
    {
        // Your solution here
        return "";
    }
}
