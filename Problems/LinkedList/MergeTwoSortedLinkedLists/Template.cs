namespace LeetcodePractice.Problems.LinkedList;

// ListNode definition
public class ListNodeMergeTemplate {
    public int val;
    public ListNodeMergeTemplate next;
    public ListNodeMergeTemplate(int val = 0, ListNodeMergeTemplate next = null) {
        this.val = val;
        this.next = next;
    }
}

// Template - Do not inherit from ProblemBase. To use: rename class to MergeTwoSortedLinkedLists and add : ProblemBase
public class MergeTwoSortedLinkedListsTemplate
{
    public void Solve()
    {
        Test("Example 1", CreateList([1, 2, 4]), CreateList([1, 3, 5]), CreateList([1, 1, 2, 3, 4, 5]));
        Test("Example 2", CreateList([]), CreateList([1, 2]), CreateList([1, 2]));
        Test("Example 3", CreateList([]), CreateList([]), CreateList([]));
    }

    private void Test(string name, ListNodeMergeTemplate list1, ListNodeMergeTemplate list2, ListNodeMergeTemplate expected)
    {
        ListNodeMergeTemplate result = MergeTwoListsSolution(list1, list2);

        bool passed = ListsEqual(result, expected);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    list1 = {ListToString(list1)}, list2 = {ListToString(list2)}");
        if (!passed) Console.WriteLine($"  Expected: {ListToString(expected)}, Got: {ListToString(result)}");
        Console.WriteLine();
    }

    private ListNodeMergeTemplate CreateList(int[] values)
    {
        if (values.Length == 0) return null;
        ListNodeMergeTemplate head = new ListNodeMergeTemplate(values[0]);
        ListNodeMergeTemplate current = head;
        for (int i = 1; i < values.Length; i++)
        {
            current.next = new ListNodeMergeTemplate(values[i]);
            current = current.next;
        }
        return head;
    }

    private bool ListsEqual(ListNodeMergeTemplate a, ListNodeMergeTemplate b)
    {
        while (a != null && b != null)
        {
            if (a.val != b.val) return false;
            a = a.next;
            b = b.next;
        }
        return a == null && b == null;
    }

    private string ListToString(ListNodeMergeTemplate head)
    {
        if (head == null) return "[]";
        var values = new System.Collections.Generic.List<int>();
        ListNodeMergeTemplate current = head;
        while (current != null)
        {
            values.Add(current.val);
            current = current.next;
        }
        return "[" + string.Join(", ", values) + "]";
    }

    public ListNodeMergeTemplate MergeTwoListsSolution(ListNodeMergeTemplate list1, ListNodeMergeTemplate list2)
    {
        // Your solution here
        return null;
    }
}
