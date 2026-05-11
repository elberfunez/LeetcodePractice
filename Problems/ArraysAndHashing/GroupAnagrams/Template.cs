namespace LeetcodePractice.Problems.ArraysAndHashing;

// Template - Do not inherit from ProblemBase. To use: rename class to GroupAnagrams and add : ProblemBase
public class GroupAnagramsTemplate
{
    public void Solve()
    {
        Test("Example 1", ["act", "pots", "tops", "cat", "stop", "hat"], [["hat"], ["act", "cat"], ["stop", "pots", "tops"]]);
        Test("Example 2", ["x"], [["x"]]);
        Test("Example 3", [""], [[""]]);
    }

    private void Test(string name, string[] strs, string[][] expected)
    {
        List<List<string>> result = GroupAnagramsSolution(strs);

        // Convert to sorted format for comparison
        var resultSorted = result.Select(g => string.Join(",", g.OrderBy(s => s))).OrderBy(x => x).ToList();
        var expectedSorted = expected.Select(g => string.Join(",", g.OrderBy(s => s))).OrderBy(x => x).ToList();

        bool passed = resultSorted.SequenceEqual(expectedSorted);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    [{string.Join(", ", strs.Select(s => $"\"{s}\""))}]");
        if (!passed) Console.WriteLine($"  Expected: {FormatOutput(expected)}");
        Console.WriteLine();
    }

    private string FormatOutput(string[][] groups)
    {
        return "[" + string.Join(", ", groups.Select(g => "[" + string.Join(", ", g.Select(s => $"\"{s}\"")) + "]")) + "]";
    }

    public List<List<string>> GroupAnagramsSolution(string[] strs)
    {
        // Your solution here
        return new List<List<string>>();
    }
}
