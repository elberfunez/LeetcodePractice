# Notes - Reorder Linked List

## Pattern / Approach

Four-step approach: (1) Find the middle using slow/fast pointers, (2) Split the list into two halves, (3) Reverse the second half, (4) Interleave the first and reversed second halves together.

## Data Structure(s) Used

- **Two pointers** (slow, fast) — Find the middle of the list
- **Pointer manipulation** — Split, reverse, and interleave lists
- **No extra data structures** — All operations done in-place

## Thought Process

**Key insight:** The reordering pattern alternates between the start and end of the list. This suggests splitting the list and bringing the ends together.

**Algorithm breakdown:**
1. Find middle: Use slow/fast pointers to locate the midpoint
2. Split: Separate into two independent lists at the middle
3. Reverse second half: Bring elements from the end to the front
4. Merge: Interleave both lists by alternating connections

```
Example: [2,4,6,8]

Step 1: Find middle with slow/fast pointers
  Start: slow=2, fast=2
  After loop: slow=4, fast=8 (fast reached end)
  Middle is at 4

Step 2: Split at middle
  First half: 2 -> 4 -> null
  Second half: 6 -> 8 -> null

Step 3: Reverse second half
  Reversed: 8 -> 6 -> null

Step 4: Interleave
  2 -> 8 -> 4 -> 6 -> null
  ✓ Matches expected output!
```

## Complexity

**Time:** O(N) where N = number of nodes
- Find middle: O(N) — traverse list with fast pointer
- Reverse second half: O(N/2) = O(N)
- Interleave: O(N) — traverse both halves once
- Total: O(N)

**Space:** O(1)
- Only use a few pointers
- All operations modify pointers in-place
- No extra data structures

## Edge Cases

- **Single node** ([1]): 
  - Middle at node 1
  - Second half is null
  - No interleaving needed, returns [1] ✓

- **Two nodes** ([1,2]):
  - Middle at node 1
  - Second half is [2]
  - Reversed second half is [2]
  - Interleave: 1 -> 2, returns [1,2] ✓

- **Three nodes** ([1,2,3]):
  - Middle at node 2
  - First: [1,2], Second: [3]
  - Reversed second: [3]
  - Interleave: 1 -> 3 -> 2, returns [1,3,2] ✓

- **Even length** ([2,4,6,8]): Both halves equal length
- **Odd length** ([2,4,6,8,10]): First half has one more element

## Key Insight

**This problem elegantly combines three linked list techniques!**

Why this approach is clever:
- **Divide and conquer** — Split the problem into manageable halves
- **Reverse for reordering** — Reversing brings end elements to the front
- **Interleaving** — Alternating connections creates the desired pattern
- **In-place modification** — No extra space needed

The beauty: instead of trying to reorder from scratch, I leverage existing techniques (finding middle, reversing, merging) to achieve the result.

## Pattern Recognition for Similar Problems

🚩 **Red flags for LINKED LIST REORDERING problems:**
1. **"Reorder nodes"** or **"rearrange list"** → Often requires finding middle + reversing + merging ⭐⭐⭐
2. **"Alternating pattern"** from ends → Find middle, reverse, interleave
3. **"Connect first with last"** → Often involves list reversal
4. **"Palindrome checking"** → Similar middle-finding + reversing approach
5. **Multi-step transformations** → Break into manageable steps

**Similar Problems:** "Palindrome Linked List" (find middle, reverse, compare), "LRU Cache" (uses reordering), "Flatten Multilevel Linked List"

---

## My Solution Approach

### **Step 1: Find the Middle Using Slow/Fast Pointers**
```csharp
ListNode slow = head;
ListNode fast = head;
while (fast != null && fast.next != null)
{
    slow = slow.next;
    fast = fast.next.next;
}
```

I use Floyd's cycle detection technique (slow/fast pointers) to find the middle of the list. By the time `fast` reaches the end, `slow` is at the middle.

**Why this works:** Fast moves 2 steps per iteration, slow moves 1 step. When fast reaches the end, slow is roughly halfway.

### **Step 2: Split the List into Two Halves**
```csharp
ListNode secondHalf = slow.next;
slow.next = null; // disconnect the lists
```

I separate the list into two independent lists:
- First half: from head to slow
- Second half: from slow.next to the end

Setting `slow.next = null` severs the connection, creating two complete lists.

### **Step 3: Reverse the Second Half**
```csharp
ListNode secondHalfReversed = ReverseList(secondHalf);
```

I reverse the second half using the standard iterative reversal algorithm (from previous ReverseLinkedList problem).

Why? Because the reordering pattern alternates from both ends. Reversing brings the end elements to the front for easy access.

### **Step 4: Interleave the Two Halves**
```csharp
ListNode firstHalf = head;
while (firstHalf != null && secondHalfReversed != null)
{
    ListNode firstNext = firstHalf.next;
    ListNode secondNext = secondHalfReversed.next;

    firstHalf.next = secondHalfReversed;
    secondHalfReversed.next = firstNext;
    
    firstHalf = firstNext;
    secondHalfReversed = secondNext;
}
```

I merge the two halves by alternating their connections:
- Connect current first node to current second node
- Connect second node back to the first's next
- Move both pointers forward

**Critical detail:** I save `firstNext` and `secondNext` before changing pointers, otherwise I'd lose track of where to go next.

### **Why This Works**

1. **Correct middle** — Slow/fast pointers reliably find the middle
2. **Independent lists** — Disconnecting at middle lets me work separately
3. **Reversing brings ends forward** — Second half reversed = easy to access end elements
4. **Interleaving alternates correctly** — Manual pointer manipulation gives precise control
5. **In-place modification** — Only pointer changes, no data duplication

### **Example Walkthrough - Even Length**

```
Input: [2,4,6,8]

Step 1: Find middle
  slow pointer reaches 4, fast reaches end (null)
  Middle identified

Step 2: Split
  First: 2 -> 4 -> null
  Second: 6 -> 8 -> null

Step 3: Reverse second
  Reversed: 8 -> 6 -> null

Step 4: Interleave
  Iteration 1:
    firstHalf = 2, secondHalfReversed = 8
    Save: firstNext = 4, secondNext = 6
    2.next = 8, 8.next = 4
    Move: firstHalf = 4, secondHalfReversed = 6
    
  Iteration 2:
    firstHalf = 4, secondHalfReversed = 6
    Save: firstNext = null, secondNext = null
    4.next = 6, 6.next = null
    Move: firstHalf = null, secondHalfReversed = null
    
  Loop exits

Final: 2 -> 8 -> 4 -> 6 -> null ✓
```

### **Example Walkthrough - Odd Length**

```
Input: [2,4,6,8,10]

Step 1: Find middle
  slow reaches 6, fast reaches end
  Middle at 6

Step 2: Split
  First: 2 -> 4 -> 6 -> null
  Second: 8 -> 10 -> null

Step 3: Reverse second
  Reversed: 10 -> 8 -> null

Step 4: Interleave
  Iteration 1:
    2.next = 10, 10.next = 4
    firstHalf = 4, secondHalfReversed = 8
    
  Iteration 2:
    4.next = 8, 8.next = 6
    firstHalf = 6, secondHalfReversed = null
    
  Loop exits (secondHalfReversed is null)
  
  Middle element (6) stays in place ✓

Final: 2 -> 10 -> 4 -> 8 -> 6 -> null ✓
```

### **Helper Method: ReverseList**

```csharp
public ListNode ReverseList(ListNode head) {
    ListNode prev = null;
    ListNode cur = head;
    while (cur != null)
    {
        ListNode next = cur.next;
        cur.next = prev;
        prev = cur;
        cur = next;
    }
    return prev;
}
```

This is the standard iterative list reversal from the ReverseLinkedList problem. It reverses the list and returns the new head.

### **Complexity Summary**

- **Time:** O(N)
  - Finding middle: O(N/2) with fast pointer
  - Reversing: O(N/2) for second half
  - Interleaving: O(N/2) to connect both halves
  - Total: O(N)

- **Space:** O(1)
  - Only a few pointers used
  - All operations in-place
  - No extra data structures

### **Why This Is Better Than Alternatives**

**Alternative 1: Store values in array and rebuild** — O(N) time, O(N) space
```csharp
// Less efficient - uses extra space
int[] values = new int[length];
// ... extract all values ...
// ... rebuild list from reordered array ...
```

**Alternative 2: Recursive interleaving** — O(N) time, O(N) space (call stack)
```csharp
// Uses recursion - wastes space with call stack
void interleave(ListNode left, ListNode right) { ... }
```

My approach is optimal:
- Time: O(N) — can't do better since we must visit each node
- Space: O(1) — minimal and elegant
- Technique: Combines proven linked list algorithms
