namespace LeetcodePractice.Problems.LinkedList;

// ListNode definition
public class ListNodeReorderTemplate
{
    public int val;
    public ListNodeReorderTemplate next;
    public ListNodeReorderTemplate(int val = 0, ListNodeReorderTemplate next = null)
    {
        this.val = val;
        this.next = next;
    }
}

// Template - Do not inherit from ProblemBase. To use: rename class to ReorderLinkedList and add : ProblemBase
public class ReorderLinkedListTemplate
{
    public void Solve()
    {
        Test("Example 1", CreateList([2, 4, 6, 8]), [2, 8, 4, 6]);
        Test("Example 2", CreateList([2, 4, 6, 8, 10]), [2, 10, 4, 8, 6]);
    }

    private void Test(string name, ListNodeReorderTemplate head, int[] expected)
    {
        ReorderListSolution(head);

        bool passed = ListsEqual(head, CreateList(expected));
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {ListToString(head)}");
        if (!passed) Console.WriteLine($"  Expected: {ListToString(CreateList(expected))}");
        Console.WriteLine();
    }

    private ListNodeReorderTemplate CreateList(int[] values)
    {
        if (values.Length == 0) return null;
        ListNodeReorderTemplate head = new ListNodeReorderTemplate(values[0]);
        ListNodeReorderTemplate current = head;
        for (int i = 1; i < values.Length; i++)
        {
            current.next = new ListNodeReorderTemplate(values[i]);
            current = current.next;
        }
        return head;
    }

    private bool ListsEqual(ListNodeReorderTemplate a, ListNodeReorderTemplate b)
    {
        while (a != null && b != null)
        {
            if (a.val != b.val) return false;
            a = a.next;
            b = b.next;
        }
        return a == null && b == null;
    }

    private string ListToString(ListNodeReorderTemplate head)
    {
        if (head == null) return "[]";
        var values = new System.Collections.Generic.List<int>();
        ListNodeReorderTemplate current = head;
        while (current != null)
        {
            values.Add(current.val);
            current = current.next;
        }
        return "[" + string.Join(", ", values) + "]";
    }

    public void ReorderListSolution(ListNodeReorderTemplate head)
    {
        // Your solution here
    }
}
