namespace LeetcodePractice.Problems.LinkedList;

// Problem statement: RemoveNthFromEnd.md
[Problem(19, "Remove Nth Node From End of List", Category.LinkedList, Difficulty.Medium)]
public class RemoveNthFromEnd : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", LinkedListHelper.CreateList([1, 2, 3, 4]), 2, [1, 2, 4]);
        Test("Example 2", LinkedListHelper.CreateList([5]), 1, []);
        Test("Example 3", LinkedListHelper.CreateList([1, 2]), 2, [2]);
    }

    private void Test(string name, ListNode head, int n, int[] expected)
    {
        ListNode result = RemoveNthFromEndSolution(head, n);

        bool passed = LinkedListHelper.ListsEqual(result, LinkedListHelper.CreateList(expected));
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {LinkedListHelper.ListToString(head)}, n = {n}");
        if (!passed) Console.WriteLine($"  Expected: {LinkedListHelper.ListToString(LinkedListHelper.CreateList(expected))}");
        Console.WriteLine();
    }

    public ListNode RemoveNthFromEndSolution(ListNode head, int n)
    {
        ListNode dummy = new ListNode(0, head);
        ListNode left = dummy;
        ListNode right = head;

        // Create n-node gap between pointers
        while (n > 0 && right != null) {
            right = right.next;
            n--;
        }

        // Shift both pointers until right reaches end
        while (right != null) {
            left = left.next;
            right = right.next;
        }

        // Skip the target node
        left.next = left.next.next;
        return dummy.next;
    }
}
