# Notes - Binary Tree Level Order Traversal

## Pattern / Approach

Classic BFS (Breadth-First Search) level-order traversal using a queue: Process nodes level by level from top to bottom. The critical trick is capturing the queue length at the start of each level—this tells us how many nodes are at that level. Process exactly that many nodes, adding their children for the next level, then add the level's values to the result.

## Data Structure(s) Used

- **Queue** — Stores nodes waiting to be processed, naturally maintains level order
- **List<List<int>>** — Outer list holds levels, inner lists hold values at each level
- **Current level list** — Temporary list for collecting values before adding to result

## Thought Process

**Key insight:** The queue automatically arranges nodes by level due to FIFO (First In, First Out) behavior. If I know how many nodes are at the current level, I can process exactly that many, and their children will be added at the end of the queue for the next level.

**Why this works:**
1. Queue starts with just root
2. I know queue has 1 node (level 1)
3. Process 1 node, add its 2 children to queue (now has 2 nodes)
4. Next iteration: queue has 2 nodes (level 2)
5. Process 2 nodes, add their children (could be 0-4 nodes)
6. Continue until queue is empty

**The breakthrough:** Capturing `queueLength = queue.Count` at the start of each while iteration tells me exactly how many nodes belong to the current level.

```
Example: [1,2,3,4,5,6,7]

       1
      / \
     2   3
    / \ / \
   4  5 6  7

Queue progression:
Initial: [1]

Level 1 (queueLength=1):
  Process 1: add 2, 3
  Queue: [2, 3]
  Result: [[1]]

Level 2 (queueLength=2):
  Process 2: add 4, 5
  Process 3: add 6, 7
  Queue: [4, 5, 6, 7]
  Result: [[1], [2,3]]

Level 3 (queueLength=4):
  Process 4, 5, 6, 7: add no children
  Queue: []
  Result: [[1], [2,3], [4,5,6,7]]

Queue empty, done!
```

## Complexity

**Time:** O(N) where N = number of nodes
- Visit each node exactly once
- Each node is enqueued once and dequeued once
- Processing each node (add to list, check children, enqueue children) is O(1)
- Total: O(N)

**Space:** O(W) where W = maximum width of tree
- Queue holds at most all nodes at one level
- For a complete binary tree: W = N/2 (half the nodes)
- For a skewed tree: W = 1
- For a very wide tree: W could approach N
- Result list also stores all N values, but that's output-required

## Edge Cases

- **Empty tree** (root = null):
  - First check catches this: return empty list immediately
  - ✓

- **Single node** ([1]):
  - queue has 1 node
  - queueLength = 1, process node 1, add no children
  - Result: [[1]] ✓

- **Left-skewed tree** ([1,2,3]):
  - Level 1: [1]
  - Level 2: [2]
  - Level 3: [3]
  - Result: [[1], [2], [3]] ✓

- **Right-skewed tree** ([1,null,2,null,3]):
  - Level 1: [1]
  - Level 2: [2]
  - Level 3: [3]
  - Result: [[1], [2], [3]] ✓

- **Balanced tree** ([1,2,3,4,5,6,7]):
  - Levels group correctly: [[1], [2,3], [4,5,6,7]] ✓

- **Wide tree** ([1,2,3,4,5,6,7,8,9,10,11,12,13,14,15]):
  - Last level has 8 nodes
  - queueLength = 8 processes all correctly ✓

## Key Insight

**The queue length trick is genius!**

Why this approach is elegant:
- **No explicit level tracking** — Don't need a "depth" or "level" variable
- **Automatic level grouping** — Queue FIFO naturally groups by level
- **Simple loop** — One while loop, one for loop
- **Single pass** — O(N) visits each node exactly once
- **Memory efficient** — No recursion, O(W) space instead of O(H)

The insight was recognizing that queue.Count at the start of each iteration IS the level size. Most people manually track depth or use sentinel markers—you use a simple counter.

## Pattern Recognition for Similar Problems

🚩 **Red flags for LEVEL-ORDER/BFS problems:**
1. **"Level order traversal"** → QUEUE + LEVEL LENGTH ⭐⭐⭐
2. **"Group by level"** → Save queue.Count, process that many
3. **"Print each level"** → Build temporary list, add to result
4. **"Vertical order"** → Similar queueLength technique
5. **"Right view of tree"** → Process levels, track last node

**Similar Problems:** "Binary Tree Right Side View" (same BFS, take last node of each level), "Average of Levels in Binary Tree" (sum/count at each level), "N-ary Tree Level Order Traversal" (same idea, multiple children)

---

## My Solution Approach

### **Step 1: Handle Empty Tree**
```csharp
if (root == null) return new List<List<int>>();
```

Empty tree → return empty result.

### **Step 2: Initialize Data Structures**
```csharp
List<List<int>> res = new();
Queue<TreeNode> queue = new Queue<TreeNode>();
queue.Enqueue(root);
```

Create result list and queue, start BFS from root.

### **Step 3: Process Level by Level**
```csharp
while (queue.Count > 0)
{
    int queueLength = queue.Count;
    List<int> curLevelVals = new();
    
    for (int i = 0; i < queueLength; i++)
    {
        // Process nodes
    }
    
    res.Add(curLevelVals);
}
```

**Critical step:** `int queueLength = queue.Count` captures the current level's node count BEFORE we start dequeuing.

Why? Because as we dequeue nodes at this level, we enqueue their children (next level). By saving the count first, we know exactly how many to process.

### **Step 4: Process Nodes at Current Level**
```csharp
for (int i = 0; i < queueLength; i++)
{
    TreeNode curNode = queue.Dequeue();
    curLevelVals.Add(curNode.val);
    
    if (curNode.left != null) queue.Enqueue(curNode.left);
    if (curNode.right != null) queue.Enqueue(curNode.right);
}
```

For each node at this level:
- Dequeue the node
- Add its value to current level list
- Enqueue its children (they'll be processed next level)

### **Step 5: Add Level to Result**
```csharp
res.Add(curLevelVals);
```

After processing all nodes at this level, add the level's values to the result.

### **Step 6: Return Result**
```csharp
return res;
```

Return the nested list of levels.

### **Why This Works**

1. **Queue maintains level order** — FIFO naturally processes top-to-bottom
2. **queueLength snapshots** — Captures level size before children are added
3. **One level per while loop** — Clean separation of concerns
4. **No special markers or sentinels** — Just a simple counter
5. **Efficient** — O(N) time, O(W) space, single pass

---

## Example Walkthrough - Example 1: [1,2,3,4,5,6,7]

```
Tree:
       1
      / \
     2   3
    / \ / \
   4  5 6  7

Step 1: root != null, continue

Step 2: Initialize
  res = []
  queue = [1]

Step 3-6: Level 1 Processing
  while (queue.Count > 0)? Yes (1 node)
  
  queueLength = 1
  curLevelVals = []
  
  For i=0:
    curNode = Dequeue() = 1
    curLevelVals = [1]
    1.left = 2, Enqueue(2) → queue = [2]
    1.right = 3, Enqueue(3) → queue = [2, 3]
  
  res.Add([1]) → res = [[1]]

Step 3-6: Level 2 Processing
  while (queue.Count > 0)? Yes (2 nodes)
  
  queueLength = 2
  curLevelVals = []
  
  For i=0:
    curNode = Dequeue() = 2
    curLevelVals = [2]
    2.left = 4, Enqueue(4) → queue = [3, 4]
    2.right = 5, Enqueue(5) → queue = [3, 4, 5]
  
  For i=1:
    curNode = Dequeue() = 3
    curLevelVals = [2, 3]
    3.left = 6, Enqueue(6) → queue = [4, 5, 6]
    3.right = 7, Enqueue(7) → queue = [4, 5, 6, 7]
  
  res.Add([2, 3]) → res = [[1], [2, 3]]

Step 3-6: Level 3 Processing
  while (queue.Count > 0)? Yes (4 nodes)
  
  queueLength = 4
  curLevelVals = []
  
  For i=0:
    curNode = Dequeue() = 4
    curLevelVals = [4]
    4.left = null, skip
    4.right = null, skip
    queue = [5, 6, 7]
  
  For i=1:
    curNode = Dequeue() = 5
    curLevelVals = [4, 5]
    5.left = null, skip
    5.right = null, skip
    queue = [6, 7]
  
  For i=2:
    curNode = Dequeue() = 6
    curLevelVals = [4, 5, 6]
    6.left = null, skip
    6.right = null, skip
    queue = [7]
  
  For i=3:
    curNode = Dequeue() = 7
    curLevelVals = [4, 5, 6, 7]
    7.left = null, skip
    7.right = null, skip
    queue = []
  
  res.Add([4, 5, 6, 7]) → res = [[1], [2, 3], [4, 5, 6, 7]]

Step 3: while (queue.Count > 0)? No (queue empty, exit)

Step 6: return res

Final: [[1], [2, 3], [4, 5, 6, 7]] ✓

Visual queue progression:
Initial:              [1]
After level 1:        [2, 3]
After level 2:        [4, 5, 6, 7]
After level 3:        []
Done!
```

## Example Walkthrough - Example 2: [1]

```
Tree: 1 (single node)

Step 1: root != null

Step 2: Initialize
  res = []
  queue = [1]

Step 3-6: Level 1
  queueLength = 1
  curLevelVals = []
  
  For i=0:
    curNode = Dequeue() = 1
    curLevelVals = [1]
    1.left = null, skip
    1.right = null, skip
    queue = []
  
  res.Add([1]) → res = [[1]]

Step 3: while (queue.Count > 0)? No (queue empty)

Final: [[1]] ✓
```

## Example Walkthrough - Example 3: []

```
Tree: empty

Step 1: root == null
  return new List<List<int>>()

Final: [] ✓
(No further steps needed)
```

## Example Walkthrough - Left-skewed: [1,2,3]

```
Tree:
  1
 /
2
/
3

Step 2: queue = [1]

Level 1:
  queueLength = 1
  Process 1: Enqueue(2)
  res = [[1]]
  queue = [2]

Level 2:
  queueLength = 1
  Process 2: Enqueue(3)
  res = [[1], [2]]
  queue = [3]

Level 3:
  queueLength = 1
  Process 3: no children
  res = [[1], [2], [3]]
  queue = []

Final: [[1], [2], [3]] ✓
```

---

## Complexity Summary

**Time: O(N)**
- N = total nodes
- Visit each node exactly once
- Each node: dequeue (O(1)), add to list (O(1)), check/enqueue children (O(1))
- Total: O(N)

**Space: O(W)**
- W = maximum width of tree (most nodes at any level)
- Queue holds at most W nodes at once
- Best case (skewed): W = 1
- Worst case (complete): W ≈ N/2
- Result list: O(N) to store all values, but that's output-required

**Comparison with alternatives:**
- Recursive DFS: O(N) time, O(H) space for call stack
- Your iterative BFS: O(N) time, O(W) space
- For wide, shallow trees: BFS is better (W < H)
- For tall, narrow trees: recursive DFS is better (H < W)
- Your approach is cleaner and more intuitive for level-order problems

The beauty of your solution is the simplicity: one while loop, one for loop, and the clever use of queue.Count to implicitly track level boundaries.
