namespace LeetcodePractice.Problems.LinkedList;

// ListNode definition
public class ListNodeRemoveTemplate
{
    public int val;
    public ListNodeRemoveTemplate next;
    public ListNodeRemoveTemplate(int val = 0, ListNodeRemoveTemplate next = null)
    {
        this.val = val;
        this.next = next;
    }
}

// Template - Do not inherit from ProblemBase. To use: rename class to RemoveNthFromEnd and add : ProblemBase
public class RemoveNthFromEndTemplate
{
    public void Solve()
    {
        Test("Example 1", CreateList([1, 2, 3, 4]), 2, [1, 2, 4]);
        Test("Example 2", CreateList([5]), 1, []);
        Test("Example 3", CreateList([1, 2]), 2, [2]);
    }

    private void Test(string name, ListNodeRemoveTemplate head, int n, int[] expected)
    {
        ListNodeRemoveTemplate result = RemoveNthFromEndSolution(head, n);

        bool passed = ListsEqual(result, CreateList(expected));
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {ListToString(head)}, n = {n}");
        if (!passed) Console.WriteLine($"  Expected: {ListToString(CreateList(expected))}");
        Console.WriteLine();
    }

    private ListNodeRemoveTemplate CreateList(int[] values)
    {
        if (values.Length == 0) return null;
        ListNodeRemoveTemplate head = new ListNodeRemoveTemplate(values[0]);
        ListNodeRemoveTemplate current = head;
        for (int i = 1; i < values.Length; i++)
        {
            current.next = new ListNodeRemoveTemplate(values[i]);
            current = current.next;
        }
        return head;
    }

    private bool ListsEqual(ListNodeRemoveTemplate a, ListNodeRemoveTemplate b)
    {
        while (a != null && b != null)
        {
            if (a.val != b.val) return false;
            a = a.next;
            b = b.next;
        }
        return a == null && b == null;
    }

    private string ListToString(ListNodeRemoveTemplate head)
    {
        if (head == null) return "[]";
        var values = new System.Collections.Generic.List<int>();
        ListNodeRemoveTemplate current = head;
        while (current != null)
        {
            values.Add(current.val);
            current = current.next;
        }
        return "[" + string.Join(", ", values) + "]";
    }

    public ListNodeRemoveTemplate RemoveNthFromEndSolution(ListNodeRemoveTemplate head, int n)
    {
        // Your solution here
        return head;
    }
}
