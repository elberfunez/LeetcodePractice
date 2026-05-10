# LeetCode Practice

A personal LeetCode practice project for solving problems in C# .NET 10 with full debugging support.

## Quick Start

Run the project and select a problem from the menu:

```bash
dotnet run
```

The console menu displays all available problems organized by topic, with difficulty tags.

## Adding a New Problem

1. **Choose a category** — Create a folder in `Problems/{Category}/{ProblemName}/`

2. **Create four files**:
   
   **{ProblemName}.cs** — Your solution (shows in the menu):
   ```csharp
   namespace LeetcodePractice.Problems.ArraysAndHashing;

   // Problem statement: TwoSum.md
   [Problem(1, "Two Sum", Category.ArraysAndHashing, Difficulty.Easy)]
   public class TwoSum : ProblemBase
   {
       protected override void Solve()
       {
           Test("Example 1", [3, 4, 5, 6], 7, [0, 1]);
           Test("Example 2", [4, 5, 6], 10, [0, 2]);
           Test("Example 3", [5, 5], 10, [0, 1]);
       }

       private void Test(string name, int[] nums, int target, int[] expected)
       {
           // Your test implementation
       }

       private int[] TwoSumMethod(int[] nums, int target)
       {
           // Your solution here
           return [];
       }
   }
   ```

   **Template.cs** — Starter boilerplate for others cloning your repo:
   ```csharp
   namespace LeetcodePractice.Problems.ArraysAndHashing;

   // Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
   public class TwoSumTemplate
   {
       // Same structure as TwoSum.cs but with empty implementations
   }
   ```

   **{ProblemName}.md** — Problem statement in NeetCode format
   
   **Notes.md** — Pre-populated headers for your approach notes

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
