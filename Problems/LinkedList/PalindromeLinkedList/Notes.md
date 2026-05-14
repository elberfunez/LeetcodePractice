# Notes - Palindrome Linked List

## Pattern / Approach

Three-step approach: (1) Find the middle using slow/fast pointers, (2) Reverse the second half of the list, (3) Compare values from the start and the reversed second half simultaneously. If all values match, it's a palindrome.

## Data Structure(s) Used

- **Two pointers** (slow, fast) — Slow moves 1 step, fast moves 2 steps to find the middle
- **Pointer manipulation** — Reverse the second half in-place
- **Two-pointer comparison** — Traverse both halves simultaneously to check equality
- **No extra data structures** — All operations done in-place

## Thought Process

**Key insight:** A palindrome reads the same forwards and backwards. If I split the list in half and reverse the second half, then compare both halves, they should be identical if it's a palindrome.

**Algorithm breakdown:**
1. Edge case: if only one node, it's always a palindrome
2. Find middle: use slow/fast pointers (slow advances 1, fast advances 2)
3. Split: disconnect the first half from the second half
4. Reverse second half: bring the end elements to the front
5. Compare: traverse both halves together, checking if values match
6. Return true if all values match (for odd-length lists, middle element is naturally ignored)

```
Example: [1,2,3,2,1] (odd length)

Step 1: Find middle with slow/fast pointers
  slow starts at 1, fast starts at 2
  
  Iteration 1: slow → 2, fast → 3
  Iteration 2: slow → 3, fast → 1 (2 steps forward)
  Iteration 3: slow → 2, fast → null (out of bounds)
  
  slow is now at middle node 3

Step 2: Split the list
  First half: 1 → 2 → 3 → null
  Second half: 2 → 1 → null
  
  Unlink: slow.next = null

Step 3: Reverse second half
  Before: 2 → 1 → null
  After:  1 → 2 → null

Step 4: Compare both halves
  First:  1 → 2 → 3
  Second: 1 → 2 → null
  
  1 == 1 ✓
  2 == 2 ✓
  3 becomes null in one list (odd length, middle element ignored) ✓

Result: true ✓
```

## Complexity

**Time:** O(N) where N = length of list
- Find middle: O(N/2) with fast pointer
- Reverse second half: O(N/2)
- Compare both halves: O(N/2)
- Total: O(N)
- Single pass through the list

**Space:** O(1)
- Only use a few pointers (slow, fast, head, rev, etc.)
- All operations modify pointers in-place
- No extra data structures or recursion
- Constant memory

## Edge Cases

- **Single node** ([5]):
  - Immediately return true (only one item)
  - ✓

- **Two nodes, same** ([1,1]):
  - slow reaches first 1, fast reaches null (fast.next is null)
  - Reverse second half [1]
  - Compare: 1 == 1
  - Return true ✓

- **Two nodes, different** ([1,2]):
  - Find middle, reverse second half
  - Compare: 1 != 2
  - Return false ✓

- **Odd length palindrome** ([1,2,3,2,1]):
  - Middle node (3) gets isolated in first half
  - When comparing, one list ends before the other (middle element is skipped)
  - Returns true if first N/2 elements match ✓

- **Even length palindrome** ([1,2,2,1]):
  - Both halves have same length after split
  - Exact match during comparison
  - Returns true ✓

- **All same values** ([2,2,2,2]):
  - All comparisons pass
  - Returns true ✓

## Key Insight

**The slow/fast pointer + reverse + compare pattern is elegant!**

Why this approach works:
- **Find middle without counting length** — Slow/fast pointers find middle in O(N)
- **Split cleanly** — Unlinking at middle creates two independent lists
- **Reverse for comparison** — Second half reversed brings end elements to front, making comparison straightforward
- **In-place modification** — No extra space, just pointer changes
- **Handles odd/even naturally** — Odd-length lists naturally skip middle element during comparison

The beauty: instead of storing values in an array or using recursion, I leverage pointer manipulation to achieve O(N) time with O(1) space.

## Pattern Recognition for Similar Problems

🚩 **Red flags for PALINDROME/MIRROR problems:**
1. **"Is palindrome"** → SLOW/FAST + REVERSE + COMPARE ⭐⭐⭐
2. **"Compare forward and backward"** → Often involves reversal
3. **"O(1) space required"** → Don't use array/stack, use in-place reversal
4. **"Find middle"** → Slow/fast pointers
5. **"Mirror structure"** → Split, reverse one half, compare

**Similar Problems:** "Reorder Linked List" (find middle, reverse, interleave), "Valid Palindrome" (use two pointers from ends)

---

## My Solution Approach

### **Step 1: Handle Edge Case - Single Node**
```csharp
if (head.next == null) return true;
```

If the list only has one node, it's always a palindrome. Return immediately.

### **Step 2: Find the Middle Using Slow/Fast Pointers**
```csharp
ListNode slow = head;
ListNode fast = head.next;
while (fast != null && fast.next != null)
{
    slow = slow.next;
    fast = fast.next.next;
}
```

I use Floyd's technique:
- **slow** advances 1 step per iteration
- **fast** advances 2 steps per iteration
- When fast reaches the end, slow is at the middle

**Why start fast at head.next?** This ensures slow ends up at the node before the actual middle for even-length lists, or at the middle itself for odd-length lists. This gives us the right split point.

### **Step 3: Reverse the Second Half**
```csharp
ListNode rev = ReverseList(slow);
slow.next = null; // unlink it
```

I pass `slow` (the middle node) to ReverseList, which reverses from the middle to the end. Then I unlink the first half by setting `slow.next = null`.

**Why this works:** After reversal, `rev` points to the head of the reversed second half. The first half ends cleanly at `slow`.

### **Step 4: The ReverseList Helper**
```csharp
public ListNode ReverseList(ListNode head)
{
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

Standard iterative reversal: three pointers (prev, cur, next) to reverse all pointers in-place. Returns the new head of the reversed list.

### **Step 5: Compare Both Halves**
```csharp
while (head != null && rev != null)
{
    if (head.val != rev.val) return false;
    head = head.next;
    rev = rev.next;
}
return true;
```

I traverse both lists simultaneously:
- Compare values at current positions
- If any mismatch, return false
- Move both pointers forward
- If we reach the end without mismatches, return true

**For odd-length lists:** One list will have one more node (the middle), but the loop exits when either list becomes null, so the middle element is naturally ignored.

### **Why This Works**

1. **Correct middle detection** — Slow/fast pointers reliably find split point
2. **Clean split** — Unlinking separates lists cleanly
3. **Reversal brings ends forward** — Second half reversed = easy forward comparison
4. **Simple comparison** — Just check equality at each step
5. **In-place operation** — Only pointer changes, O(1) space

---

## Example Walkthrough - Odd Length [1,2,3,2,1]

```
Initial list:
  1 → 2 → 3 → 2 → 1 → null
  ↑ head            ↑ tail

Step 1: Find middle with slow/fast pointers
  slow = head (1)
  fast = head.next (2)
  
  Loop iteration 1:
    slow = slow.next = 2
    fast = fast.next.next = 3
    
  Loop iteration 2:
    slow = slow.next = 3 (MIDDLE)
    fast = fast.next.next = 1
    
  Loop iteration 3:
    slow = slow.next = 2
    fast = fast.next.next = null (exits loop, fast.next is null)
  
  Final: slow points to node 3 (middle)

Step 2: Split and reverse
  First half:  1 → 2 → 3 → null
  Second half (starting from 3): 3 → 2 → 1 → null
  
  Wait, that's not right. slow points to 3, so ReverseList(3) reverses from 3 onward.
  Actually, the second half is the REST of the list after split point.
  
  Let me reconsider: slow.next is the start of second half.
  So before unlinking: first=1→2→3→2→1
  After ReverseList(3): reverses 3→2→1 to 1→2→3
  After slow.next=null: first=1→2→3→null
  
  Actually, I need to look more carefully. The slow pointer is at position 3.
  ReverseList(slow) starts reversing from slow itself.
  So it reverses [3, 2, 1]
  
  Result after reversal: 1 → 2 → 3 → null (reversed)
  
  But wait, we need to understand what's being reversed.
  Original: 1 → 2 → 3 → 2 → 1 → null
                    ↑ slow here
  
  ReverseList(3) reverses the sublist starting at 3: [3 → 2 → 1]
  Reversed becomes: [1 → 2 → 3]
  
  The node at position 3 (value 3) is now at the end of the reversed list.
  So rev points to the new head which is the node with value 1.

Step 3: Compare both halves
  First:  1 → 2 → 3 → null
  Second: 1 → 2 → 3 → null (this is the reversed second half)
  
  Hmm, but the second half should only have [2, 1], not [3, 2, 1].
  
  Let me reconsider the middle finding. With head.next as initial fast:
  
  slow = 1, fast = 2
  Iter 1: slow = 2, fast = 3
  Iter 2: slow = 3, fast = 1
  Iter 3: slow = 2, fast = null
  
  So slow ends at position 2 (value 2), not position 3!
  
  First half:  1 → 2 → null
  Second half: 3 → 2 → 1 → null (reversed to: 1 → 2 → 3 → null)
  
  Comparing:
  1 == 1 ✓
  2 == 2 ✓
  null vs 3: first loop ends, return true
  
  The middle element (3) is naturally skipped! ✓

Final result: true ✓
```

Wait, let me recalculate more carefully:

```
List: 1 → 2 → 3 → 2 → 1 → null
      0   1   2   3   4

slow = 1, fast = 2 initially

Iteration 1:
  slow = 2, fast = fast.next.next = node at position 3
  
Iteration 2:
  slow = 3, fast = fast.next.next = node at position 1 (wrapping around... no wait)
  slow = 3, fast now points to: from position 3, next.next = position 5 = null
  fast.next is not null (it's node 2), so we continue
  
Actually, let me trace positions:
Position: 0   1   2   3   4
Value:    1   2   3   2   1

slow=0 (1), fast=1 (2)
Loop iteration 1: slow=1 (2), fast=3 (2)
Loop iteration 2: slow=2 (3), fast=5 (null exists? no, after 4 is null)
  After position 4 is null, so fast.next is null, loop exits

So slow is at position 2, which is value 3.

But wait, the code does slow.next = null to unlink.
So the first half becomes: 1 → 2 → 3 → null
And second half is what comes after position 2: 2 → 1

ReverseList(slow) where slow points to position 2 (value 3)...
Actually, this reverses starting from that node.
So it reverses: 3 → 2 → 1 to get 1 → 2 → 3

Hmm, but node 3 is now in the middle of first half, and node 2 (the duplicate) is in the second half.

Let me think about this differently. For odd-length:
- If we split at exact middle with slow.next = null
- First half: 1 → 2 → 3 (has the middle element)
- Second half: 2 → 1 (doesn't have middle)
- Reverse second: 1 → 2
- Compare: 1==1, 2==2, then first list has 3 left but second is null, loop exits
- Result: true

That makes sense!
```

### **Example Walkthrough - Even Length [1,2,2,1]**

```
List: 1 → 2 → 2 → 1 → null
      0   1   2   3

slow=0 (1), fast=1 (2)
Iteration 1: slow=1 (2), fast=3 (1)
Iteration 2: slow=2 (2), fast=5 (past end, null)
  Loop exits because fast.next is null

slow at position 1 (value 2 in the middle)

First half:  1 → 2 → null
Second half: 2 → 1 → null

Reverse second: 1 → 2 → null

Compare:
  1 == 1 ✓
  2 == 2 ✓
  Both lists are null, loop exits
  
Return true ✓
```

### **Example Walkthrough - Single Node [5]**

```
Input: [5]

head.next == null? Yes!
Return true immediately.

Result: true ✓
```

### **Example Walkthrough - Non-palindrome [2,1]**

```
List: 2 → 1 → null
      0   1

slow=0 (2), fast=1 (1)
Iteration 1: slow=1 (1), fast=3 (past end, null)
  Loop exits

slow at position 1 (value 1)

First half:  2 → 1 → null
Second half: (nothing after position 1, it's the end)

ReverseList(slow) where slow is at value 1:
  Just has [1], reverses to [1]

After slow.next = null:
First: 2 → null
Second: (empty, was just node 1 which is now part of first)

Wait, this is confusing. Let me reconsider.

Actually, when slow is at position 1 (the last position), slow.next is null.
ReverseList(slow) where slow points to last node... just returns that node as head of reversed list.
Then slow.next = null makes no difference (already null).

Actually, I think the issue is my indexing. Let me retrace:

For list [2, 1]:
Initially:
  2 → 1 → null

slow = head (points to 2)
fast = head.next (points to 1)

Loop condition: fast != null && fast.next != null
  fast is 1, fast.next is null
  fast.next is null, so loop exits without entering

So slow stays at the head (2).

ReverseList(slow) where slow points to 2:
  Reverses [2 → 1] to [1 → 2]
  
After slow.next = null:
  First half: 2 → null
  Second half (reversed): 1 → null
  
Actually wait, slow.next = null on the node that slow points to...
slow points to 2, so 2.next = null means 2 → null.

But in the reversal of [2 → 1], we're creating new pointer connections.
After ReverseList, the reversed list is 1 → 2 → ...
And ReverseList returns a pointer to 1.

So:
  rev points to 1
  slow points to 2
  
After slow.next = null:
  The original node 2 has its next set to null
  But in the reversed list, 1 points to 2, and 2 now points to null (which was already true)
  
Hmm, I'm getting confused with the pointer reuse. Let me think about what actually happens:

Original nodes: Node(2) → Node(1) → null

ReverseList starts with Node(2):
  prev=null, cur=Node(2)
  next = Node(1)
  cur.next (Node(2)'s next) = null
  prev = Node(2)
  cur = Node(1)
  
  next = null (Node(1)'s next is null)
  cur.next (Node(1)'s next) = Node(2)
  prev = Node(1)
  cur = null
  
  return prev = Node(1)

After reversal:
  Node(1) → Node(2) → null
  
rev points to Node(1)
slow still points to the original Node(2) (but its next was changed during reversal to point to null, then changed again to point to prev)

Actually, Node(2)'s next was changed during reversal to point to null in iteration 1, then to Node(2) in iteration 2... no wait.

Let me retrace more carefully:
```csharp
ListNode prev = null;
ListNode cur = head; // Node(2)

// Iteration 1
ListNode next = cur.next; // Node(1)
cur.next = prev; // Node(2).next = null
prev = cur; // prev = Node(2)
cur = next; // cur = Node(1)

// Iteration 2
ListNode next = cur.next; // null
cur.next = prev; // Node(1).next = Node(2)
prev = cur; // prev = Node(1)
cur = next; // cur = null (exits loop)

return prev; // Node(1)
```

So after ReverseList:
- Node(1) → Node(2) → null
- rev points to Node(1)

Then slow.next = null:
- slow points to the original Node(2)
- But Node(2) was already modified during reversal
- During reversal iteration 1, Node(2).next was set to null
- During reversal iteration 2, Node(1).next was set to Node(2)
- So slow.next = null is just reaffirming what's already true

The comparison:
- head points to the original Node(2) = first half
- rev points to Node(1) = second half (reversed)
- Loop: head=Node(2), rev=Node(1)
  - Compare 2 != 1? Return false immediately!

Result: false ✓
```

### **Complexity Summary**

- **Time:** O(N)
  - Finding middle: O(N/2) with fast pointer
  - Reversing second half: O(N/2)
  - Comparing: O(N/2)
  - Total: O(N)

- **Space:** O(1)
  - Only a few pointers
  - No extra data structures
  - No recursion
  - Constant memory
