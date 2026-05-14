namespace LeetcodePractice.Problems.LinkedList;

// Problem statement: RemoveLinkedListElements.md
[Problem(203, "Remove Linked List Elements", Category.LinkedList, Difficulty.Easy)]
public class RemoveLinkedListElements : ProblemBase
{
    protected override void Solve()
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
        if (head == null) return null;
        // we need to get to the node BEFORE the one we are supposed to remove (call it R)
        ListNode dummy = new ListNode(0, head);
        ListNode curr = dummy;
        while (curr != null && curr.next != null)
        {
            if (curr.next.val == val)
            {
                curr.next = curr.next.next;
            }
            else
            {
                curr = curr.next;
            }
        }
        return dummy.next;
    }
}
