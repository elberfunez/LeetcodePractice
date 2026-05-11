namespace LeetcodePractice.Problems.ArraysAndHashing;

// Template - Do not inherit from ProblemBase. To use: rename class to ValidAnagram and add : ProblemBase
public class ValidAnagramTemplate
{
    public void Solve()
    {
        Test("Example 1", "racecar", "carrace", true);
        Test("Example 2", "jar", "jam", false);
        Test("Example 3", "a", "a", true);
    }

    private void Test(string name, string s, string t, bool expected)
    {
        bool result = IsAnagramSolution(s, t);
        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    s=\"{s}\", t=\"{t}\"");
        Console.WriteLine($"  Output:   {result}");
        if (!passed) Console.WriteLine($"  Expected: {expected}");
        Console.WriteLine();
    }

    private bool IsAnagramSolution(string s, string t)
    {
        // Your solution here
        return false;
    }
}
