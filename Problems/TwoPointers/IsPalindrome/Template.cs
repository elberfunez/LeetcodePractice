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
        // Implement test logic here
    }

    private bool ValidPalindrome(string s)
    {
        // Your solution here
        return false;
    }
}
