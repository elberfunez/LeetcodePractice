namespace LeetcodePractice.Problems.SlidingWindow;

// Template - Do not inherit from ProblemBase. To use: rename class to BestTimeToBuyAndSellStock and add : ProblemBase
public class BestTimeToBuyAndSellStockTemplate
{
    public void Solve()
    {
        Test("Example 1", [10, 1, 5, 6, 7, 1], 6);
        Test("Example 2", [10, 8, 7, 5, 2], 0);
    }

    private void Test(string name, int[] prices, int expected)
    {
        int result = MaxProfitSolution(prices);

        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    [{string.Join(", ", prices)}]");
        if (!passed) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    public int MaxProfitSolution(int[] prices)
    {
        // Your solution here
        return 0;
    }
}
