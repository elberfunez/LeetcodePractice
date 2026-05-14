# Notes - Remove Linked List Elements

## Pattern / Approach

Dummy node + single pointer: create a dummy node before the head, then use a curr pointer to traverse. When the next node matches the target value, skip it by connecting to the node after it. Only move curr forward when NOT removing.

## Data Structure(s) Used

- **Dummy node** — Simplifies removal of the head node
- **Single pointer** (curr) — Track the current position while removing
- **Conditional movement** — Only advance curr when not removing

## Thought Process

**Key insight:** To remove a node, I need to be at the node BEFORE it, then skip it: `before.next = before.next.next`. But what if I need to remove the head? That's where the dummy node comes in.

**Algorithm breakdown:**
1. Handle edge case: empty list
2. Create dummy node pointing to head — gives us a "before-head" node to work with
3. Use curr pointer starting at dummy
4. For each node, check if the NEXT node should be removed:
   - If yes: skip it (curr.next = curr.next.next) and check again without moving curr
   - If no: move curr forward to the next node
5. Return dummy.next as the new head

**Why this works:**
- Dummy node means we can remove the head without special logic
- Checking `curr.next.val` lets us identify targets before removing them
- Only moving curr on non-matches ensures we check multiple consecutive targets
- When we skip a node, we re-check the same position (curr.next) which might also be a target

```
Example: [2,1,4,1,2,3], val = 2

Step 1: Create dummy pointing to head
  dummy(0) → 2 → 1 → 4 → 1 → 2 → 3 → null
  ↑ curr

Step 2: Check if curr.next (2) should be removed? YES
  Skip it: curr.next = curr.next.next = 1
  curr stays at dummy
  dummy(0) → 1 → 4 → 1 → 2 → 3 → null
  ↑ curr

Step 3: Check if curr.next (1) should be removed? NO
  Move curr forward: curr = curr.next
  dummy(0) → 1 → 4 → 1 → 2 → 3 → null
             ↑ curr

Step 4: Check if curr.next (4) should be removed? NO
  Move curr forward: curr = curr.next
  dummy(0) → 1 → 4 → 1 → 2 → 3 → null
                  ↑ curr

Step 5: Check if curr.next (1) should be removed? NO
  Move curr forward: curr = curr.next
  dummy(0) → 1 → 4 → 1 → 2 → 3 → null
                       ↑ curr

Step 6: Check if curr.next (2) should be removed? YES
  Skip it: curr.next = curr.next.next = 3
  curr stays at 1
  dummy(0) → 1 → 4 → 1 → 3 → null
                       ↑ curr

Step 7: Check if curr.next (3) should be removed? NO
  Move curr forward: curr = curr.next
  dummy(0) → 1 → 4 → 1 → 3 → null
                            ↑ curr

Step 8: Loop exits (curr.next is null)

Return dummy.next: [1,4,1,3] ✓
```

## Complexity

**Time:** O(N) where N = length of list
- Single pass through the list: each node is visited once
- Each comparison and pointer update is O(1)
- Total: O(N)

**Space:** O(1)
- Only use a few pointers (dummy, curr)
- No extra data structures
- Constant memory

## Edge Cases

- **Empty list** ([], val = 5):
  - head == null returns immediately
  - Return null ✓

- **All nodes match** ([1,1], val = 1):
  - dummy → 1 → 1 → null
  - Skip first 1: dummy → 1 → null
  - Skip remaining 1: dummy → null
  - Return null ✓

- **No nodes match** ([2,3], val = 1):
  - Every curr.next doesn't match, so curr keeps advancing
  - Loop exits normally
  - Return unchanged list ✓

- **Head matches** ([2,1,3], val = 2):
  - dummy → 2 → 1 → 3 → null
  - curr at dummy, curr.next = 2 matches
  - Skip: dummy → 1 → 3 → null
  - Continue normally
  - Return [1,3] ✓

- **Tail matches** ([1,2,3], val = 3):
  - curr reaches node 2
  - curr.next = 3 matches
  - Skip: node 2 → null
  - Loop exits (curr.next is null)
  - Return [1,2] ✓

- **Consecutive matches** ([1,2,2,3], val = 2):
  - curr at 1
  - curr.next = 2 matches, skip it
  - curr stays at 1, now curr.next = 2 (the next one)
  - curr.next matches again, skip it
  - Only after both are removed does curr advance
  - Return [1,3] ✓

- **Single node match** ([5], val = 5):
  - dummy → 5 → null
  - curr at dummy, curr.next = 5 matches
  - Skip: dummy → null
  - Return null ✓

## Key Insight

**The dummy node + conditional movement pattern is powerful!**

Why this approach works:
- **Dummy eliminates head-removal special case** — Treats head like any other node
- **Checking NEXT node** — Lets us identify and remove targets without getting stuck
- **Conditional curr movement** — Only advance when we DON'T remove (when we remove, we stay and re-check)
- **Single pass** — O(N) time, can't do better since we must examine each node
- **In-place removal** — Just pointer changes, no new allocations

The key was understanding that I don't move forward after removing—I stay put and check the same position again. This naturally handles consecutive matches.

## Pattern Recognition for Similar Problems

🚩 **Red flags for NODE REMOVAL problems:**
1. **"Remove all nodes matching X"** → DUMMY + TAIL POINTER ⭐⭐⭐
2. **"Might need to remove head"** → Use dummy node
3. **"Single pass required"** → Dummy + curr makes it O(N)
4. **"Consecutive removals"** → Conditional movement handles this
5. **"In-place modification"** → Just pointer changes

**Similar Problems:** "Remove Nth Node From End" (remove single node), "Remove Duplicates from Sorted List" (remove repeated values)

---

## My Solution Approach

### **Step 1: Handle Empty List Edge Case**
```csharp
if (head == null) return null;
```

If the list is empty, nothing to remove. Return null immediately.

### **Step 2: Create Dummy Node**
```csharp
ListNode dummy = new ListNode(0, head);
ListNode curr = dummy;
```

Create a dummy node pointing to the head. This gives me a node "before" the head, which eliminates the special case of removing the head node.

**Why dummy?** If I try to remove the head without dummy, I'd need to do `head = head.next`, but I can't reassign the parameter. With dummy, I can remove the head through pointer manipulation just like any other node.

### **Step 3: Traverse and Remove Loop**
```csharp
while (curr != null && curr.next != null)
{
    if (curr.next.val == val)
    {
        curr.next = curr.next.next;
    }
    else
    {
        curr = curr.next;
    }
}
```

Loop through the list, checking if the NEXT node should be removed:

**If yes (curr.next.val == val):**
- Skip the node: `curr.next = curr.next.next`
- DON'T move curr forward
- This lets me check the same position again (which might also be a target)
- Handles consecutive matches naturally

**If no:**
- Move curr forward: `curr = curr.next`
- This is a keeper, move on to the next node

### **Step 4: Return New Head**
```csharp
return dummy.next;
```

The new head is whatever dummy points to after removal. If the original head was removed, dummy now points to the next node.

### **Why This Works**

1. **Dummy handles head removal** — No special case needed
2. **Check NEXT node** — Identifies targets before removing them
3. **Conditional movement** — Only advance when keeping the node
4. **Consecutive removals work naturally** — Re-checking same position handles multiple targets in a row
5. **Single pass** — O(N) time complexity

### **Example Walkthrough - Remove Middle [2,1,4,1,2,3], val=2**

```
Initial:
  dummy(0) → 2 → 1 → 4 → 1 → 2 → 3 → null
  ↑ curr

Iteration 1: curr=dummy, curr.next=2
  2 == 2? Yes, remove it
  dummy.next = dummy.next.next = 1
  curr stays at dummy
  dummy(0) → 1 → 4 → 1 → 2 → 3 → null
  ↑ curr

Iteration 2: curr=dummy, curr.next=1
  1 == 2? No
  curr = curr.next = 1
  dummy(0) → 1 → 4 → 1 → 2 → 3 → null
             ↑ curr

Iteration 3: curr=1, curr.next=4
  4 == 2? No
  curr = curr.next = 4
  dummy(0) → 1 → 4 → 1 → 2 → 3 → null
                  ↑ curr

Iteration 4: curr=4, curr.next=1
  1 == 2? No
  curr = curr.next = 1
  dummy(0) → 1 → 4 → 1 → 2 → 3 → null
                       ↑ curr

Iteration 5: curr=1, curr.next=2
  2 == 2? Yes, remove it
  1.next = 1.next.next = 3
  curr stays at 1
  dummy(0) → 1 → 4 → 1 → 3 → null
                       ↑ curr

Iteration 6: curr=1, curr.next=3
  3 == 2? No
  curr = curr.next = 3
  dummy(0) → 1 → 4 → 1 → 3 → null
                            ↑ curr

Iteration 7: curr=3, curr.next=null
  Loop condition: curr.next != null? No, exit loop

Return dummy.next = 1
Final: [1,4,1,3] ✓
```

### **Example Walkthrough - Remove All [1,1], val=1**

```
Initial:
  dummy(0) → 1 → 1 → null
  ↑ curr

Iteration 1: curr=dummy, curr.next=1
  1 == 1? Yes, remove it
  dummy.next = dummy.next.next = 1 (the second one)
  curr stays at dummy
  dummy(0) → 1 → null
  ↑ curr

Iteration 2: curr=dummy, curr.next=1
  1 == 1? Yes, remove it
  dummy.next = dummy.next.next = null
  curr stays at dummy
  dummy(0) → null
  ↑ curr

Iteration 3: curr=dummy, curr.next=null
  Loop condition: curr.next != null? No, exit loop

Return dummy.next = null
Final: [] ✓
```

### **Example Walkthrough - Consecutive Matches [1,2,2,3], val=2**

```
Initial:
  dummy(0) → 1 → 2 → 2 → 3 → null
  ↑ curr

Iteration 1: curr=dummy, curr.next=1
  1 == 2? No
  curr = 1
  dummy(0) → 1 → 2 → 2 → 3 → null
             ↑ curr

Iteration 2: curr=1, curr.next=2
  2 == 2? Yes, remove it
  1.next = 1.next.next = 2 (the next 2)
  curr stays at 1
  dummy(0) → 1 → 2 → 3 → null
             ↑ curr

Iteration 3: curr=1, curr.next=2
  2 == 2? Yes, remove it (second consecutive match!)
  1.next = 1.next.next = 3
  curr stays at 1
  dummy(0) → 1 → 3 → null
             ↑ curr

Iteration 4: curr=1, curr.next=3
  3 == 2? No
  curr = 3
  dummy(0) → 1 → 3 → null
                  ↑ curr

Iteration 5: curr=3, curr.next=null
  Loop condition: curr.next != null? No, exit loop

Return dummy.next = 1
Final: [1,3] ✓

Notice how both 2's were removed without moving curr away from node 1!
This is the power of conditional movement.
```

### **Complexity Summary**

- **Time:** O(N)
  - Single pass through the list
  - Each node is examined once
  - Each removal operation is O(1)
  - Total: O(N)

- **Space:** O(1)
  - Only a few pointers (dummy, curr)
  - No extra data structures
  - No recursion
  - Constant memory
