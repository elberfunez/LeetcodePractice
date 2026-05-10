namespace LeetcodePractice.Problems.TwoPointers;

// Template - Do not inherit from ProblemBase. To use: rename class to ContainerWithMostWater and add : ProblemBase
public class ContainerWithMostWaterTemplate
{
    public void Solve()
    {
        Test("Example 1", [1, 7, 2, 5, 4, 7, 3, 6], 36);
        Test("Example 2", [2, 2, 2], 4);
        Test("Example 3", [1, 2], 1);
    }

    private void Test(string name, int[] height, int expected)
    {
        int result = MaxAreaSolution(height);
        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    [{string.Join(", ", height)}]");
        Console.WriteLine($"  Output:   {result}");
        if (!passed) Console.WriteLine($"  Expected: {expected}");
        Console.WriteLine();
    }

    private int MaxAreaSolution(int[] height)
    {
        // Your solution here
        return 0;
    }
}
