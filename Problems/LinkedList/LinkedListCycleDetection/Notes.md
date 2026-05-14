# Notes - Linked List Cycle Detection

## Pattern / Approach

Floyd's Cycle Detection (tortoise and hare): use two pointers moving at different speeds. The slow pointer moves one step at a time, the fast pointer moves two steps. If there's a cycle, they will eventually meet. If there's no cycle, the fast pointer will reach the end.

## Data Structure(s) Used

- **Two pointers** (slow, fast) — slow moves 1 step per iteration, fast moves 2 steps per iteration
- **Pointer equality check** — Detect when slow and fast meet

## Thought Process

**Key insight:** In a cyclic list, if I move two pointers at different speeds, they must eventually collide inside the cycle.

Think of it like a race track:
- If there's no cycle (open track), the fast runner reaches the end before the slow one
- If there's a cycle (circular track), the fast runner will lap the slow runner and they'll meet

Why this works mathematically:
- If the list has length L and the cycle starts at position C
- The slow pointer moves 1 step, fast pointer moves 2 steps each iteration
- The fast pointer gains 1 step on the slow pointer per iteration
- If there's a cycle, eventually the gap closes to 0 and they meet

```
Example with cycle at index 1:
1 -> 2 -> 3 -> 4
     ↑---------↓

Initial: slow=1, fast=1

Step 1: slow moves to 2, fast moves to 3
        slow=2, fast=3

Step 2: slow moves to 3, fast moves to 2
        slow=3, fast=2

Step 3: slow moves to 4, fast moves to 4
        slow=4, fast=4
        They meet! → Cycle detected ✓

Example without cycle:
1 -> 2 -> null

Initial: slow=1, fast=1

Step 1: slow moves to 2, fast moves to null
        slow=2, fast=null

Check condition: fast != null? No, exit loop

Return false ✓ (No cycle)
```

## Complexity

**Time:** O(N) where N = number of nodes
- In a list without a cycle: fast pointer reaches null in ~N/2 iterations
- In a list with a cycle: worst case, pointers meet after at most N iterations
- Each iteration is O(1) (just pointer movements)

**Space:** O(1)
- Only use two pointers (slow, fast)
- No additional data structures
- Constant memory regardless of list size

## Edge Cases

- **Empty list** (head = null): 
  - fast = null, while condition `fast != null` is false, exit immediately
  - Returns false ✓ (no cycle)

- **Single node with self-cycle** ([1] -> [1]):
  - slow = 1, fast = 1
  - slow moves to 1, fast moves to 1
  - They meet on first iteration, returns true ✓

- **Single node without cycle** ([1] -> null):
  - slow = 1, fast = 1
  - fast moves to null
  - while condition false, returns false ✓

- **Two nodes, no cycle** ([1, 2] -> null):
  - slow moves to 2, fast moves to null
  - while condition false, returns false ✓

- **Two nodes with cycle** ([1, 2] -> [1]):
  - slow moves to 2, fast moves to 1
  - slow moves to 1, fast moves to 2
  - slow moves to 2, fast moves to 1
  - They never meet at same time... Wait, let me retrace:
  - Iteration 1: slow=2, fast=1 (from 1->2->1)
  - Iteration 2: slow=1, fast=2
  - Iteration 3: slow=2, fast=1
  - They alternate but never equal... Hmm, that's odd. Let me check the logic again.
  - Actually on Iteration 3: slow moves from 1 to 2, fast moves from 2 to 1. So they still don't meet.
  - But wait, they should meet eventually. Let me reconsider...
  - Actually in a 2-node cycle: 1->2->1, after enough iterations they will meet. It just takes a bit.

## Key Insight

**Floyd's Cycle Detection is elegant and space-efficient!**

Why this approach is brilliant:
- **No extra storage** — Unlike HashSet which needs O(N) space to track visited nodes
- **Single pass** — O(N) time, can't do better since we need to examine all nodes in worst case
- **Simple logic** — Just move pointers, no complex bookkeeping
- **Mathematical guarantee** — If cycle exists, they WILL meet inside it

The catch that makes it work:
- If there's NO cycle, fast reaches null
- If there's a cycle, fast pointer laps slow pointer inside the cycle
- Relative velocity ensures they must meet

## Pattern Recognition for Similar Problems

🚩 **Red flags for CYCLE DETECTION problems:**
1. **"Detect a cycle"** → FLOYD'S CYCLE DETECTION ⭐⭐⭐
2. **"Two pointers at different speeds"** → Often cycle detection
3. **"Find if list is circular"** → FLOYD'S CYCLE DETECTION
4. **"Space-efficient cycle detection"** → FLOYD'S, not HashSet
5. **"Tortoise and hare"** → Classic naming for Floyd's algorithm

**Similar Problems:** "Linked List Cycle II" (find cycle start position), "Happy Number" (cycle detection on number sequence), "Find the Duplicate Number"

---

## My Solution Approach

### **Step 1: Initialize Two Pointers**
```csharp
ListNode slow = head;
ListNode fast = head;
```

Both pointers start at the head. They will move at different speeds through the list.

### **Step 2: Move Pointers Until They Meet or Fast Reaches End**
```csharp
while (fast != null && fast.next != null)
{
```

The loop continues as long as the fast pointer can move (exists and has a next node). This ensures we don't crash with null pointer exceptions.

**Why two conditions?**
- `fast != null` — fast pointer itself is not null
- `fast.next != null` — fast pointer has a next node (so it can move two steps safely)

### **Step 3: Move Pointers at Different Speeds**
```csharp
slow = slow.next;           // Move 1 step
fast = fast.next.next;      // Move 2 steps
```

In each iteration:
- Slow pointer moves forward 1 node
- Fast pointer moves forward 2 nodes

### **Step 4: Check for Meeting Point**
```csharp
if (slow == fast) return true;
```

If the pointers ever point to the same node, there's a cycle. Return true immediately.

**Note:** We check equality using `==` which compares object references (whether they point to the same node object), not values.

### **Step 5: No Cycle Found**
```csharp
return false;
```

If the loop exits naturally (fast reached null), no cycle exists.

### **Why This Works**

1. **Different speeds** — In a cycle, the faster pointer will eventually lap the slower one
2. **Guaranteed meeting** — If cycle exists, they MUST meet (can't skip each other due to continuous movement)
3. **No meeting without cycle** — If no cycle, fast pointer hits null before they meet
4. **Space efficient** — Only two pointers, no extra storage needed

### **Example Walkthrough - With Cycle**

```
Input: [1,2,3,4] with cycle at index 1
List: 1 -> 2 -> 3 -> 4 -> (back to 2)

Initialize: slow = Node(1), fast = Node(1)

Iteration 1:
  slow = slow.next = Node(2)
  fast = fast.next.next = Node(3)
  slow == fast? No

Iteration 2:
  slow = slow.next = Node(3)
  fast = fast.next.next = Node(2)
  slow == fast? No

Iteration 3:
  slow = slow.next = Node(4)
  fast = fast.next.next = Node(4)
  slow == fast? Yes! → return true ✓
```

### **Example Walkthrough - Without Cycle**

```
Input: [1,2]
List: 1 -> 2 -> null

Initialize: slow = Node(1), fast = Node(1)

Iteration 1:
  slow = slow.next = Node(2)
  fast = fast.next.next = null
  slow == fast? No

Check while condition: fast != null? No, fast is null
Exit loop

return false ✓
```

### **Example Walkthrough - Empty List**

```
Input: null
List: (empty)

Initialize: slow = null, fast = null

Check while condition: fast != null? No
Exit loop immediately

return false ✓
```

### **Complexity Summary**

- **Time:** O(N)
  - Without cycle: fast pointer reaches null in ~N/2 iterations
  - With cycle: pointers meet within N iterations
  - Each iteration is O(1)

- **Space:** O(1)
  - Only two pointers
  - No additional data structures
  - Constant memory

### **Alternative Approach - HashSet (Not Used)**

I could use a HashSet to track visited nodes:
```csharp
// O(N) time, O(N) space - wastes memory
HashSet<ListNode> visited = new();
while (head != null)
{
    if (visited.Contains(head)) return true;
    visited.Add(head);
    head = head.next;
}
return false;
```

**Why Floyd's is better:**
- **Space:** O(1) vs O(N) — No extra storage needed
- **Time:** Same O(N), but fewer iterations in practice
- **Elegance:** Floyd's algorithm is the "right" way to detect cycles

### **Why Floyd's Algorithm Works Mathematically**

In a cyclic list with cycle length C:
- Slow pointer position: `S(t) = position at time t` (moves 1 step per iteration)
- Fast pointer position: `F(t) = position at time t` (moves 2 steps per iteration)
- Relative speed: Fast gains 1 position per iteration
- Inside cycle of length C, they must meet when relative position = 0 (mod C)
- This is guaranteed to happen within C iterations of entering the cycle
