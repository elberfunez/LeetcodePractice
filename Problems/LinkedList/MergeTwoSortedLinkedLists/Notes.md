# Notes - Merge Two Sorted Linked Lists

## Pattern / Approach

Two-pointer merge: maintain a tail pointer on the result list, compare values from both input lists, attach the smaller node, and advance that list's pointer. At the end, attach any remaining nodes.

## Data Structure(s) Used

- **Two pointers** (list1, list2) — Track position in each input list
- **Tail pointer** — Track the end of the merged result list
- **Dummy node** — Simplify list construction by avoiding special case for the first node
- **Pointer manipulation** — Direct node connections without creating new nodes

## Thought Process

**Key insight:** Since both lists are already sorted, I don't need to search or reorder. I just need to pick nodes in order from whichever list has the smaller value.

**Algorithm breakdown:**
1. Create a dummy node pointing to nothing — this avoids handling the "first node" specially
2. Use a tail pointer starting at dummy to track where to attach the next node
3. Compare the first nodes of both lists, attach the smaller one to tail.next
4. Advance the pointer of whichever list I just took from
5. Move tail forward to the node I just attached
6. Repeat until one list is exhausted
7. Attach any remaining nodes from the non-empty list
8. Return dummy.next as the head of the merged list

```
Example: list1 = [1,2,4], list2 = [1,3,5]

Step 1: Create dummy and tail
  dummy -> null
  tail points to dummy

Step 2: Compare 1 vs 1
  1 < 1? No, equal. Attach list2's node.
  dummy -> 1(list2) -> null
  tail points to 1(list2)
  list2 advances to 3

Step 3: Compare 1 vs 3
  1 < 3? Yes. Attach list1's node.
  dummy -> 1(list2) -> 1(list1) -> null
  tail points to 1(list1)
  list1 advances to 2

Step 4: Compare 2 vs 3
  2 < 3? Yes. Attach list1's node.
  dummy -> 1(list2) -> 1(list1) -> 2 -> null
  tail points to 2
  list1 advances to 4

Step 5: Compare 4 vs 3
  4 < 3? No. Attach list2's node.
  dummy -> 1(list2) -> 1(list1) -> 2 -> 3 -> null
  tail points to 3
  list2 advances to 5

Step 6: Compare 4 vs 5
  4 < 5? Yes. Attach list1's node.
  dummy -> 1(list2) -> 1(list1) -> 2 -> 3 -> 4 -> null
  tail points to 4
  list1 is now null

Step 7: list1 is null, attach remaining list2
  dummy -> 1(list2) -> 1(list1) -> 2 -> 3 -> 4 -> 5 -> null

Step 8: Return dummy.next
  Result: [1,1,2,3,4,5] ✓
```

## Complexity

**Time:** O(L + M) where L and M are the lengths of list1 and list2
- Single pass through each list: we visit each node exactly once
- Each comparison and attachment is O(1)
- Total: O(L + M)

**Space:** O(1)
- Only use a few pointers (dummy, tail, list1, list2)
- No extra data structures
- Reuse existing nodes from input lists, no copying
- Constant memory

## Edge Cases

- **Both lists empty** ([], []):
  - dummy.next stays null
  - Return dummy.next = null ✓

- **One list empty** ([1,2], []):
  - Comparison loop exits immediately (second condition fails)
  - Attach remaining list1: tail.next = list1
  - Return [1,2] ✓

- **All values in list1 are smaller** ([1,2,3], [4,5,6]):
  - All list1 nodes attached first, tail ends at 3
  - list2 still has nodes, attach: tail.next = list2
  - Return [1,2,3,4,5,6] ✓

- **Duplicate values across lists** ([1,2,4], [1,3,5]):
  - Equal values (1 == 1): attach second list's node (due to else branch)
  - Order is deterministic
  - Return [1,1,2,3,4,5] ✓

- **Single node each** ([1], [2]):
  - Compare 1 vs 2, attach 1, list1 becomes null
  - Attach remaining 2
  - Return [1,2] ✓

## Key Insight

**The dummy node + tail pointer pattern is elegant for list construction!**

Why this approach works:
- **No special first node handling** — dummy gives us a place to attach the first real node
- **Tail pointer keeps it simple** — always attach to tail.next, never worry about where to connect
- **In-place merge** — reuse existing nodes, no allocation needed
- **Single pass** — linear time, can't do better since we must examine each node
- **Handles edge cases naturally** — empty lists work without special logic

The key was understanding that I don't need to create new nodes — I'm just rearranging pointers of existing nodes.

## Pattern Recognition for Similar Problems

🚩 **Red flags for MERGE problems:**
1. **"Merge two sorted lists"** → TWO-POINTER MERGE ⭐⭐⭐
2. **"Combine two data structures"** that are already ordered → Consider two-pointer merge
3. **"Merge in-place"** → Dummy node + tail pointer pattern
4. **"No extra space"** → Reuse existing nodes, just change pointers
5. **Linear time required** → Two-pointer approach achieves O(N+M)

**Similar Problems:** "Merge K Sorted Lists" (extend merge to multiple lists), "Sorted Array Merge" (similar logic, different data structure)

---

## My Solution Approach

### **Step 1: Create Dummy Node and Tail Pointer**
```csharp
ListNode dummy = new ListNode();
ListNode tail = dummy;
```

I create a dummy node with default value (0) that serves as the starting point for building the merged list. The tail pointer tracks where to attach the next node.

**Why dummy?** It eliminates the special case of attaching the first node. I always use `tail.next = ...` without checking if this is the first attachment.

### **Step 2: Compare and Merge While Both Lists Have Nodes**
```csharp
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
```

I compare the current nodes from both lists:
- If list1's value is smaller, attach list1's node and advance list1
- Otherwise (equal or list2 is smaller), attach list2's node and advance list2
- Move tail forward to the newly attached node

**Critical detail:** I attach the entire node, not just the value. By doing `tail.next = list1`, I'm linking the rest of list1's chain. Then `list1 = list1.next` moves my comparison pointer forward, but the already-attached node stays in the merged list.

### **Step 3: Attach Remaining Nodes**
```csharp
if (list1 != null) tail.next = list1;
if (list2 != null) tail.next = list2;
```

After the loop, exactly one of the two lists has remaining nodes (or both are null). I attach whichever list still has nodes.

**Why this works:** Since both input lists are sorted, any remaining nodes are already in correct order relative to what I've merged so far. No need to compare further.

### **Step 4: Return the Merged List**
```csharp
return dummy.next;
```

The merged list starts at dummy.next, skipping the dummy node itself.

### **Why This Works**

1. **Dummy eliminates special cases** — Always use tail.next, never need to handle "first node" separately
2. **Tail pointer stays at the end** — I know exactly where to attach next
3. **Node reuse** — No allocation, just pointer changes
4. **Linear time** — Single pass through both lists
5. **Handles all edge cases** — Empty lists, duplicates, one list finishing before the other

### **Example Walkthrough - Mixed Values**

```
Input: list1 = [1,2,4], list2 = [1,3,5]

Initial state:
  dummy(0) -> null
  ↑ tail
  
Iteration 1: Compare 1 vs 1
  1 < 1? No, attach list2's 1
  dummy(0) -> 1(from list2) -> 3 -> 5 -> null
  ↑ tail moves to this 1
  list2 advances to 3
  
Iteration 2: Compare 1 vs 3
  1 < 3? Yes, attach list1's 1
  dummy(0) -> 1(list2) -> 1(list1) -> 2 -> 4 -> null
  ↑ tail moves to this 1
  list1 advances to 2
  
Iteration 3: Compare 2 vs 3
  2 < 3? Yes, attach list1's 2
  dummy(0) -> 1 -> 1 -> 2 -> 4 -> null
           └─────────────┘↑ tail
  list1 advances to 4
  
Iteration 4: Compare 4 vs 3
  4 < 3? No, attach list2's 3
  dummy(0) -> 1 -> 1 -> 2 -> 3 -> 5 -> null
           └───────────────────────┘↑ tail
  list2 advances to 5
  
Iteration 5: Compare 4 vs 5
  4 < 5? Yes, attach list1's 4
  dummy(0) -> 1 -> 1 -> 2 -> 3 -> 4 -> 5 -> null
           └─────────────────────────────┘↑ tail
  list1 advances to null (exits loop)
  
Post-loop: list1 is null, so skip it
           list2 is null, so skip it

Return dummy.next:
  [1,1,2,3,4,5] ✓
```

### **Example Walkthrough - One Empty**

```
Input: list1 = [], list2 = [1,2]

Initial: dummy -> null, tail = dummy, list1 = null

Loop condition: while (list1 != null && list2 != null)
  list1 is null, loop exits immediately

Post-loop: list1 is null, skip
           list2 is not null, attach it
           tail.next = list2 (which is the node with value 1)
           
Result: dummy -> 1 -> 2 -> null

Return dummy.next:
  [1,2] ✓
```

### **Example Walkthrough - Both Empty**

```
Input: list1 = [], list2 = []

Initial: dummy -> null, tail = dummy, list1 = null, list2 = null

Loop exits immediately (both are null)

Post-loop: both null, skip both

Result: dummy -> null

Return dummy.next = null:
  [] ✓
```

### **Complexity Summary**

- **Time:** O(L + M)
  - Compare loop: O(min(L,M)) iterations
  - Each iteration does constant work (comparison and pointer update)
  - Post-loop attachment: O(max(L,M) - min(L,M)) = O(L or M)
  - Total: O(L + M)
  - Can't do better since we must examine each node at least once

- **Space:** O(1)
  - Only a few pointers (dummy, tail, list1, list2)
  - No extra data structures
  - Reuse existing nodes
  - Constant memory
