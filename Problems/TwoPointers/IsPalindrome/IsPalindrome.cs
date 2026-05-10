namespace LeetcodePractice.Problems.TwoPointers;

// Problem statement: IsPalindrome.md
[Problem(125, "Valid Palindrome", Category.TwoPointers, Difficulty.Easy)]
public class IsPalindrome : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", "Was it a car or a cat I saw?", true);
        Test("Example 2", "tab a cat", false);
        Test("Example 3", "No lemon, no melon", true);
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
        int l = 0;
        int r = s.Length - 1;

        while (l < r)
        {
            while (l < r && !char.IsLetterOrDigit(s[l])) l++;
            while (l < r && !char.IsLetterOrDigit(s[r])) r--;

            if (char.ToLower(s[l]) != char.ToLower(s[r]))
            {
                return false;
            }

            l++;
            r--;
        }

        return true;
    }
}
