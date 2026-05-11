namespace LeetcodePractice.Problems.Stack;

// Problem statement: ValidParentheses.md
[Problem(20, "Valid Parentheses", Category.Stack, Difficulty.Easy)]
public class ValidParentheses : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", "[]", true);
        Test("Example 2", "([{}])", true);
        Test("Example 3", "[(])", false);
        Test("Example 4", "]]", false);
        Test("Example 5", "(])", false);
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
        if (s.Length % 2 != 0) return false; // must be even length
        
        var stack = new Stack<char>();
        var pairs = new Dictionary<char, char> {
            { ')', '(' },
            { ']', '[' },
            { '}', '{' }
        };
        
        foreach (char c in s) {
            if (pairs.TryGetValue(c, out char opening)) 
            {
                // closing bracket
                if (stack.Count == 0 || stack.Pop() != opening)
                {
                    return false;
                }
            } 
            else 
            {
                // opening bracket
                stack.Push(c);
            }
        }
        
        return stack.Count == 0;
    }
}
