namespace LeetcodePractice.Problems.LinkedList;

// Problem statement: AddTwoNumbers.md
[Problem(2, "Add Two Numbers", Category.LinkedList, Difficulty.Medium)]
public class AddTwoNumbers : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1: Basic case",
            CreateList(1, 2, 3),
            CreateList(4, 5, 6),
            CreateList(5, 7, 9),
            "321 + 654 = 975");

        Test("Example 2: Single digit with carry",
            CreateList(9),
            CreateList(9),
            CreateList(8, 1),
            "9 + 9 = 18");

        Test("Edge case: Different lengths",
            CreateList(9, 9, 9, 9, 9, 9, 9),
            CreateList(9, 9, 9, 9),
            CreateList(8, 9, 9, 9, 0, 0, 0, 1),
            "9999999 + 9999 = 10009998");

        Test("Edge case: One list is zero",
            CreateList(0),
            CreateList(1, 2, 3),
            CreateList(1, 2, 3),
            "0 + 321 = 321");

        Test("Edge case: Multiple carries",
            CreateList(5, 5, 5),
            CreateList(5, 5, 5),
            CreateList(0, 1, 1, 1),
            "555 + 555 = 1110");
    }

    private void Test(string name, ListNode? l1, ListNode? l2, ListNode? expected, string explanation)
    {
        try
        {
            var solution = new Solution();
            var result = solution.AddTwoNumbers(l1!, l2!);
            bool passed = ListsEqual(result, expected);
            Console.WriteLine($"{name}: {(passed ? "✓ PASS" : "✗ FAIL")}");
            Console.WriteLine($"  Input:    l1={FormatList(l1)}, l2={FormatList(l2)}");
            Console.WriteLine($"  Output:   {FormatList(result)}");
            if (!passed)
                Console.WriteLine($"  Expected: {FormatList(expected)}");
            Console.WriteLine($"  {explanation}");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{name}: ✗ FAIL");
            Console.WriteLine($"  Error: {ex.Message}");
            Console.WriteLine();
        }
    }

    private string FormatList(ListNode? head)
    {
        if (head == null) return "[]";
        var values = new List<int>();
        var current = head;
        while (current != null)
        {
            values.Add(current.val);
            current = current.next;
        }
        return "[" + string.Join(",", values) + "]";
    }

    private ListNode? CreateList(params int[] values)
    {
        if (values.Length == 0) return null;
        var head = new ListNode(values[0]);
        var current = head;
        for (int i = 1; i < values.Length; i++)
        {
            current.next = new ListNode(values[i]);
            current = current.next;
        }
        return head;
    }

    private bool ListsEqual(ListNode? l1, ListNode? l2)
    {
        while (l1 != null && l2 != null)
        {
            if (l1.val != l2.val) return false;
            l1 = l1.next;
            l2 = l2.next;
        }
        return l1 == null && l2 == null;
    }

    public class Solution
    {
        public ListNode? AddTwoNumbers(ListNode l1, ListNode l2)
        {
            // since they are given in reverse-order it makes adding them easier
            ListNode dummy = new ListNode();
            ListNode cur = dummy;
            
            int carry = 0;
            while (l1 != null || l2 != null || carry > 0)
            {
                int val1 = l1?.val ?? 0;
                int val2 = l2?.val ?? 0;
                
                // add the two together 
                int sum = val1 + val2 + carry;
                int digit = sum % 10;
                carry = sum / 10;

                cur.next = new ListNode(digit);
                cur = cur.next;
                
                // update pointers only if they exist
                l1 = l1?.next;
                l2 = l2?.next;
            }

            return dummy.next;
        }
    }
}
