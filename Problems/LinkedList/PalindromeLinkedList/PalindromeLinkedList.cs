namespace LeetcodePractice.Problems.LinkedList;

// Problem statement: PalindromeLinkedList.md
[Problem(234, "Palindrome Linked List", Category.LinkedList, Difficulty.Easy)]
public class PalindromeLinkedList : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", LinkedListHelper.CreateList([1, 2, 3, 2, 1]), true);
        Test("Example 2", LinkedListHelper.CreateList([2, 2]), true);
        Test("Example 3", LinkedListHelper.CreateList([2, 1]), false);
    }

    private void Test(string name, ListNode head, bool expected)
    {
        bool result = IsPalindromeSolution(head);

        Console.WriteLine($"{name}: {(result == expected ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {LinkedListHelper.ListToString(head)}");
        if (result != expected) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    public bool IsPalindromeSolution(ListNode head)
    {
        if (head.next == null) return true; // theres only one item
        // find the middle of the list and reverse it
        // then traverse both lists making sure each node equals eachother
        ListNode slow = head;
        ListNode fast = head.next;
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }
        // slow is the middle
        ListNode rev = ReverseList(slow);
        slow.next = null; // unlink it

        while (head != null && rev != null)
        {
            if (head.val != rev.val) return false;
            // move pointers forward
            head = head.next;
            rev = rev.next;
        }
        // at this point , if it was an odd number of nodes,
        // maybe one list has one element left so its fine
        return true;

    }
    public ListNode ReverseList(ListNode head)
    {
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
