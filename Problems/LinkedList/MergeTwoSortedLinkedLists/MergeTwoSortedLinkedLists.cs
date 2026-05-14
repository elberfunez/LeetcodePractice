namespace LeetcodePractice.Problems.LinkedList;

// Problem statement: MergeTwoSortedLinkedLists.md
[Problem(21, "Merge Two Sorted Linked Lists", Category.LinkedList, Difficulty.Easy)]
public class MergeTwoSortedLinkedLists : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", LinkedListHelper.CreateList([1, 2, 4]), LinkedListHelper.CreateList([1, 3, 5]), LinkedListHelper.CreateList([1, 1, 2, 3, 4, 5]));
        Test("Example 2", LinkedListHelper.CreateList([]), LinkedListHelper.CreateList([1, 2]), LinkedListHelper.CreateList([1, 2]));
        Test("Example 3", LinkedListHelper.CreateList([]), LinkedListHelper.CreateList([]), LinkedListHelper.CreateList([]));
    }

    private void Test(string name, ListNode list1, ListNode list2, ListNode expected)
    {
        ListNode result = MergeTwoListsSolution(list1, list2);

        bool passed = LinkedListHelper.ListsEqual(result, expected);
        Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    list1 = {LinkedListHelper.ListToString(list1)}, list2 = {LinkedListHelper.ListToString(list2)}");
        if (!passed) Console.WriteLine($"  Expected: {LinkedListHelper.ListToString(expected)}, Got: {LinkedListHelper.ListToString(result)}");
        Console.WriteLine();
    }

    public ListNode MergeTwoListsSolution(ListNode list1, ListNode list2)
    {
        ListNode dummy = new ListNode();
        ListNode tail = dummy;
        while (list1 != null && list2 != null)
        {
            if (list1.val < list2.val)
            {
                tail.next = list1;
                list1 = list1.next;
            }
            else
            {
                tail.next = list2;
                list2 = list2.next;
            }
            tail = tail.next;
        }
        // there could be a non empty list still
        if (list1 != null) tail.next = list1;
        if (list2 != null) tail.next = list2;

        return dummy.next;
    }
}
