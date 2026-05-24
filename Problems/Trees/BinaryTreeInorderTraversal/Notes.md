# Notes - Binary Tree Inorder Traversal

## Pattern / Approach

Recursive inorder traversal: Traverse in the order Left → Root → Right. The helper function recursively visits the left subtree, adds the current node's value, then recursively visits the right subtree. This naturally produces values in inorder sequence.

## Data Structure(s) Used

- **Recursion** — Call stack to traverse the tree
- **Result list** — Accumulator passed down through recursive calls to collect values in order
- **No explicit stack** — Recursion provides the stack implicitly

## Thought Process

**Key insight:** Inorder means "visit left, then self, then right." This maps perfectly to recursive structure: recurse left, process self, recurse right.

**Why this works:**
1. Every subtree is itself a tree that needs inorder traversal
2. Solving a subtree problem is the same as solving the whole problem
3. Base case: null node contributes nothing
4. Inductive case: inorder(node) = inorder(left) + [node.val] + inorder(right)

```
Example: [1,2,3,4,5,6,7]

       1
      / \
     2   3
    / \ / \
   4  5 6  7

Inorder(1) = Inorder(2) + [1] + Inorder(3)
           = (Inorder(4) + [2] + Inorder(5)) + [1] + (Inorder(6) + [3] + Inorder(7))
           = ([] + [4] + []) + [2] + ([] + [5] + []) + [1] + ([] + [6] + []) + [3] + ([] + [7] + [])
           = [4] + [2] + [5] + [1] + [6] + [3] + [7]
           = [4,2,5,1,6,3,7] ✓
```

## Complexity

**Time:** O(N) where N = number of nodes
- Visit each node exactly once
- Each node does constant work (one function call, one list addition)
- Total: O(N)
- Can't do better since we must process every node

**Space:** O(H) where H = height of tree
- Recursion call stack depth = tree height
- Worst case (skewed tree): O(N)
- Best case (balanced tree): O(log N)
- Result list stores N values (output-required, doesn't count against algorithm space)

## Edge Cases

- **Empty tree** (root = null):
  - InorderTraversalSolution returns empty list immediately
  - ✓

- **Single node** ([5]):
  - Traverse(5): left(null) → add 5 → right(null)
  - Result: [5] ✓

- **Left-skewed** ([1,2,null,3]):
  - Traverse(1): Traverse(2): Traverse(3): add 3 → add 2 → add 1
  - Result: [3,2,1] ✓

- **Right-skewed** ([1,null,2,null,3]):
  - Traverse(1): add 1 → Traverse(2): add 2 → Traverse(3): add 3
  - Result: [1,2,3] ✓

- **Balanced** ([1,2,3,4,5,6,7]):
  - Perfect inorder grouping
  - Result: [4,2,5,1,6,3,7] ✓

- **Unbalanced** ([1,2,3,null,4,5]):
  - Complex structure still traversed correctly
  - Result: [2,4,1,5,3] ✓

## Key Insight

**Recursion mirrors the problem structure perfectly!**

Why this approach is elegant:
- **No explicit stack management** — Recursion handles it automatically
- **Code matches definition** — Left-Root-Right order is explicit in code
- **Natural base case** — Null node is trivial
- **No state tracking** — Don't need to track "visited left" or "visiting right"
- **Pass accumulator down** — Result list flows through recursive calls
- **Minimal code** — 4 lines of actual logic

The insight was recognizing that tree traversal is naturally recursive: "traverse this subtree = traverse its left + process it + traverse its right."

## Pattern Recognition for Similar Problems

🚩 **Red flags for TREE TRAVERSAL problems:**
1. **"Inorder traversal"** → LEFT-ROOT-RIGHT RECURSION ⭐⭐⭐
2. **"Preorder traversal"** → ROOT-LEFT-RIGHT recursion
3. **"Postorder traversal"** → LEFT-RIGHT-ROOT recursion
4. **"Any traversal"** → Recursive pattern is natural
5. **"Iterative version required"** → Use explicit stack

**Similar Problems:** "Binary Tree Preorder Traversal" (change order), "Binary Tree Postorder Traversal" (change order), "N-ary Tree Traversal" (same idea, multiple children)

---

## My Solution Approach

### **Step 1: Main Method - Initialize Result**
```csharp
public List<int> InorderTraversalSolution(TreeNode root)
{
    List<int> result = new();
    Traverse(root, result);
    return result;
}
```

Create an empty result list to accumulate traversal values. Call the recursive helper with the root and result list.

**Why separate methods?** Keeps the helper method clean (just recursion logic) and the main method simple (just initialization and return).

### **Step 2: Helper Method - Base Case**
```csharp
private void Traverse(TreeNode node, List<int> res)
{
    if (node == null) return;
    // ...
}
```

If node is null, we've reached a leaf's child. Nothing to do, return and backtrack.

### **Step 3: Traverse Left Subtree**
```csharp
Traverse(node.left, res);
```

Recursively process the entire left subtree. By the time this returns, all left descendants have been added to `res`.

### **Step 4: Process Current Node**
```csharp
res.Add(node.val);
```

Add the current node's value to result. At this point, the entire left subtree has been processed (added to res).

### **Step 5: Traverse Right Subtree**
```csharp
Traverse(node.right, res);
```

Recursively process the entire right subtree. This adds all right descendants after the current node.

### **Step 6: Return (Implicit)**

The function returns (either at base case or after processing both subtrees). Control passes to the parent call.

### **Why This Works**

1. **Recursive structure matches tree structure** — Each call handles one subtree
2. **Order is correct** — Left before root, root before right
3. **Accumulator pattern** — Result list is passed down and filled by each recursive call
4. **Natural base case** — Null node is handled cleanly
5. **No state tracking** — Don't need variables to track "which side am I visiting"

---

## Example Walkthrough - Example 1: [1,2,3,4,5,6,7]

```
Tree:
       1
      / \
     2   3
    / \ / \
   4  5 6  7

Call InorderTraversalSolution(1):
  res = []
  Call Traverse(1, res):
    node=1 != null
    
    Call Traverse(1.left=2, res):
      node=2 != null
      
      Call Traverse(2.left=4, res):
        node=4 != null
        
        Call Traverse(4.left=null, res):
          node==null, return
        
        res.Add(4) → res=[4]
        
        Call Traverse(4.right=null, res):
          node==null, return
      
      res.Add(2) → res=[4,2]
      
      Call Traverse(2.right=5, res):
        node=5 != null
        
        Call Traverse(5.left=null, res):
          return
        
        res.Add(5) → res=[4,2,5]
        
        Call Traverse(5.right=null, res):
          return
    
    res.Add(1) → res=[4,2,5,1]
    
    Call Traverse(1.right=3, res):
      node=3 != null
      
      Call Traverse(3.left=6, res):
        node=6 != null
        
        Call Traverse(6.left=null, res):
          return
        
        res.Add(6) → res=[4,2,5,1,6]
        
        Call Traverse(6.right=null, res):
          return
      
      res.Add(3) → res=[4,2,5,1,6,3]
      
      Call Traverse(3.right=7, res):
        node=7 != null
        
        Call Traverse(7.left=null, res):
          return
        
        res.Add(7) → res=[4,2,5,1,6,3,7]
        
        Call Traverse(7.right=null, res):
          return

Return res = [4,2,5,1,6,3,7]

Final: [4,2,5,1,6,3,7] ✓

Call tree:
Traverse(1)
├─ Traverse(2)
│  ├─ Traverse(4)
│  │  ├─ Traverse(null) → return
│  │  └─ Traverse(null) → return
│  │  → add 4
│  └─ Traverse(5)
│     ├─ Traverse(null) → return
│     └─ Traverse(null) → return
│     → add 5
│  → add 2
├─ Traverse(3)
│  ├─ Traverse(6)
│  │  ├─ Traverse(null) → return
│  │  └─ Traverse(null) → return
│  │  → add 6
│  └─ Traverse(7)
│     ├─ Traverse(null) → return
│     └─ Traverse(null) → return
│     → add 7
│  → add 3
→ add 1

Result order: 4,2,5,1,6,3,7 ✓
```

## Example Walkthrough - Example 2: [1,2,3,null,4,5,null]

```
Tree:
      1
     / \
    2   3
     \ / 
      4 5

Call Traverse(1):
  Traverse(2):
    Traverse(null) → return
    res.Add(2) → res=[2]
    Traverse(4):
      Traverse(null) → return
      res.Add(4) → res=[2,4]
      Traverse(null) → return
  
  res.Add(1) → res=[2,4,1]
  
  Traverse(3):
    Traverse(5):
      Traverse(null) → return
      res.Add(5) → res=[2,4,1,5]
      Traverse(null) → return
    
    res.Add(3) → res=[2,4,1,5,3]
    Traverse(null) → return

Final: [2,4,1,5,3] ✓
```

## Example Walkthrough - Left-Skewed: [1,2,3]

```
Tree:
  1
 /
2
/
3

Call Traverse(1):
  Traverse(2):
    Traverse(3):
      Traverse(null) → return
      res.Add(3) → res=[3]
      Traverse(null) → return
    res.Add(2) → res=[3,2]
    Traverse(null) → return
  
  res.Add(1) → res=[3,2,1]
  Traverse(null) → return

Final: [3,2,1] ✓

All left children processed before nodes added to result.
```

## Example Walkthrough - Single Node: [5]

```
Call Traverse(5):
  Traverse(null) → return
  res.Add(5) → res=[5]
  Traverse(null) → return

Final: [5] ✓
```

---

## Complexity Summary

**Time: O(N)**
- N = total nodes
- Visit each node exactly once
- Each visit: one function call, one list add
- Both are O(1)
- Total: O(N)

**Space: O(H)**
- H = height of tree
- Recursion call stack depth = tree height
- Balanced: O(log N)
- Skewed: O(N)
- Result list: O(N) to store values (output-required)

**Comparison with alternatives:**
- **Recursive (your approach):** O(N) time, O(H) space, simple and elegant
- **Iterative with stack:** O(N) time, O(H) space, more complex but avoids recursion
- **Morris traversal:** O(N) time, O(1) space, very complex, modifies tree

Your recursive solution is the most intuitive. The iterative version (follow-up) would use an explicit stack to simulate the recursion, tracking which phase you're in (visiting left, process node, visiting right).

The beauty of your solution is its simplicity: the code directly reflects the problem definition (left-root-right).
