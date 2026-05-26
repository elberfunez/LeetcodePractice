namespace LeetcodePractice.Problems.ArraysAndHashing;

// Problem statement: LongestConsecutive.md
[Problem(128, "Longest Consecutive Sequence", Category.ArraysAndHashing, Difficulty.Hard)]
public class LongestConsecutive : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1: Unordered sequence", () =>
        {
            var solution = new Solution();
            var input = new int[] { 2, 20, 4, 10, 3, 4, 5 };
            var result = solution.LongestConsecutive(input);
            Assert(result, 4, "longest sequence [2,3,4,5]");
        });

        Test("Example 2: Multiple consecutive elements", () =>
        {
            var solution = new Solution();
            var input = new int[] { 0, 3, 2, 5, 4, 6, 1, 1 };
            var result = solution.LongestConsecutive(input);
            Assert(result, 7, "longest sequence [0,1,2,3,4,5,6]");
        });

        Test("Edge case: Empty array", () =>
        {
            var solution = new Solution();
            var input = new int[] { };
            var result = solution.LongestConsecutive(input);
            Assert(result, 0, "no elements");
        });

        Test("Edge case: Single element", () =>
        {
            var solution = new Solution();
            var input = new int[] { 42 };
            var result = solution.LongestConsecutive(input);
            Assert(result, 1, "single element");
        });

        Test("Edge case: All duplicates", () =>
        {
            var solution = new Solution();
            var input = new int[] { 5, 5, 5, 5 };
            var result = solution.LongestConsecutive(input);
            Assert(result, 1, "only one unique value");
        });

        Test("Edge case: Negative numbers", () =>
        {
            var solution = new Solution();
            var input = new int[] { -3, -1, -2, 0, 1 };
            var result = solution.LongestConsecutive(input);
            Assert(result, 5, "sequence [-3,-2,-1,0,1]");
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

    private void Assert(int actual, int expected, string context)
    {
        if (actual != expected)
            throw new AssertionException($"{context} — expected {expected}, got {actual}");
    }

    private class AssertionException : Exception
    {
        public AssertionException(string message) : base(message) { }
    }

    public class Solution
    {
        public int LongestConsecutive(int[] nums)
        {
            // store the nums in a HashSet (get rid of dups)
            // Find the start of a sequence (has no left neighbor)
            HashSet<int> hs = new(nums); // shorthand for creating hashset, same as for loop. JUST LEARNING THIS hehe
            int counter = 0;
            foreach (int num in hs)
            {
                bool isBegOfSeq = !hs.Contains(num - 1); // has no left neighbor
                if (isBegOfSeq)
                {
                    int seqCounter = 1;
                    int curNum = num; // iterator to move to next seq num
                    while (hs.Contains(curNum + 1))
                    {
                        seqCounter++;
                        curNum = curNum + 1;
                    }
                    counter = Math.Max(counter, seqCounter);
                }
            }

            return counter;

        }
    }
}
