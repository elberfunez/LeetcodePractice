namespace LeetcodePractice.Problems.TwoPointers;

// Problem statement: IsPalindrome.md
[Problem(125, "Valid Palindrome", Category.TwoPointers, Difficulty.Easy)]
public class IsPalindrome : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", "Was it a car or a cat I saw?", true);
        Test("Example 2", "tab a cat", false);
    }

    private void Test(string name, string s, bool expected)
    {
        // Implement test logic here
        bool isPassed = ValidPalindrome(s);
    }

    private bool ValidPalindrome(string s)
    {
        int l = 0;
        int r = s.Length - 1;
        while (l <= r)
        {
            if (!char.IsLetterOrDigit(s[l]))
            {
                l++;
            }
            if (!char.IsLetterOrDigit(s[r]))
            {
                r--;
            }
            char left = char.ToLower(s[l]);
            char right = char.ToLower(s[r]);
            if (left != right)
            {
                return false;
            }
            else
            {
                l++;
                r--;
            }
        }

        return true;
    }
}
