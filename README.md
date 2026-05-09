# LeetCode Practice

A personal LeetCode practice project for solving problems in C# .NET 10 with full debugging support.

## Quick Start

Run the project and select a problem from the menu:

```bash
dotnet run
```

The console menu displays all available problems organized by topic, with difficulty tags.

## Adding a New Problem

1. **Choose a category** — Create your file in `Problems/{Category}/`
   
2. **Create the class file** — Name it `P{number:4digits}_{Title}.cs`
   ```csharp
   namespace LeetcodePractice.Problems.ArraysAndHashing;

   [Problem(1, "Two Sum", Category.ArraysAndHashing, Difficulty.Easy)]
   public class P0001_TwoSum : ProblemBase
   {
       protected override void Solve()
       {
           Test("Example 1", [2, 7, 11, 15], 9, [0, 1]);
           Test("Example 2", [3, 2, 4], 6, [1, 2]);
           Test("Example 3", [3, 3], 6, [0, 1]);
       }

       private void Test(string name, int[] nums, int target, int[] expected)
       {
           int[] result = TwoSum(nums, target);
           bool passed = result.SequenceEqual(expected);
           Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
           Console.WriteLine($"  Input:    nums=[{string.Join(", ", nums)}], target={target}");
           Console.WriteLine($"  Output:   [{string.Join(", ", result)}]");
           if (!passed) Console.WriteLine($"  Expected: [{string.Join(", ", expected)}]");
           Console.WriteLine();
       }

       private int[] TwoSum(int[] nums, int target)
       {
           var seen = new Dictionary<int, int>();
           for (int i = 0; i < nums.Length; i++)
           {
               if (seen.TryGetValue(target - nums[i], out int j))
                   return [j, i];
               seen[nums[i]] = i;
           }
           return [];
       }
   }
   ```
   
   **Key points:**
   - Inherit from `ProblemBase` (not `IProblem`)
   - Override `Solve()` instead of implementing `Run()`
   - Create a `Test()` method for your problem type
   - Run multiple test cases and see `✓ PASS`/`✗ FAIL` status

3. **Build and run** — The problem automatically appears in the menu:
   ```bash
   dotnet build
   dotnet run
   ```

## Debugging

Set breakpoints in your solution method and debug normally:

1. Open the `.cs` file
2. Click the gutter to set a breakpoint inside your solution method
3. Run the project in Debug mode (F5 in Rider/VS)
4. Select your problem from the menu
5. The breakpoint is hit — step through, inspect locals, etc.

## Categories

Problems are organized by topic following the [NeetCode 150](https://neetcode.io/practice) roadmap:

- Arrays & Hashing
- Two Pointers
- Stack
- Binary Search
- Sliding Window
- Linked List
- Trees
- Tries
- Backtracking
- Heap / Priority Queue
- Graphs
- 1-D DP
- Intervals
- Greedy
- Advanced Graphs
- 2-D DP
- Bit Manipulation
- Math & Geometry

## Difficulty Tags

- `[E]` — Easy
- `[M]` — Medium
- `[H]` — Hard

## Problem Log

| # | Title | Category | Difficulty | Date |
|---|-------|----------|------------|------|
| 1 | Two Sum | Arrays & Hashing | Easy | — |
