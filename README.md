# LeetCode Practice

A personal LeetCode practice project for solving problems in C# .NET 10. Includes a Terminal.Gui TUI for selecting problems by topic with live filter search, with full debugger support.

## Quick Start

```bash
dotnet run
```

## Navigation

The TUI is a two-panel layout:

```
┌─ Topics ─────────────────────┐┌─ Problems — Trees (12)   [Tab] switch  [Enter] run  [Q] quit ─┐
│  All Problems          ( 21) ││ Filter: [________]                                             │
│  ArraysAndHashing      (  3) ││                                                                │
│> Trees                 ( 12) ││  [E] #0094  Binary Tree Inorder Traversal                     │
│  LinkedList            (  6) ││  [M] #0098  Valid Binary Search Tree                          │
│  TwoPointers           (  3) ││  [E] #0102  Binary Tree Level Order Traversal                 │
└──────────────────────────────┘└────────────────────────────────────────────────────────────────┘
```

| Key | Action |
|-----|--------|
| `↑` / `↓` | Move selection |
| `Tab` | Switch between Topics and Problems panels |
| `Enter` | Run selected problem |
| `Q` / `Esc` | Quit |
| Type anything | Filter problems by title or number (in Problems panel) |

Select a topic on the left to filter, then type in the search box to narrow by name or number. After a problem runs, press any key to return to the menu.

## Debugging

Breakpoints work exactly as before:

1. Open the `.cs` file for your problem
2. Click the gutter to set a breakpoint inside your solution method
3. Run the project in Debug mode (F5 in Rider/VS)
4. Select the topic and problem from the TUI
5. The breakpoint is hit — step through, inspect locals, etc.

## Adding a New Problem

Each problem gets its own folder with four files:

1. Create folder: `Problems/{Category}/{ProblemName}/`
2. Create four files inside:

**`{ProblemName}.cs`** — your solution (automatically appears in the TUI menu):
```csharp
namespace LeetcodePractice.Problems.ArraysAndHashing;

// Problem statement: TwoSum.md
[Problem(1, "Two Sum", Category.ArraysAndHashing, Difficulty.Easy)]
public class TwoSum : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", [3, 4, 5, 6], 7, [0, 1]);
    }

    private void Test(string name, int[] nums, int target, int[] expected) { ... }

    private int[] TwoSumMethod(int[] nums, int target)
    {
        // Your solution here
        return [];
    }
}
```

**`Template.cs`** — starter boilerplate for others cloning the repo (standalone class, no inheritance)

**`{ProblemName}.md`** — problem statement in NeetCode format

**`Notes.md`** — pre-populated section headers for your approach notes

Problems are discovered via reflection at startup — no manual registration needed.

## Project Structure

```
Problems/
  {Category}/
    {ProblemName}/
      {ProblemName}.cs    ← your solution (inherits ProblemBase)
      Template.cs         ← boilerplate for others
      {ProblemName}.md    ← problem statement
      Notes.md            ← approach notes
```

## Categories (NeetCode 150 Roadmap)

| Enum | Topic |
|------|-------|
| `ArraysAndHashing` | Arrays & Hashing |
| `TwoPointers` | Two Pointers |
| `Stack` | Stack |
| `BinarySearch` | Binary Search |
| `SlidingWindow` | Sliding Window |
| `LinkedList` | Linked List |
| `Trees` | Trees |
| `Tries` | Tries |
| `Backtracking` | Backtracking |
| `HeapPriorityQueue` | Heap / Priority Queue |
| `Graphs` | Graphs |
| `OneDDP` | 1-D Dynamic Programming |
| `Intervals` | Intervals |
| `Greedy` | Greedy |
| `AdvancedGraphs` | Advanced Graphs |
| `TwoDDP` | 2-D Dynamic Programming |
| `BitManipulation` | Bit Manipulation |
| `MathAndGeometry` | Math & Geometry |

## Difficulty Tags

- `[E]` — Easy
- `[M]` — Medium
- `[H]` — Hard
