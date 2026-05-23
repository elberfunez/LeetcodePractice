# Notes - Lowest Common Ancestor in Binary Search Tree

## Pattern / Approach

Iterative BST-aware traversal: Leverage the BST property (left < parent < right) to navigate directly toward the LCA. At each node, check if both p and q are on the same side (left or right). If they're on different sides, the current node IS the LCA. Return immediately when the split point is found.

## Data Structure(s) Used

- **Pointer traversal** — Single `cur` pointer moves through the tree
- **BST ordering** — Comparison of values (p.val, q.val vs cur.val) determines direction
- **No extra structures** — Iterative, no stack or queue

## Thought Process

**Key insight:** In a BST, if p and q are both on the left, the LCA is in the left subtree. If both on the right, LCA is in the right subtree. The moment they split to different sides, I've found the LCA.

**Why this works:**
- LCA is the first (lowest) node where p and q diverge
- In a BST, divergence happens when one value is on left and one is on right
- OR when the current node IS one of p or q

**The three cases:**
1. Both > cur → both in right subtree → go right
2. Both < cur → both in left subtree → go left
3. Neither of above → cur is the LCA (split point or cur is p or q)

```
Example: root=[5,3,8,1,4,7,9,null,2], p=3, q=8

        5 ← Start here
       / \
      3   8 ← p is on left, q is on right
     / \
    1   4
     \
      2

At node 5:
  p.val (3) < 5? Yes
  q.val (8) > 5? Yes
  They're on different sides → 5 is the LCA!

Why? Because 5 is the lowest node where the paths to p and q diverge.
```

## Complexity

**Time:** O(H) where H = height of the tree
- At most H iterations (depth of tree)
- Each iteration: constant time comparisons and pointer movement
- Best case: O(1) if LCA is root
- Worst case: O(N) if tree is skewed (height = N)
- Average case (balanced BST): O(log N)
- Much better than general tree (which would be O(N))

**Space:** O(1)
- Only use a single pointer `cur`
- Iterative approach, no call stack
- No additional data structures
- Constant memory

This is optimal for a BST!

## Edge Cases

- **LCA is root** ([5,3,8], p=3, q=8):
  - At root: 3 < 5, 8 > 5 (different sides)
  - return root ✓

- **One node is ancestor of other** ([5,3,8,1,4], p=3, q=4):
  - At root: 3 < 5, 4 < 5 (both left)
  - cur = 3
  - At node 3: 3 == 3, 4 > 3 (neither condition matches)
  - return node 3 ✓

- **p and q are siblings** ([5,3,8,1,4,7,9], p=1, q=4):
  - At root: 1 < 5, 4 < 5 (both left)
  - cur = 3
  - At node 3: 1 < 3, 4 > 3 (different sides)
  - return node 3 ✓

- **LCA is a leaf's parent** ([5,3,8,1,4,7,9,2], p=1, q=2):
  - Both descend from 1
  - Eventually: at node 1: 1 == 1, 2 > 1 (neither condition)
  - return node 1 ✓

- **Deep tree** ([5,3,8,1,4,null,9,null,2], p=2, q=4):
  - Navigate down through multiple levels
  - Stop when split point found
  - ✓

## Key Insight

**The BST property is a superpower!**

Why this approach is brilliant:
- **Leverages BST structure** — No need to search everywhere like in a general tree
- **Single pass** — O(H) instead of O(N)
- **Iterative elegance** — No recursion needed
- **Early termination** — Stops at first split point
- **Constant space** — Just one pointer
- **Intuitive logic** — Follow the values like a treasure map

A general binary tree approach would need to:
- Check both subtrees for p and q
- Combine results from left and right
- O(N) time, O(H) space for recursion

Your BST approach is much more efficient!

## Pattern Recognition for Similar Problems

🚩 **Red flags for BST problems:**
1. **"Find LCA in BST"** → VALUE-BASED NAVIGATION ⭐⭐⭐
2. **"Use BST property to optimize"** → Compare values, not recursion
3. **"Binary search in tree"** → Think value-based path
4. **"Single pass needed"** → BST property allows it
5. **"Space-efficient tree traversal"** → Iterative value-based navigation

**Similar Problems:** "LCA in Binary Tree (general)" (must recurse, no property), "Kth Smallest in BST" (value-based traversal), "Validate BST" (value comparisons)

---

## My Solution Approach

### **Step 1: Start at Root**
```csharp
TreeNode cur = root;
```

Begin traversal from the root. We'll move cur through the tree following value comparisons.

### **Step 2: Loop While Node Exists**
```csharp
while (cur != null)
{
    // Navigation logic
}
```

Continue until we find the LCA (or reach null, though guaranteed to find it).

### **Step 3: Check if Both Are in Right Subtree**
```csharp
if (p.val > cur.val && q.val > cur.val)
{
    cur = cur.right;
}
```

If both p's and q's values are greater than current node's value, both must be in the right subtree. Navigate right.

**Why both conditions?** We need to ensure BOTH are on the right. If only one were, that node would be on different sides and might be the LCA.

### **Step 4: Check if Both Are in Left Subtree**
```csharp
else if (p.val < cur.val && q.val < cur.val)
{
    cur = cur.left;
}
```

If both p's and q's values are less than current node's value, both must be in the left subtree. Navigate left.

### **Step 5: Found the LCA**
```csharp
else
{
    return cur;
}
```

If neither condition matched, we're at the LCA because:
- **p and q are on different sides** of cur (one left, one right)
- **OR cur equals p or q** (a node is its own ancestor)
- **OR cur is between p and q** in value (if p < cur < q, cur is the split point)

Return immediately without further traversal.

### **Step 6: Return null (Safety)**
```csharp
return null;
```

Guaranteed not to reach this per problem constraints (p and q both exist in BST).

### **Why This Works**

1. **BST property ensures correctness** — Value ordering guarantees correct path
2. **Single pass suffices** — No need to visit both subtrees
3. **Efficient navigation** — Each step eliminates half the tree (like binary search)
4. **Handles all cases** — Both in left, both in right, or split

---

## Example Walkthrough - Example 1: root=[5,3,8,1,4,7,9,null,2], p=3, q=8

```
Tree structure:
       5
      / \
     3   8
    / \ / \
   1  4 7  9
    \
     2

Call LowestCommonAncestorSolution(root=[5...], p=3, q=8):
  cur = 5
  
  Iteration 1: cur = 5
    p.val (3) > cur.val (5)? No
    p.val (3) < cur.val (5)? Yes
    q.val (8) < cur.val (5)? No
    
    Check first condition: 3 > 5 && 8 > 5? No
    Check second condition: 3 < 5 && 8 < 5? No
    
    Neither condition matches!
    return cur = 5 ✓

Result: 5 ✓

Why: p=3 is on the LEFT of 5, q=8 is on the RIGHT of 5
     5 is the split point → it's the LCA
     No further navigation needed!
```

## Example Walkthrough - Example 2: root=[5,3,8,1,4,7,9,null,2], p=3, q=4

```
Tree structure:
       5
      / \
     3   8
    / \ / \
   1  4 7  9
    \
     2

Call LowestCommonAncestorSolution(root=[5...], p=3, q=4):
  cur = 5
  
  Iteration 1: cur = 5
    p.val (3) > 5 && q.val (4) > 5? 3 > 5 && 4 > 5? No
    p.val (3) < 5 && q.val (4) < 5? 3 < 5 && 4 < 5? Yes!
    
    cur = cur.left = 3
  
  Iteration 2: cur = 3
    p.val (3) > 3 && q.val (4) > 3? 3 > 3 && 4 > 3? No
    p.val (3) < 3 && q.val (4) < 3? 3 < 3 && 4 < 3? No
    
    Neither condition matches!
    return cur = 3 ✓

Result: 3 ✓

Why: Both p=3 and q=4 are < 5, so go left to node 3
     At node 3: p=3 is AT node 3, q=4 is to the RIGHT of node 3
     Node 3 is the LCA because it's the ancestor of both
     (3 is ancestor of itself, 3 is ancestor of 4)
```

## Example Walkthrough - Both in Right Subtree: p=7, q=9

```
Tree structure:
       5
      / \
     3   8
    / \ / \
   1  4 7  9
    \
     2

Call LowestCommonAncestorSolution(root=[5...], p=7, q=9):
  cur = 5
  
  Iteration 1: cur = 5
    p.val (7) > 5 && q.val (9) > 5? 7 > 5 && 9 > 5? Yes!
    
    cur = cur.right = 8
  
  Iteration 2: cur = 8
    p.val (7) > 8 && q.val (9) > 8? 7 > 8 && 9 > 8? No
    p.val (7) < 8 && q.val (9) < 8? 7 < 8 && 9 < 8? No
    
    Neither condition matches!
    return cur = 8 ✓

Result: 8 ✓

Why: Both p=7 and q=9 are > 5, so go right to node 8
     At node 8: p=7 is to the LEFT, q=9 is to the RIGHT
     Node 8 is the split point → it's the LCA
```

## Example Walkthrough - One Node on Each Side: p=1, q=4

```
Tree structure:
       5
      / \
     3   8
    / \ / \
   1  4 7  9
    \
     2

Call LowestCommonAncestorSolution(root=[5...], p=1, q=4):
  cur = 5
  
  Iteration 1: cur = 5
    p.val (1) > 5 && q.val (4) > 5? No (both < 5)
    p.val (1) < 5 && q.val (4) < 5? Yes!
    
    cur = cur.left = 3
  
  Iteration 2: cur = 3
    p.val (1) > 3 && q.val (4) > 3? 1 > 3 && 4 > 3? No
    p.val (1) < 3 && q.val (4) < 3? 1 < 3 && 4 < 3? No
    
    Neither condition matches!
    return cur = 3 ✓

Result: 3 ✓

Why: At node 3, p=1 is on LEFT (1 < 3), q=4 is on RIGHT (4 > 3)
     They split at node 3 → node 3 is the LCA
```

---

## Complexity Summary

**Time: O(H) where H = height**
- Balanced BST: O(log N)
- Skewed BST: O(N)
- Each iteration eliminates a subtree by navigating in one direction
- Much better than general tree approach O(N)

**Space: O(1)**
- Only one pointer: `cur`
- No recursion, no call stack
- No additional data structures
- Iterative approach is memory-optimal

**Comparison with General Tree LCA:**
- General tree: Must check entire tree O(N), space O(H) for recursion
- BST: Can navigate efficiently O(H), space O(1)
- Your approach leverages the BST structure perfectly!

The beauty of your solution is that it recognizes the problem property (BST) and uses it to achieve optimal efficiency with minimal code.
