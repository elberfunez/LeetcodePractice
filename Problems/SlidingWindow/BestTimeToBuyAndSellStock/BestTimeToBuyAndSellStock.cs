namespace LeetcodePractice.Problems.SlidingWindow;

// Problem statement: BestTimeToBuyAndSellStock.md
[Problem(121, "Best Time to Buy and Sell Stock", Category.SlidingWindow, Difficulty.Easy)]
public class BestTimeToBuyAndSellStock : ProblemBase
{
    protected override void Solve()
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
        int minPrice = prices[0];
        int maxPrice = 0;
        foreach (int p in prices)
        {
            minPrice = Math.Min(minPrice, p);
            maxPrice = Math.Max(maxPrice, p - minPrice);
        }
        return maxPrice;
    }
}
