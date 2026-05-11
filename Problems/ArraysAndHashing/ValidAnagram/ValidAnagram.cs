namespace LeetcodePractice.Problems.ArraysAndHashing;

// Problem statement: ValidAnagram.md
[Problem(242, "Valid Anagram", Category.ArraysAndHashing, Difficulty.Easy)]
public class ValidAnagram : ProblemBase
{
    protected override void Solve()
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
        if (s.Length != t.Length) return false;
        Dictionary<char, int> freq = new(); // <character, count>
        foreach (char c in s)
        {
            if (!freq.ContainsKey(c))
            {
                freq[c] = 1;
            }
            else
            {
                freq[c]++;
            } 
        }
        foreach (char c in t)
        {
            if (!freq.ContainsKey(c)) return false; // no way it could be anagram
            else freq[c]--;

            if (freq[c] < 0) return false; // no way it could be anagram
        }
        return true;
    }
}
