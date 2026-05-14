namespace LeetcodePractice.Problems.LinkedList;

// ListNode definition (use this or reference the shared ListNode class)
public class ListNodeCycleTemplate
{
    public int val;
    public ListNodeCycleTemplate next;
    public ListNodeCycleTemplate(int val = 0, ListNodeCycleTemplate next = null)
    {
        this.val = val;
        this.next = next;
    }
}

// Template - Do not inherit from ProblemBase. To use: rename class to LinkedListCycleDetection and add : ProblemBase
public class LinkedListCycleDetectionTemplate
{
    public void Solve()
    {
        Test("Example 1", CreateCyclicList([1, 2, 3, 4], 1), true);
        Test("Example 2", CreateCyclicList([1, 2], -1), false);
    }

    private void Test(string name, ListNodeCycleTemplate head, bool expected)
    {
        bool result = HasCycleSolution(head);

        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {(expected ? "linked list with cycle" : "linked list without cycle")}");
        if (!passed) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    private ListNodeCycleTemplate CreateCyclicList(int[] values, int cycleIndex)
    {
        if (values.Length == 0) return null;

        ListNodeCycleTemplate head = new ListNodeCycleTemplate(values[0]);
        ListNodeCycleTemplate current = head;
        ListNodeCycleTemplate cycleNode = (cycleIndex == 0) ? head : null;

        for (int i = 1; i < values.Length; i++)
        {
            current.next = new ListNodeCycleTemplate(values[i]);
            current = current.next;

            if (i == cycleIndex)
            {
                cycleNode = current;
            }
        }

        // Create cycle if cycleIndex is valid
        if (cycleIndex >= 0)
        {
            current.next = cycleNode;
        }

        return head;
    }

    public bool HasCycleSolution(ListNodeCycleTemplate head)
    {
        // Your solution here
        return false;
    }
}
