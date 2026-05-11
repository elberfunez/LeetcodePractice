namespace LeetcodePractice.Problems.ArraysAndHashing;

// Problem statement: GroupAnagrams.md
[Problem(49, "Group Anagrams", Category.ArraysAndHashing, Difficulty.Medium)]
public class GroupAnagrams : ProblemBase
{
    protected override void Solve()
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
        var groups = new Dictionary<string, List<string>>();

        foreach (var s in strs) 
        {
            char[] chars = s.ToCharArray();
            Array.Sort(chars);
            string key = new string(chars);
    
            if (!groups.TryGetValue(key, out var group)) 
            {
                group = new List<string>();
                groups[key] = group;
            }
    
            group.Add(s);
        }

        return new List<List<string>>(groups.Values);
    }
}
