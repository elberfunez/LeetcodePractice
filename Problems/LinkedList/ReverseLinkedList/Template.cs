namespace LeetcodePractice.Problems.LinkedList;

// ListNode definition
public class ListNodeTemplate {
    public int val;
    public ListNodeTemplate next;
    public ListNodeTemplate(int val = 0, ListNodeTemplate next = null) {
        this.val = val;
        this.next = next;
    }
}

// Template - Do not inherit from ProblemBase. To use: rename class to ReverseLinkedList and add : ProblemBase
public class ReverseLinkedListTemplate
{
    public void Solve()
    {
        Test("Example 1", CreateList([0, 1, 2, 3]), CreateList([3, 2, 1, 0]));
        Test("Example 2", null, null);
    }

    private void Test(string name, ListNodeTemplate head, ListNodeTemplate expected)
    {
        ListNodeTemplate result = ReverseListSolution(head);

        bool passed = ListsEqual(result, expected);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {ListToString(head)}");
        if (!passed) Console.WriteLine($"  Expected: {ListToString(expected)}, Got: {ListToString(result)}");
        Console.WriteLine();
    }

    private ListNodeTemplate CreateList(int[] values)
    {
        if (values.Length == 0) return null;
        ListNodeTemplate head = new ListNodeTemplate(values[0]);
        ListNodeTemplate current = head;
        for (int i = 1; i < values.Length; i++)
        {
            current.next = new ListNodeTemplate(values[i]);
            current = current.next;
        }
        return head;
    }

    private bool ListsEqual(ListNodeTemplate a, ListNodeTemplate b)
    {
        while (a != null && b != null)
        {
            if (a.val != b.val) return false;
            a = a.next;
            b = b.next;
        }
        return a == null && b == null;
    }

    private string ListToString(ListNodeTemplate head)
    {
        if (head == null) return "[]";
        var values = new System.Collections.Generic.List<int>();
        ListNodeTemplate current = head;
        while (current != null)
        {
            values.Add(current.val);
            current = current.next;
        }
        return "[" + string.Join(", ", values) + "]";
    }

    public ListNodeTemplate ReverseListSolution(ListNodeTemplate head)
    {
        // Your solution here
        return null;
    }
}
