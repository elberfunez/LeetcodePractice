namespace LeetcodePractice.Problems.TwoPointers;

// Template - Do not inherit from ProblemBase. To use: rename class to IsPalindrome and add : ProblemBase
public class IsPalindromeTemplate
{
    public void Solve()
    {
        Test("Example 1", "Was it a car or a cat I saw?", true);
        Test("Example 2", "tab a cat", false);
    }

    private void Test(string name, string s, bool expected)
    {
        bool result = ValidPalindrome(s);
        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    \"{s}\"");
        Console.WriteLine($"  Output:   {result}");
        if (!passed) Console.WriteLine($"  Expected: {expected}");
        Console.WriteLine();
    }

    private bool ValidPalindrome(string s)
    {
        // Your solution here
        return false;
    }
}
