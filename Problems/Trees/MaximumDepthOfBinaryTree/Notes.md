# Notes - Maximum Depth of Binary Tree

## Pattern / Approach

Two distinct approaches:

**1. BFS (Level-Order Traversal):** Process the tree level by level using a queue. Increment depth counter for each level. When queue empties, we've counted all levels.

**2. DFS (Recursive):** For each node, recursively find the maximum depth of left and right subtrees, then return 1 + max of those two depths.

## Data Structure(s) Used

**BFS:**
- **Queue** — Process nodes level by level
- **Depth counter** — Track current level number

**DFS:**
- **Recursion** — Call stack implicitly stores parent references
- **Math.Max** — Compare left and right subtree depths

## Thought Process

**BFS insight:** The depth is the number of levels in the tree. If I traverse level by level and count how many levels I visit, that's the depth.

**DFS insight:** The depth of a tree is 1 + the maximum depth of its two subtrees. A null node has depth 0 (base case).

```
Example tree: [1,2,3,null,null,4]

        1           ← Level 1 (depth so far: 1)
       / \
      2   3         ← Level 2 (depth so far: 2)
         /
        4           ← Level 3 (depth so far: 3)

BFS perspective: Count levels → 3 levels → depth = 3
DFS perspective: 
  - Node 1: 1 + max(depth(2), depth(3))
  - depth(2) = 1 (no children, so 1 + 0)
  - depth(3) = 1 + max(depth(4), depth(null)) = 1 + max(1, 0) = 2
  - So depth(1) = 1 + max(1, 2) = 3 ✓
```

## Complexity

### **BFS Approach**
**Time:** O(N) where N = number of nodes
- Visit each node exactly once
- Queue operations (enqueue/dequeue) are O(1) per node
- Total: O(N)

**Space:** O(W) where W = maximum width of the tree
- Queue stores at most all nodes at one level
- Worst case (complete binary tree): O(N/2) = O(N)
- Best case (skewed tree): O(1)
- Typically better than DFS for very deep trees with few nodes per level

### **DFS Approach**
**Time:** O(N) where N = number of nodes
- Visit each node exactly once
- Each node computed once, no redundant work
- Total: O(N)

**Space:** O(H) where H = height of the tree
- Recursion call stack depth = height of tree
- Worst case (skewed tree): O(N)
- Best case (balanced tree): O(log N)
- Typically better than BFS for wide trees with limited depth

## Edge Cases

- **Empty tree** (root = null):
  - BFS: returns 0 immediately
  - DFS: returns 0 immediately (base case)
  - ✓

- **Single node** ([5]):
  - BFS: one level → depth = 1
  - DFS: 1 + max(0, 0) = 1
  - ✓

- **Left-skewed tree** ([1,2,3]):
  - BFS: processes 3 levels → depth = 3
  - DFS: recursively finds 3 levels
  - ✓

- **Right-skewed tree** ([1,null,2,null,3]):
  - BFS: processes 3 levels → depth = 3
  - DFS: recursively finds 3 levels
  - ✓

- **Balanced tree** ([1,2,3,4,5,6,7]):
  - BFS: 3 levels
  - DFS: 3 levels
  - ✓

## Key Insight

**Both approaches visit every node once (O(N) time), but differ in space usage:**

- **BFS better for:** Wide trees with limited depth (uses only W space for width)
- **DFS better for:** Tall trees with limited width (uses only H space for height)
- **DFS advantage:** Simple recursive code, no explicit queue needed
- **BFS advantage:** Explicit level-by-level processing, good for finding level-specific info

The choice depends on tree shape and memory constraints, not correctness.

## Pattern Recognition for Similar Problems

🚩 **Red flags for DEPTH/HEIGHT problems:**
1. **"Find depth/height of tree"** → BFS or recursive DFS ⭐⭐⭐
2. **"Count levels"** → BFS with level counter
3. **"Maximum/Minimum at each level"** → BFS (level-aware)
4. **"Path from root to leaf"** → DFS (stack-based)
5. **"Number of leaves/nodes"** → Both work, DFS simpler

**Similar Problems:** "Maximum Depth of N-ary Tree" (same idea, more children), "Balanced Binary Tree" (compare left/right depths), "Binary Tree Level Order Traversal" (explicit BFS)

---

## My Solution Approach

---

## **BFS Solution (Level-Order Traversal)**

### **Step 1: Handle Empty Tree**
```csharp
if (root == null) return 0;
```

Empty tree has depth 0.

### **Step 2: Initialize Queue and Depth Counter**
```csharp
Queue<TreeNode> q = new();
q.Enqueue(root);
int depth = 0;
```

Start with root in queue. Depth counter begins at 0, will increment for each level.

### **Step 3: Process Level by Level**
```csharp
while (q.Count > 0)
{
    depth++;
    int qLength = q.Count;
    
    for (int i = 0; i < qLength; i++)
    {
        TreeNode current = q.Dequeue();
        if (current.left != null) q.Enqueue(current.left);
        if (current.right != null) q.Enqueue(current.right);
    }
}
```

**Critical insight:** `qLength = q.Count` captures all nodes at CURRENT level BEFORE we dequeue them. This lets us:
1. Increment depth once per level (not per node)
2. Process exactly one level per while iteration
3. Enqueue children without affecting current level's count

**Loop flow:**
- Save current level's node count
- Dequeue all nodes at that level
- Enqueue their children
- Loop increments depth when while condition is checked again

### **Step 4: Return Depth**
```csharp
return depth;
```

At this point, depth = number of levels visited.

### **BFS Walkthrough - [1,2,3,null,null,4]**

```
Initial tree:
        1
       / \
      2   3
         /
        4

Step 1: Check if root null? No

Step 2: Initialize
  Queue: [1]
  depth: 0

Step 3: While loop - Level 1
  depth++ → depth = 1
  qLength = 1 (only node 1)
  
  Iteration 1: Dequeue 1
    Enqueue 1.left (2): Queue: [2]
    Enqueue 1.right (3): Queue: [2, 3]
  
  Queue now: [2, 3] (next level)

Step 4: While loop - Level 2
  depth++ → depth = 2
  qLength = 2 (nodes 2 and 3)
  
  Iteration 1: Dequeue 2
    2.left = null, skip
    2.right = null, skip
    Queue: [3]
  
  Iteration 2: Dequeue 3
    Enqueue 3.left (4): Queue: [4]
    3.right = null, skip
    Queue: [4]
  
  Queue now: [4] (next level)

Step 5: While loop - Level 3
  depth++ → depth = 3
  qLength = 1 (only node 4)
  
  Iteration 1: Dequeue 4
    4.left = null, skip
    4.right = null, skip
    Queue: []
  
  Queue now: empty

Step 6: While condition false (q.Count = 0), exit loop

Return depth = 3 ✓

Visual progression:
Level 1:  [1]           → depth=1, enqueue children
Level 2:  [2,3]         → depth=2, enqueue children
Level 3:  [4]           → depth=3, no children
         [] (empty)     → loop exits, return 3
```

### **BFS Walkthrough - Empty Tree []**

```
Step 1: root == null? Yes
Return 0 immediately

Result: 0 ✓
```

### **BFS Walkthrough - Single Node [5]**

```
Step 1: root == null? No

Step 2: Initialize
  Queue: [5]
  depth: 0

Step 3: While loop - Level 1
  depth++ → depth = 1
  qLength = 1
  
  Dequeue 5:
    5.left = null, skip
    5.right = null, skip
    Queue: []

Step 4: While condition false, exit

Return depth = 1 ✓
```

---

## **DFS Solution (Recursive)**

### **Step 1: Base Case - Null Node**
```csharp
if (root == null) return 0;
```

A null node contributes 0 to depth. This stops recursion.

### **Step 2: Recursively Find Left and Right Depths**
```csharp
int maxDepthLeftSide = MaxDepth(root.left);
int maxDepthRightSide = MaxDepth(root.right);
```

Recursively ask: "What's the maximum depth of my left subtree?" and "What's the maximum depth of my right subtree?"

Each recursive call eventually hits a null node (base case) and returns 0.

### **Step 3: Calculate This Node's Depth**
```csharp
return 1 + Math.Max(maxDepthLeftSide, maxDepthRightSide);
```

Current node contributes 1. Add the larger of the two subtree depths.

### **Why This Works**

- **Base case:** null → 0 (no nodes below)
- **Leaf node:** 1 + max(0, 0) = 1 (itself)
- **Internal node:** 1 + max(left_depth, right_depth) (itself + deeper subtree)

### **DFS Walkthrough - [1,2,3,null,null,4]**

```
Initial tree:
        1
       / \
      2   3
         /
        4

Call MaxDepth(1):
  1 != null, so continue
  
  Call MaxDepth(1.left) = MaxDepth(2):
    2 != null, so continue
    
    Call MaxDepth(2.left) = MaxDepth(null):
      null, return 0
    
    Call MaxDepth(2.right) = MaxDepth(null):
      null, return 0
    
    return 1 + max(0, 0) = 1 ← Node 2's depth
  
  Call MaxDepth(1.right) = MaxDepth(3):
    3 != null, so continue
    
    Call MaxDepth(3.left) = MaxDepth(4):
      4 != null, so continue
      
      Call MaxDepth(4.left) = MaxDepth(null):
        null, return 0
      
      Call MaxDepth(4.right) = MaxDepth(null):
        null, return 0
      
      return 1 + max(0, 0) = 1 ← Node 4's depth
    
    Call MaxDepth(3.right) = MaxDepth(null):
      null, return 0
    
    return 1 + max(1, 0) = 2 ← Node 3's depth (node 4 is deeper)
  
  maxDepthLeftSide = 1
  maxDepthRightSide = 2
  return 1 + max(1, 2) = 3 ← Node 1's depth

Result: 3 ✓

Call stack visualization:
MaxDepth(1)
├─ MaxDepth(2)
│  ├─ MaxDepth(null) → 0
│  └─ MaxDepth(null) → 0
│  return 1 + max(0,0) = 1
└─ MaxDepth(3)
   ├─ MaxDepth(4)
   │  ├─ MaxDepth(null) → 0
   │  └─ MaxDepth(null) → 0
   │  return 1 + max(0,0) = 1
   └─ MaxDepth(null) → 0
   return 1 + max(1,0) = 2
return 1 + max(1,2) = 3
```

### **DFS Walkthrough - Empty Tree []**

```
Call MaxDepth(null):
  null, return 0

Result: 0 ✓
```

### **DFS Walkthrough - Single Node [5]**

```
Call MaxDepth(5):
  5 != null, so continue
  
  Call MaxDepth(null):
    return 0
  
  Call MaxDepth(null):
    return 0
  
  return 1 + max(0, 0) = 1

Result: 1 ✓
```

---

## **Comparison Diagram**

```
For tree [1,2,3,null,null,4]:

BFS (Level-by-level):          DFS (Recursive, top-down):
                               
Queue progression:             Call stack progression:
  [1]          depth=1         MaxDepth(1)
  [2,3]        depth=2           |
  [4]          depth=3           ├─ MaxDepth(2) → 1
  []           return 3          └─ MaxDepth(3)
                                   |
Processing order:                  ├─ MaxDepth(4) → 1
1 → 2 → 3 → 4                      └─ 2
                               
Horizontal (wide trees):       Vertical (deep trees):
Space ∝ Width (W)              Space ∝ Height (H)
```

---

## **Complexity Summary**

### **BFS**
- **Time:** O(N) — visit each node once
- **Space:** O(W) — queue holds at most one level's width
  - Best: O(1) for highly skewed tree
  - Worst: O(N) for complete/wide tree
- **Better for:** Wide trees with limited depth

### **DFS**
- **Time:** O(N) — visit each node once
- **Space:** O(H) — recursion stack depth
  - Best: O(log N) for balanced tree
  - Worst: O(N) for completely skewed tree
- **Better for:** Tall/skewed trees with limited width

### **Code Simplicity**
- **DFS:** 4 lines, very clean, natural recursion
- **BFS:** 10 lines, explicit queue management, good for level info

Both are correct and optimal. Choose based on tree structure and problem requirements.
