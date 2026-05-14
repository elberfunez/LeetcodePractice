namespace LeetcodePractice.Problems.LinkedList;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class PalindromeLinkedListTemplate
{
    public void Solve()
    {
        Test("Example 1", LinkedListHelper.CreateList([1, 2, 3, 2, 1]), true);
        Test("Example 2", LinkedListHelper.CreateList([2, 2]), true);
        Test("Example 3", LinkedListHelper.CreateList([2, 1]), false);
    }

    private void Test(string name, ListNode head, bool expected)
    {
        bool result = IsPalindromeSolution(head);

        Console.WriteLine($"{name}: {(result == expected ? "✓ PASS" : "✗ FAIL")}");
        Console.WriteLine($"  Input:    {LinkedListHelper.ListToString(head)}");
        if (result != expected) Console.WriteLine($"  Expected: {expected}, Got: {result}");
        Console.WriteLine();
    }

    public bool IsPalindromeSolution(ListNode head)
    {
        // Your solution here
        return true;
    }
}
