namespace LeetcodePractice.Problems.LinkedList;

public static class LinkedListHelper
{
    public static ListNode CreateList(int[] values)
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

    public static bool ListsEqual(ListNode a, ListNode b)
    {
        while (a != null && b != null)
        {
            if (a.val != b.val) return false;
            a = a.next;
            b = b.next;
        }
        return a == null && b == null;
    }

    public static string ListToString(ListNode head)
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
}
