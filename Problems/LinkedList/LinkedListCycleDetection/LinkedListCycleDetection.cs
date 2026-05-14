namespace LeetcodePractice.Problems.LinkedList;

// Problem statement: LinkedListCycleDetection.md
[Problem(141, "Linked List Cycle Detection", Category.LinkedList, Difficulty.Easy)]
public class LinkedListCycleDetection : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", CreateCyclicList([1, 2, 3, 4], 1), true);
        Test("Example 2", CreateCyclicList([1, 2], -1), false);
    }

    private void Test(string name, ListNode head, bool expected)
    {
        bool result = HasCycleSolution(head);

        bool passed = result == expected;
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {(expected ? "linked list with cycle" : "linked list without cycle")}");
        if (!passed) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    private ListNode CreateCyclicList(int[] values, int cycleIndex)
    {
        if (values.Length == 0) return null;

        ListNode head = new ListNode(values[0]);
        ListNode current = head;
        ListNode cycleNode = (cycleIndex == 0) ? head : null;

        for (int i = 1; i < values.Length; i++)
        {
            current.next = new ListNode(values[i]);
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

    public bool HasCycleSolution(ListNode head)
    {
        ListNode slow = head;
        ListNode fast = head;
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
            if (slow == fast) return true;
        }
        return false;
    }
}
