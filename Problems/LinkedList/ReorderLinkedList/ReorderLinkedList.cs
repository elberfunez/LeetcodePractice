namespace LeetcodePractice.Problems.LinkedList;

// Problem statement: ReorderLinkedList.md
[Problem(143, "Reorder Linked List", Category.LinkedList, Difficulty.Medium)]
public class ReorderLinkedList : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", LinkedListHelper.CreateList([2, 4, 6, 8]), [2, 8, 4, 6]);
        Test("Example 2", LinkedListHelper.CreateList([2, 4, 6, 8, 10]), [2, 10, 4, 8, 6]);
    }

    private void Test(string name, ListNode head, int[] expected)
    {
        ReorderListSolution(head);

        bool passed = LinkedListHelper.ListsEqual(head, LinkedListHelper.CreateList(expected));
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {LinkedListHelper.ListToString(head)}");
        if (!passed) Console.WriteLine($"  Expected: {LinkedListHelper.ListToString(LinkedListHelper.CreateList(expected))}");
        Console.WriteLine();
    }

    public void ReorderListSolution(ListNode head)
    {
        // Step 1: Find the middle of the list using slow/fast pointers
        ListNode slow = head;
        ListNode fast = head;
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        // Step 2: Split the list into two halves at the middle
        ListNode secondHalf = slow.next;
        slow.next = null; // disconnect the lists
       
        // Step 3: Reverse the second half
        ListNode secondHalfReversed = ReverseList(secondHalf);

        // Step 4: Interleave the first half with the reversed second half 
        ListNode firstHalf = head;
        while (firstHalf != null && secondHalfReversed != null)
        {
            // Save the next nodes before we change the pointers
            ListNode firstNext = firstHalf.next;
            ListNode secondNext = secondHalfReversed.next;

            // Link: first → reversed
            firstHalf.next = secondHalfReversed;
            
            // Link: reversed → first's next
            secondHalfReversed.next = firstNext;
            
            // Move both pointers forward
            firstHalf = firstNext;
            secondHalfReversed = secondNext;
        }
    }
    public ListNode ReverseList(ListNode head) {
        ListNode prev = null;
        ListNode cur = head;
        while (cur != null)
        {
            ListNode next = cur.next;
            cur.next = prev;
            prev = cur;
            cur = next;
        }
        return prev;
    }
}
