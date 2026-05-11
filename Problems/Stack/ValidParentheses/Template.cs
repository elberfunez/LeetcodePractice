namespace LeetcodePractice.Problems.Stack;

// Template - Do not inherit from ProblemBase. To use: rename class to ValidParentheses and add : ProblemBase
public class ValidParenthesesTemplate
{
    public void Solve()
    {
        Test("Example 1", "[]", true);
        Test("Example 2", "([{}])", true);
        Test("Example 3", "[(])", false);
    }

    private void Test(string name, string s, bool expected)
    {
        bool result = IsValidSolution(s);
        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    \"{s}\"");
        Console.WriteLine($"  Output:   {result}");
        if (!passed) Console.WriteLine($"  Expected: {expected}");
        Console.WriteLine();
    }

    private bool IsValidSolution(string s)
    {
        // Your solution here
        return false;
    }
}
