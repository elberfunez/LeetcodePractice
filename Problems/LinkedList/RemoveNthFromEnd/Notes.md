# Notes - Remove Nth Node From End of List

## Pattern / Approach

Two-pointer with dummy node: create a dummy node before the head, establish an n-node gap between left and right pointers, then move both pointers until right reaches the end. The node just before the target is found, and we remove it by skipping to its next.next.

## Data Structure(s) Used

- **Two pointers** (left, right) — Create a gap to identify which node to remove
- **Dummy node** — Handles the edge case of removing the head node
- **Pointer arithmetic** — Skip the target node by updating next pointer

## Thought Process

**Key insight:** To remove the nth node from the end, I need to find the node BEFORE it, not the node itself. Then I can skip it: `left.next = left.next.next`.

**Challenge:** How do I know which node is "before the nth from end" without calculating the list length first?

**Solution:** Use a two-pointer gap technique:
1. Create an n-node gap between pointers
2. Move both pointers in sync until the right pointer reaches the end
3. The left pointer will be exactly at the node before the target

**Why dummy node matters:**
- If I need to remove the head, left pointer points to dummy instead
- This avoids a special case for removing the first element
- I return `dummy.next` instead of `head`

```
Example: [1,2,3,4], n = 2 (remove node 3)

Create gap of 2:
  left = dummy
  right = 1 (move 2 steps)
  right = 3

Now gap: dummy -> 1 -> 2 -> 3 -> 4
          ↑ left          ↑ right

Move until right reaches end:
  left = 1, right = 3 (2 steps)
  left = 2, right = 4
  left = 3, right = null

Now left = 3, which is right before the target (3 -> 4)
Wait, that's wrong. Let me recalculate...

Actually:
  left points to dummy
  right points to 1 (position 0 in 0-indexed from dummy)
  
Move right n=2 steps:
  right = 1.next = 2
  right = 2.next = 3
  
Now gap is 2 nodes: dummy -> 1 -> 2 -> 3
                    ↑ left           ↑ right

Now move both until right.next = null:
  left = 1, right = 3
  left = 2, right = 4
  left = 3, right = null

left points to node 3, left.next points to node 4
Target is node 3 (left.next)
Skip it: left.next = left.next.next = node 4

Result: dummy -> 1 -> 2 -> 4 ✓
```

## Complexity

**Time:** O(L) where L = length of list
- First loop: O(N) to create the gap
- Second loop: O(L-N) to move both pointers to the end
- Total: O(L)
- Single pass algorithm (once we count n, we know what to remove)

**Space:** O(1)
- Only use two pointers and a dummy node
- No additional data structures
- Constant memory

## Edge Cases

- **Remove the head** ([1,2], n=2):
  - Dummy node prevents null pointer exception
  - left stays at dummy when right reaches end
  - left.next = left.next.next removes the head
  - Returns dummy.next = node 2 ✓

- **Single node** ([1], n=1):
  - dummy -> 1 -> null
  - After creating gap: right = null
  - left stays at dummy
  - left.next = left.next.next = null
  - Returns dummy.next = null ✓

- **Remove last node** ([1,2,3], n=1):
  - Gap of 1: right reaches the last node
  - Both pointers move together
  - left stops at the second-to-last node
  - Removes the last node correctly ✓

- **Remove from beginning of long list** ([1,2,3,4,5], n=5):
  - Gap of 5: right moves past the list
  - Wait, right becomes null after moving past position 4
  - Let me reconsider... n=5 means remove 5th from end, which is the 1st node
  - Gap of 5: right = null (we go past the end)
  - left stays at dummy
  - Correctly removes the head ✓

## Key Insight

**The dummy node + two-pointer gap is elegant!**

Why this approach is brilliant:
- **No length calculation** — I don't need to know the list length beforehand
- **Handles head removal** — Dummy node makes it seamless
- **Single pass** — Two loops but only O(L) time total
- **Clean logic** — No special cases for removing the head

The gap technique works because:
- Gap of n means: when right reaches the end, left is n steps behind
- Left is exactly at the node before the target
- No need to calculate "which position is nth from end"

## Pattern Recognition for Similar Problems

🚩 **Red flags for N-FROM-END problems:**
1. **"Remove/find nth from the end"** → TWO-POINTER GAP ⭐⭐⭐
2. **"Avoid recalculating length"** → TWO-POINTER GAP
3. **"Dummy node + two pointers"** → Often solves end-based problems
4. **"Single pass solution"** on lists → TWO-POINTER GAP
5. **"Find middle/position from end"** → TWO-POINTER

**Similar Problems:** "Find Middle of Linked List", "Linked List Cycle II" (find cycle start), "Palindrome Linked List" (partially)

---

## My Solution Approach

### **Step 1: Create Dummy Node**
```csharp
ListNode dummy = new ListNode(0, head);
```

I create a dummy node with value 0 that points to the head. This is a common technique to handle edge cases (like removing the head) without special logic.

**Why?** If I need to remove the head, I can't do `head = head.next` in a method — I have no way to return the new head. With dummy, left pointer can safely point to it.

### **Step 2: Initialize Pointers**
```csharp
ListNode left = dummy;
ListNode right = head;
```

- `left` starts at dummy
- `right` starts at head
- They will maintain a gap of n nodes between them

### **Step 3: Create N-Node Gap**
```csharp
while (n > 0 && right != null) {
    right = right.next;
    n--;
}
```

I move the `right` pointer n steps ahead. After this loop, there's an n-node gap between left and right.

**Visualization after creating gap of n=2:**
```
dummy -> 1 -> 2 -> 3 -> 4 -> null
↑ left           ↑ right (3 steps from dummy)
```

### **Step 4: Move Both Pointers Until Right Reaches End**
```csharp
while (right != null) {
    left = left.next;
    right = right.next;
}
```

I move both pointers in sync. The gap remains constant. When right becomes null (reaches end), left is exactly at the node before the target.

**Why this works:** The gap was n, so when right finishes, left is n positions from the end, which is exactly before the node I want to remove.

### **Step 5: Remove the Target Node**
```csharp
left.next = left.next.next;
```

The node right after left is the target node. I skip it by making left point to left.next.next.

### **Step 6: Return New Head**
```csharp
return dummy.next;
```

The new head is what dummy points to after removal. This handles the case where I removed the original head.

### **Why This Works**

1. **Dummy node** — Eliminates special case for removing the head
2. **Gap technique** — Left pointer is always before the target without length calculation
3. **Single pass** — Only one forward traversal, very efficient
4. **Clean removal** — left.next = left.next.next is straightforward

### **Example Walkthrough - Remove Middle**

```
Input: [1,2,3,4], n = 2 (remove node with value 3)

Step 1: Create dummy
  dummy(0) -> 1 -> 2 -> 3 -> 4 -> null

Step 2: Initialize
  left = dummy, right = 1

Step 3: Create gap of n=2
  Iteration 1: right = 2, n = 1
  Iteration 2: right = 3, n = 0
  Loop exits
  
  State: dummy -> 1 -> 2 -> 3 -> 4
         ↑ left           ↑ right (gap = 2)

Step 4: Move both until right = null
  Iteration 1: left = 1, right = 3
  Iteration 2: left = 2, right = 4
  Iteration 3: left = 3, right = null
  Loop exits
  
  State: dummy -> 1 -> 2 -> 3 -> 4
                        ↑ left  ↑ right (null)
  
  left is the node before the target (3)!

Step 5: Skip target node
  left.next = left.next.next
  2.next = 2.next.next = 4
  
  Result: dummy -> 1 -> 2 -> 4 -> null

Step 6: Return
  return dummy.next = 1
  
Final: [1,2,4] ✓
```

### **Example Walkthrough - Remove Head**

```
Input: [1,2], n = 2 (remove the 2nd from end = the head)

Step 1: Create dummy
  dummy(0) -> 1 -> 2 -> null

Step 2: Initialize
  left = dummy, right = 1

Step 3: Create gap of n=2
  Iteration 1: right = 2, n = 1
  Iteration 2: right = null, n = 0
  Loop exits
  
  State: dummy -> 1 -> 2 -> null
         ↑ left        ↑ right (null)
  
  right is already null! Gap created in first iteration.

Step 4: Move both until right = null
  right is already null, so loop doesn't execute
  left stays at dummy
  
  State: dummy -> 1 -> 2 -> null
         ↑ left

Step 5: Skip target node
  left.next = left.next.next
  dummy.next = dummy.next.next = 2
  
  Result: dummy -> 2 -> null

Step 6: Return
  return dummy.next = 2
  
Final: [2] ✓
```

### **Example Walkthrough - Single Node**

```
Input: [5], n = 1

Step 1: Create dummy
  dummy(0) -> 5 -> null

Step 2: Initialize
  left = dummy, right = 5

Step 3: Create gap of n=1
  Iteration 1: right = null, n = 0
  Loop exits
  
  State: dummy -> 5 -> null
         ↑ left  ↑ right (null)

Step 4: Move both until right = null
  right is already null, loop doesn't execute
  left stays at dummy
  
Step 5: Skip target
  dummy.next = dummy.next.next = null
  
  Result: dummy -> null

Step 6: Return
  return dummy.next = null
  
Final: [] ✓
```

### **Complexity Summary**

- **Time:** O(L)
  - First loop: at most N moves to create gap
  - Second loop: at most L-N moves until right reaches end
  - Total: O(N + (L-N)) = O(L)
  - Single forward pass, can't do better

- **Space:** O(1)
  - Only two pointers and a dummy node
  - Constant space

### **Why This Is Better Than Alternatives**

**Alternative 1: Calculate length first** — O(L) + O(L) = O(2L)
```csharp
// Two passes - slower
int length = GetLength(head);
int targetPos = length - n;
// ... traverse to targetPos and remove ...
```

**Alternative 2: Recursive approach** — O(L) time, O(L) space (call stack)
```csharp
// Wastes space with recursion
private int RemoveRecursive(ListNode node, int n) { ... }
```

My solution is optimal:
- **Time:** O(L) single pass
- **Space:** O(1) constant
- **Elegance:** No special cases, clean logic
