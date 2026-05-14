namespace LeetcodePractice.Problems.LinkedList;

// ListNode definition
public class ListNode {
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null) {
        this.val = val;
        this.next = next;
    }
}

// Problem statement: ReverseLinkedList.md
[Problem(206, "Reverse Linked List", Category.LinkedList, Difficulty.Easy)]
public class ReverseLinkedList : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", CreateList([0, 1, 2, 3]), CreateList([3, 2, 1, 0]));
        Test("Example 2", null, null);
    }

    private void Test(string name, ListNode head, ListNode expected)
    {
        ListNode result = ReverseListSolution(head);

        bool passed = ListsEqual(result, expected);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {ListToString(head)}");
        if (!passed) Console.WriteLine($"  Expected: {ListToString(expected)}, Got: {ListToString(result)}");
        Console.WriteLine();
    }

    private ListNode CreateList(int[] values)
    {
        if (values.Length == 0) return null;
        ListNode head = new ListNode(values[0]);
        ListNode current = head;
        for (int i = 1; i < values.Length; i++)
        {
            current.next = new ListNode(values[i]);
            current = current.next;
        }
        return head;
    }

    private bool ListsEqual(ListNode a, ListNode b)
    {
        while (a != null && b != null)
        {
            if (a.val != b.val) return false;
            a = a.next;
            b = b.next;
        }
        return a == null && b == null;
    }

    private string ListToString(ListNode head)
    {
        if (head == null) return "[]";
        var values = new System.Collections.Generic.List<int>();
        ListNode current = head;
        while (current != null)
        {
            values.Add(current.val);
            current = current.next;
        }
        return "[" + string.Join(", ", values) + "]";
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
