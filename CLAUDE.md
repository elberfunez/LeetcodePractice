# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a C# .NET 10 console application for practicing LeetCode problems organized by topic (NeetCode 150 roadmap). The project features a reflection-based menu system that automatically discovers and loads problems at runtime.

## Build & Run Commands

```bash
# Run the program (displays interactive menu)
dotnet run

# Build the project
dotnet build

# Clean build artifacts
dotnet clean

# Run in Release mode
dotnet run --configuration Release
```

## Architecture Overview

### Problem Discovery & Execution

Problems are discovered at runtime using reflection in [Program.cs:4-19](Program.cs#L4-L19). The discovery process:

1. Scans all types in the executing assembly
2. Finds classes implementing `IProblem` that aren't abstract
3. Reads metadata from `ProblemAttribute` on each class
4. Sorts by problem number and displays in an interactive menu
5. When selected, instantiates the problem class and calls `Run()`

**Key insight:** Problems are automatically included just by being placed in the correct folder structure and decorated with `@Problem`. No manual registration needed.

### Problem Inheritance Chain

- **IProblem** ([IProblem.cs](IProblem.cs)): Single method `Run()` that launches the problem
- **ProblemBase** ([ProblemBase.cs](ProblemBase.cs)): Abstract base that implements `Run()` and delegates to abstract `Solve()`
- **Concrete Problems**: Inherit from `ProblemBase`, override `Solve()` with solution logic and test cases

Always inherit from `ProblemBase`, never directly from `IProblem`.

### Metadata System

[ProblemAttribute.cs](ProblemAttribute.cs) defines the metadata attached to each problem:
- `Number`: LeetCode problem number (used for sorting)
- `Title`: Problem name
- `Category`: Topic enum (ArraysAndHashing, Trees, etc.)
- `Difficulty`: Easy, Medium, or Hard

All four properties are required when decorating a problem class.

## Adding a New Problem

Each problem gets its own folder with four files:

1. Create folder: `Problems/{Category}/{ProblemName}/`
2. Create four files inside:
   - `{ProblemName}.cs` — your solution (inherits `ProblemBase`)
   - `Template.cs` — starter boilerplate for others (plain class, no inheritance)
   - `{ProblemName}.md` — problem statement (NeetCode format)
   - `Notes.md` — pre-populated section headers for your thought process

Example structure:
```
Problems/
  TwoPointers/
    IsPalindrome/
      IsPalindrome.cs    ← your solution
      Template.cs        ← boilerplate for others
      IsPalindrome.md    ← problem statement
      Notes.md           ← approach notes
```

`IsPalindrome.cs` content:
```csharp
namespace LeetcodePractice.Problems.TwoPointers;

// Problem statement: IsPalindrome.md
[Problem(125, "Valid Palindrome", Category.TwoPointers, Difficulty.Easy)]
public class IsPalindrome : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", "input", expectedOutput);
        Test("Example 2", "input", expectedOutput);
    }

    private void Test(string name, /* params */)
    {
        // Implement test logic and call solution method
    }

    private bool YourSolutionMethod(/* params */)
    {
        // Your solution here
        return false;
    }
}
```

`Template.cs` content (for others cloning your repo):
```csharp
namespace LeetcodePractice.Problems.TwoPointers;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class IsPalindromeTemplate
{
    public void Solve()
    {
        Test("Example 1", "input", expectedOutput);
    }

    private void Test(string name, /* params */)
    {
        // Implement test logic here
    }

    private bool YourSolutionMethod(/* params */)
    {
        // Your solution here
        return false;
    }
}
```

## Debugging

Set breakpoints directly in solution methods:
1. Open the `.cs` file for your problem
2. Click in the gutter to set a breakpoint
3. Run with F5 (Debug) in Rider/VS
4. Select your problem from the menu
5. Breakpoint is hit — step through normally

## Problem File Organization

- **Structure**: `Problems/{Category}/{ProblemName}/` — each problem has its own folder
- **Files per problem**:
  - `{ProblemName}.cs` — your solution (implements IProblem via ProblemBase)
  - `Template.cs` — boilerplate template (standalone class, no inheritance)
  - `{ProblemName}.md` — problem statement
  - `Notes.md` — thought process, patterns, complexity analysis
- **Namespace**: `LeetcodePractice.Problems.{CategoryName}` (kept at category level, not problem level)

## Categories (Enums in ProblemAttribute.cs)

ArraysAndHashing, TwoPointers, Stack, BinarySearch, SlidingWindow, LinkedList, Trees, Tries, Backtracking, HeapPriorityQueue, Graphs, OneDDP, Intervals, Greedy, AdvancedGraphs, TwoDDP, BitManipulation, MathAndGeometry
