namespace LeetcodePractice.Problems.LinkedList;

// Problem statement: ReverseLinkedList.md
[Problem(206, "Reverse Linked List", Category.LinkedList, Difficulty.Easy)]
public class ReverseLinkedList : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", LinkedListHelper.CreateList([0, 1, 2, 3]), LinkedListHelper.CreateList([3, 2, 1, 0]));
        Test("Example 2", null, null);
    }

    private void Test(string name, ListNode head, ListNode expected)
    {
        ListNode result = ReverseListSolution(head);

        bool passed = LinkedListHelper.ListsEqual(result, expected);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {LinkedListHelper.ListToString(head)}");
        if (!passed) Console.WriteLine($"  Expected: {LinkedListHelper.ListToString(expected)}, Got: {LinkedListHelper.ListToString(result)}");
        Console.WriteLine();
    }

    public ListNode ReverseListSolution(ListNode head)
    {
        ListNode cur = head;
        ListNode prev = null;
        while (cur != null)
        {
            var next = cur.next;
            cur.next = prev;
            prev = cur;
            cur = next;
        }
        return prev;
    }
}
