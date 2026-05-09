namespace LeetcodePractice;

public enum Difficulty { Easy, Medium, Hard }

public enum Category
{
    ArraysAndHashing,
    TwoPointers,
    Stack,
    BinarySearch,
    SlidingWindow,
    LinkedList,
    Trees,
    Tries,
    Backtracking,
    HeapPriorityQueue,
    Graphs,
    OneDDP,
    Intervals,
    Greedy,
    AdvancedGraphs,
    TwoDDP,
    BitManipulation,
    MathAndGeometry
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class ProblemAttribute(int number, string title, Category category, Difficulty difficulty) : Attribute
{
    public int Number { get; } = number;
    public string Title { get; } = title;
    public Category Category { get; } = category;
    public Difficulty Difficulty { get; } = difficulty;
}
