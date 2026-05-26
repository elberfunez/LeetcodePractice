using System.Text;

namespace LeetcodePractice.Problems.ArraysAndHashing;

// Problem statement: EncodeDecodeStrings.md
[Problem(271, "Encode and Decode Strings", Category.ArraysAndHashing, Difficulty.Medium)]
public class EncodeDecodeStrings : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1: Multiple strings", () =>
        {
            var solution = new Solution();
            var input = new List<string> { "Hello", "World" };
            var encoded = solution.Encode(input);
            var decoded = solution.Decode(encoded);
            AssertListEqual(decoded, input);
        });

        Test("Example 2: Single empty string", () =>
        {
            var solution = new Solution();
            var input = new List<string> { "" };
            var encoded = solution.Encode(input);
            var decoded = solution.Decode(encoded);
            AssertListEqual(decoded, input);
        });

        Test("Edge case: Empty list", () =>
        {
            var solution = new Solution();
            var input = new List<string>();
            var encoded = solution.Encode(input);
            var decoded = solution.Decode(encoded);
            AssertListEqual(decoded, input);
        });

        Test("Edge case: Strings with special characters", () =>
        {
            var solution = new Solution();
            var input = new List<string> { "!@#$%", "abc#def", "" };
            var encoded = solution.Encode(input);
            var decoded = solution.Decode(encoded);
            AssertListEqual(decoded, input);
        });
    }

    private void Test(string name, Action testLogic)
    {
        try
        {
            testLogic();
            Console.WriteLine($"{name}: ✓");
        }
        catch (AssertionException ex)
        {
            Console.WriteLine($"{name}: ✗ - {ex.Message}");
        }
    }

    private void AssertListEqual(List<string> actual, IList<string> expected)
    {
        if (actual.Count != expected.Count)
            throw new AssertionException($"Count mismatch: expected {expected.Count}, got {actual.Count}");

        for (int i = 0; i < actual.Count; i++)
        {
            if (actual[i] != expected[i])
                throw new AssertionException($"Element {i} mismatch: expected \"{expected[i]}\", got \"{actual[i]}\"");
        }
    }

    private class AssertionException : Exception
    {
        public AssertionException(string message) : base(message) { }
    }

    public class Solution
    {
        public string Encode(IList<string> strs)
        {
            StringBuilder sb = new();
            foreach (string s in strs)
            {
                sb.Append($"{s.Length}#{s}");
            }
            return sb.ToString();
            // 5#Hello5#World
        }

        public List<string> Decode(string s)
        {
            List<string> res = new();
            int i = 0;
            while (i < s.Length)
            {
                int delim = s.IndexOf("#", i);
                int len = int.Parse(s.Substring(i, delim - i)); // substring is (Where to start, How many characters to take)
                i = delim + 1;
                res.Add(s.Substring(i, len));
                i += len;
            }

            return res;
        }
    }
}
