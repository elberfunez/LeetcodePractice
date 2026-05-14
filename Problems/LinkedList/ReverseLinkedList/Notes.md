# Notes - Reverse Linked List

## Pattern / Approach

Iterative three-pointer approach: maintain a `prev` pointer (previous node), `cur` pointer (current node), and `next` pointer (next node). Iterate through the list, reversing the next pointer of each node to point backward instead of forward.

## Data Structure(s) Used

- **Three pointers** (prev, cur, next) — Track the previous node, current node, and next node during traversal
- **ListNode** — The linked list node structure with val and next fields

## Thought Process

**Key insight:** To reverse a linked list, I need to change the direction of the `next` pointers. Instead of `A -> B -> C`, I want `A <- B <- C`.

The challenge: I can't just reverse a pointer in place — if I change `cur.next` to point backward before saving where to go next, I'll lose the rest of the list.

**Algorithm:**
1. Use three pointers: prev (starts at null), cur (starts at head), next (placeholder)
2. For each node:
   - Save the next node before I change the pointer
   - Reverse the current node's pointer to point to prev
   - Move prev and cur one step forward
3. Return prev as the new head

```
Example: 1 -> 2 -> 3 -> null

Initial: prev=null, cur=1, next=?
         1 -> 2 -> 3 -> null

Step 1:  next=2 (save next)
         cur.next = prev (1.next = null)
         prev=1, cur=2
         null <- 1   2 -> 3 -> null

Step 2:  next=3 (save next)
         cur.next = prev (2.next = 1)
         prev=2, cur=3
         null <- 1 <- 2   3 -> null

Step 3:  next=null (save next)
         cur.next = prev (3.next = 2)
         prev=3, cur=null
         null <- 1 <- 2 <- 3

Loop ends (cur=null), return prev=3
Result: 3 -> 2 -> 1 -> null ✓
```

## Complexity

**Time:** O(N) where N = number of nodes
- Single pass through the entire list
- Each node is visited exactly once
- Each operation (pointer reassignment) is O(1)

**Space:** O(1)
- Only use three pointers (prev, cur, next)
- No extra data structures
- Constant space regardless of list size

## Edge Cases

- **Empty list** (head = null): cur starts as null, while loop never executes, returns prev = null ✓
- **Single node** ([1]): 
  - cur = 1, prev = null
  - next = null
  - cur.next = prev (1.next = null)
  - prev = 1, cur = null
  - Loop exits, return prev = 1 ✓
- **Two nodes** ([1, 2]):
  - Step 1: 1.next = null, prev = 1, cur = 2
  - Step 2: 2.next = 1, prev = 2, cur = null
  - Return 2, giving 2 -> 1 -> null ✓

## Key Insight

**Three pointers are essential to avoid losing the list!**

Why I need all three:
- **prev** — Tells me what the current node should point to
- **cur** — The node I'm currently processing
- **next** — Saves where to go next BEFORE I change cur.next

If I don't save next before changing cur.next, I lose the rest of the list and can't continue the traversal.

This is the fundamental difference between reversing a linked list and reversing an array (where I can easily access elements by index).

## Pattern Recognition for Similar Problems

🚩 **Red flags for LINKED LIST REVERSAL problems:**
1. **"Reverse the linked list"** → ITERATIVE THREE-POINTER ⭐⭐⭐
2. **"Reverse nodes in groups"** → Variation of reversal
3. **"Palindrome linked list"** → Often uses reversal on second half
4. **"Reverse between two positions"** → Partial reversal
5. **"LRU Cache"** or **"Reorder List"** → Often uses reversal techniques

**Similar Problems:** "Reverse Linked List II" (reverse between positions), "Reverse Nodes in K-Group", "Palindrome Linked List"

---

## My Solution Approach

### **Step 1: Initialize Three Pointers**
```csharp
ListNode cur = head;
ListNode prev = null;
ListNode next = null;
```

I start with:
- `cur` at the head (the node I'm processing)
- `prev` at null (no previous node yet)
- `next` uninitialized (will store the next node before I change pointers)

### **Step 2: Iterate Through the List**
```csharp
while (cur != null)
{
```

I continue until I reach the end of the list (cur becomes null).

**Critical:** The condition is `cur != null`, not `cur.next != null`. This ensures I process the last node correctly.

### **Step 3: Save the Next Node**
```csharp
next = cur.next;
```

Before I change `cur.next`, I save where the list continues. This prevents me from losing access to the rest of the list.

### **Step 4: Reverse the Pointer**
```csharp
cur.next = prev;
```

I reverse the direction: instead of pointing forward, the current node now points backward to the previous node.

### **Step 5: Move Pointers Forward**
```csharp
prev = cur;
cur = next;
```

I advance both pointers:
- `prev` becomes the current node (for the next iteration)
- `cur` becomes the saved next node (move to the next position)

### **Step 6: Return New Head**
```csharp
return prev;
```

After the loop, `prev` is at the last node of the original list, which is now the new head.

**Why `prev` and not `cur`?** When the loop exits, `cur` is null. The actual new head is the last node, which is now in `prev`.

### **Why This Works**

1. **Saves next before changing pointer** — Prevents losing the list
2. **Reverses one node at a time** — Gradual pointer direction change
3. **Single pass** — O(N) efficiency
4. **In-place reversal** — No extra space needed
5. **Handles all cases** — Empty list, single node, and multiple nodes

### **Example Walkthrough - Full Case**

```
Input: head = [0,1,2,3]

Initialize: cur=0, prev=null, next=null
List: 0 -> 1 -> 2 -> 3 -> null

Iteration 1:
  next = cur.next = 1 (save 1)
  cur.next = prev (0.next = null)
  prev = cur = 0
  cur = next = 1
  List now: null <- 0   1 -> 2 -> 3 -> null

Iteration 2:
  next = cur.next = 2 (save 2)
  cur.next = prev (1.next = 0)
  prev = cur = 1
  cur = next = 2
  List now: null <- 0 <- 1   2 -> 3 -> null

Iteration 3:
  next = cur.next = 3 (save 3)
  cur.next = prev (2.next = 1)
  prev = cur = 2
  cur = next = 3
  List now: null <- 0 <- 1 <- 2   3 -> null

Iteration 4:
  next = cur.next = null (save null)
  cur.next = prev (3.next = 2)
  prev = cur = 3
  cur = next = null
  List now: null <- 0 <- 1 <- 2 <- 3

Loop exits (cur = null)
return prev = 3

Result: 3 -> 2 -> 1 -> 0 -> null ✓
```

### **Example Walkthrough - Empty List**

```
Input: head = null

Initialize: cur=null, prev=null, next=null

while (cur != null)? No, cur is null

return prev = null ✓
```

### **Complexity Summary**

- **Time:** O(N)
  - One pass through all N nodes
  - Each node visited once
  - Each operation (assignment, comparison) is O(1)

- **Space:** O(1)
  - Only three pointers regardless of list size
  - No recursion stack (unlike recursive approach)
  - Constant memory usage

### **Alternative Approach - Recursive**

I could also reverse recursively:
```csharp
// Recursive approach - O(1) space is misleading (recursion stack uses O(N) space)
public ListNode ReverseListRecursive(ListNode head)
{
    if (head == null || head.next == null) return head;
    
    ListNode newHead = ReverseListRecursive(head.next);
    head.next.next = head;  // Reverse the link
    head.next = null;
    return newHead;
}
```

But this uses O(N) space due to the recursion stack, so the iterative approach is better.

### **Why Iterative Over Recursive?**

- **Space**: O(1) vs O(N) (recursion stack)
- **Simplicity**: Easier to understand and debug
- **Performance**: No function call overhead
- **Stack safety**: Won't overflow on very long lists

My iterative solution is the optimal approach for this problem.
