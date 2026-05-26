namespace LeetcodePractice.Problems.ArraysAndHashing;

// Problem statement: ProductExceptSelf.md
[Problem(238, "Product of Array Except Self", Category.ArraysAndHashing, Difficulty.Medium)]
public class ProductExceptSelf : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1: Classic case", () =>
        {
            var solution = new Solution();
            var input = new int[] { 1, 2, 3, 4 };
            var expected = new int[] { 24, 12, 8, 6 };
            var result = solution.ProductExceptSelf(input);
            AssertArrayEqual(result, expected);
        });
        Test("Example 2: Basic case", () =>
        {
            var solution = new Solution();
            var input = new int[] { 1, 2, 4, 6 };
            var expected = new int[] { 48, 24, 12, 8 };
            var result = solution.ProductExceptSelf(input);
            AssertArrayEqual(result, expected);
        });

        Test("Example 3: Array with zero", () =>
        {
            var solution = new Solution();
            var input = new int[] { -1, 0, 1, 2, 3 };
            var expected = new int[] { 0, -6, 0, 0, 0 };
            var result = solution.ProductExceptSelf(input);
            AssertArrayEqual(result, expected);
        });

        Test("Edge case: Two elements", () =>
        {
            var solution = new Solution();
            var input = new int[] { 1, 2 };
            var expected = new int[] { 2, 1 };
            var result = solution.ProductExceptSelf(input);
            AssertArrayEqual(result, expected);
        });

        Test("Edge case: All negative numbers", () =>
        {
            var solution = new Solution();
            var input = new int[] { -1, -2, -3 };
            var expected = new int[] { -6, -3, -2 };
            var result = solution.ProductExceptSelf(input);
            AssertArrayEqual(result, expected);
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

    private void AssertArrayEqual(int[] actual, int[] expected)
    {
        if (actual.Length != expected.Length)
            throw new AssertionException($"Length mismatch: expected {expected.Length}, got {actual.Length}");

        for (int i = 0; i < actual.Length; i++)
        {
            if (actual[i] != expected[i])
                throw new AssertionException($"Element {i} mismatch: expected {expected[i]}, got {actual[i]}");
        }
    }

    private class AssertionException : Exception
    {
        public AssertionException(string message) : base(message) { }
    }

    public class Solution
    {
        public int[] ProductExceptSelf(int[] nums)
        {
            // Approach: Use prefix and postfix products to avoid division and handle zeros
            // prefix[i] = product of all elements from index 0 to i
            // postfix[i] = product of all elements from index i to end
            // result[i] = prefix[i-1] * postfix[i+1] (all except nums[i])
            // Time: O(n), Space: O(n) for the two helper arrays

            // Step 1: Build prefix array (cumulative product from left)
            int[] prefix = new int[nums.Length];
            prefix[0] = 1 * nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                // Each prefix[i] is the product of all elements up to and including index i
                prefix[i] = prefix[i - 1] * nums[i];
            }

            // Step 2: Build postfix array (cumulative product from right)
            int[] postfix = new int[nums.Length];
            postfix[nums.Length - 1] = 1 * nums[nums.Length - 1];
            for (int i = nums.Length - 2; i >= 0; i--)
            {
                // Each postfix[i] is the product of all elements from index i to end
                postfix[i] = nums[i] * postfix[i + 1];
            }

            // Step 3: Calculate result by combining left and right products
            int[] res = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                // For position i, we need everything BEFORE i (left product) and AFTER i (right product)
                int leftProduct = i > 0 ? prefix[i - 1] : 1; // Edge case: first element has no left
                int rightProduct = i < nums.Length - 1 ? postfix[i + 1] : 1; // Edge case: last element has no right
                res[i] = leftProduct * rightProduct;
            }

            return res;
        }
    }
}
