namespace LeetcodePractice.Problems.LinkedList;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class RemoveLinkedListElementsTemplate
{
    public void Solve()
    {
        Test("Example 1", LinkedListHelper.CreateList([2, 1, 4, 1, 2, 3]), 2, [1, 4, 1, 3]);
        Test("Example 2", LinkedListHelper.CreateList([1, 1]), 1, []);
    }

    private void Test(string name, ListNode head, int val, int[] expected)
    {
        ListNode result = RemoveElementsSolution(head, val);

        bool passed = LinkedListHelper.ListsEqual(result, LinkedListHelper.CreateList(expected));
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {LinkedListHelper.ListToString(head)}, val = {val}");
        if (!passed) Console.WriteLine($"  Expected: {LinkedListHelper.ListToString(LinkedListHelper.CreateList(expected))}");
        Console.WriteLine();
    }

    public ListNode RemoveElementsSolution(ListNode head, int val)
    {
        // Your solution here
        return head;
    }
}
