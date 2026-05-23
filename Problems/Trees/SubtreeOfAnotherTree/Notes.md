# Notes - Subtree of Another Tree

## Pattern / Approach

Two-step recursive approach: (1) For each node in root, check if subRoot matches at that position using a SameTree helper, (2) If no match, recursively search the left and right subtrees. The key insight is that a subtree exists somewhere in root if either it matches at the current node OR it exists in the left subtree OR it exists in the right subtree.

## Data Structure(s) Used

- **Recursion** — Two levels: outer loop traverses root, inner SameTree compares node-by-node
- **Boolean short-circuit (OR)** — Stop searching once a match is found
- **SameTree helper** — Reuses identical tree comparison logic

## Thought Process

**Key insight:** A subtree doesn't have to start at the root of root. It can start at ANY node in root. So I need to:
1. Try matching subRoot at the current node
2. If it doesn't match, search for it in the left and right subtrees

**Why this works:** Every possible subtree in root has a root node somewhere. I'm checking every possible starting point.

```
Example: root = [1,2,3,4,5], subRoot = [2,4,5]

        1 ← Check if [2,4,5] matches here? No
       / \
      2   3 ← Check if [2,4,5] matches here? Yes! Found it ✓
     / \
    4   5

If 2 didn't match, I would recursively check:
- Left subtree of 2 (rooted at 4)
- Right subtree of 2 (rooted at 5)
- Left subtree of 1 (rooted at 2) ← Would find it here
- Right subtree of 1 (rooted at 3)
```

## Complexity

**Time:** O(N * M) where N = nodes in root, M = nodes in subRoot
- Worst case: Check every node in root (O(N))
- For each node, run SameTree comparison (O(M))
- Total: O(N * M)
- Best case: O(M) if subRoot matches root immediately
- Average case: Better than worst if match found early

**Space:** O(H_root + H_subRoot) where H = height
- Recursion stack depth for IsSubtreeSolution: O(H_root)
- Recursion stack depth for SameTree: O(H_subRoot)
- At most both are on stack simultaneously
- Worst case (skewed trees): O(N + M)
- Best case (balanced trees): O(log N + log M)

## Edge Cases

- **Empty subRoot** (root = [1,2,3], subRoot = null):
  - First check: subRoot == null → return true
  - Empty tree is a subtree of everything
  - ✓

- **Empty root, non-empty subRoot** (root = null, subRoot = [1]):
  - Second check: root == null → return false
  - Can't find a tree in an empty root
  - ✓

- **subRoot matches entire root** ([1,2,3], [1,2,3]):
  - SameTree matches at root immediately
  - return true ✓

- **subRoot is in left subtree only** ([1,2,3,4,5], [2,4,5]):
  - root doesn't match, check left
  - Finds [2,4,5] at node 2
  - return true ✓

- **subRoot is in right subtree only** ([1,2,3,null,null,4,5], [3,4,5]):
  - root doesn't match, check left (fails)
  - Check right, finds [3,4,5] at node 3
  - return true ✓

- **subRoot structure matches but has extra node** ([1,2,3,4,5,null,null,6], [2,4,5]):
  - [2,4,5] doesn't match [2,4,5,null,null,6] (node 4 has extra child 6)
  - SameTree returns false at node 4
  - Continues searching, doesn't find match elsewhere
  - return false ✓

- **Leaf node subtree** ([1,2,3], [3]):
  - Finds leaf node 3 matches [3]
  - return true ✓

## Key Insight

**Reusing the SameTree helper is elegant!**

Why this approach works:
- **Separation of concerns** — SameTree handles comparison, IsSubtreeSolution handles search
- **Reuse from previous problem** — Leverages "Same Tree" logic without duplication
- **Try all starting points** — Every node is a potential subtree root
- **Early exit** — OR short-circuits when first match found
- **Recursive elegance** — Searching left/right subtrees uses same logic as main problem

The insight was recognizing that "finding a subtree" = "finding ANY node where the trees match" → try every node with SameTree.

## Pattern Recognition for Similar Problems

🚩 **Red flags for SUBTREE/CONTAIN problems:**
1. **"Is X a subtree of Y?"** → TRY ALL NODES + HELPER ⭐⭐⭐
2. **"Check at this node, else search children"** → Recursive pattern
3. **"Reuse tree comparison logic"** → Helper function (SameTree)
4. **"Search through structure while comparing"** → Two-level recursion
5. **"Match can be at any position"** → Check every node in outer structure

**Similar Problems:** "Same Tree" (helper used here), "Check if Tree is Balanced" (check each node + recurse), "Invert Binary Tree" (process each node)

---

## My Solution Approach

### **Step 1: Base Case - Empty SubRoot**
```csharp
if (subRoot == null) return true;
```

An empty tree is trivially a subtree of any tree. By definition, a subtree is "a node and all its descendants" — with null, there's nothing, which is valid.

### **Step 2: Base Case - Empty Root**
```csharp
if (root == null) return false;
```

If root is empty but subRoot is not, root can't contain subRoot. There's nowhere to find it.

### **Step 3: Check Current Node**
```csharp
if (SameTree(root, subRoot)) return true;
```

Does the tree rooted at root exactly match subRoot? If yes, found it! Return immediately.

### **Step 4: Search Left Subtree**
```csharp
else
{
    return IsSubtreeSolution(root.left, subRoot) || IsSubtreeSolution(root.right, subRoot);
}
```

If not at current node, search recursively:
- Check if subRoot is a subtree of root's left child
- OR check if subRoot is a subtree of root's right child

The OR operator short-circuits: if left subtree contains it, we return true without checking right.

### **Step 5: SameTree Helper**
```csharp
public bool SameTree(TreeNode p, TreeNode q)
{
    if (p == null && q == null) return true;
    if (p != null && q == null) return false;
    if (p.val != q.val) return false;
    
    bool leftSideSame = SameTree(p.left, q.left);
    bool rightSideSame = SameTree(p.right, q.right);
    
    return leftSideSame && rightSideSame;
}
```

This is the "Same Tree" solution from the previous problem. It checks if two trees are structurally identical with matching values:
1. Both null → same
2. One null → different
3. Values differ → different
4. Recursively check both subtrees

### **Why This Works**

1. **Every subtree has a root** — By checking every node in root with SameTree, we check all possible subtree positions
2. **SameTree ensures exact match** — Both structure AND values must match
3. **Recursive search is exhaustive** — If subRoot exists in root, we'll find it
4. **Short-circuits save time** — OR and early returns prevent unnecessary work
5. **Reuse prevents duplication** — SameTree logic is proven and reused

---

## Example Walkthrough - Example 1: root=[1,2,3,4,5], subRoot=[2,4,5]

```
root:              subRoot:
    1                  2
   / \                / \
  2   3              4   5
 / \

Call IsSubtreeSolution(1, [2,4,5]):
  subRoot == null? No
  root == null? No
  SameTree(1, [2,4,5])?
    Compare node 1 with node 2
    1.val (1) != 2.val (2)? Yes
    return false
  
  SameTree returned false, check children:
  return IsSubtreeSolution(1.left=[2], [2,4,5]) || IsSubtreeSolution(1.right=[3], [2,4,5])
  
  Call IsSubtreeSolution(2, [2,4,5]):
    subRoot == null? No
    root == null? No
    SameTree(2, [2,4,5])?
      Compare node 2 with node 2
      2.val == 2.val ✓
      
      Compare left: SameTree(4, 4)?
        4.val == 4.val ✓
        SameTree(null, null) → true
        SameTree(null, null) → true
        return true
      
      Compare right: SameTree(5, 5)?
        5.val == 5.val ✓
        SameTree(null, null) → true
        SameTree(null, null) → true
        return true
      
      leftSideSame = true && rightSideSame = true
      return true ✓
    
    return true (found match at node 2!)

Result: true ✓

Tree shows the match:
    1
   / \
  2*  3        ← Node 2 matches [2,4,5] exactly
 / \
4   5
```

## Example Walkthrough - Example 2: root=[1,2,3,4,5,null,null,6], subRoot=[2,4,5]

```
root:                   subRoot:
      1                     2
     / \                   / \
    2   3                 4   5
   / \  /
  4   5 6

Call IsSubtreeSolution(1, [2,4,5]):
  SameTree(1, [2,4,5])?
    1 != 2, return false
  
  Check children:
  IsSubtreeSolution(2, [2,4,5]) || IsSubtreeSolution(3, [2,4,5])
  
  Call IsSubtreeSolution(2, [2,4,5]):
    SameTree(2, [2,4,5])?
      2 == 2 ✓
      Compare left: SameTree(4, 4)?
        4 == 4 ✓
        SameTree(null, null) → true
        SameTree(null, null) → true
        return true
      
      Compare right: SameTree(5, 5)?
        5 == 5 ✓
        SameTree(null, null) → true
        SameTree(null, null) → true
        return true
      
      Looks like match! Let's verify...
      Actually wait, checking root's structure more carefully:
      root = [1,2,3,4,5,null,null,6]
      
      Level-order: 1, then 2,3, then 4,5,null (which is child of 2), then 6 (child of 4)
      
      So:
         1
        / \
       2   3
      / \  /
     4   5 6
     
     Wait, node 6 is a child of node 4. Let me re-read.
     
     Actually [1,2,3,4,5,null,null,6]:
     Position 0: 1 (root)
     Position 1,2: 2,3 (children of 1)
     Position 3,4: 4,5 (children of 2)
     Position 5,6: null,null (children of 3)
     Position 7: 6 (child of node at position 3, which is 4)
     
     Tree:
         1
        / \
       2   3
      / \
     4   5
     /
    6
    
    So node 4 HAS a child (6), but subRoot [2,4,5] doesn't expect node 4 to have a left child.
    
    Call IsSubtreeSolution(2, [2,4,5]):
      SameTree(2, [2,4,5])?
        2 == 2 ✓
        
        Compare left: SameTree(4, 4)?
          4 == 4 ✓
          Compare left: SameTree(6, null)?
            6 != null (structure mismatch)
            return false ✗
        
        leftSideSame = false
        return false (tree rooted at 2 doesn't match [2,4,5])
      
      Check children of 2:
      IsSubtreeSolution(4, [2,4,5]) || IsSubtreeSolution(5, [2,4,5])
      
      Call IsSubtreeSolution(4, [2,4,5]):
        SameTree(4, [2,4,5])?
          4 != 2, return false
        
        Check children of 4:
        IsSubtreeSolution(6, [2,4,5]) || IsSubtreeSolution(null, [2,4,5])
        
        Call IsSubtreeSolution(6, [2,4,5]):
          SameTree(6, [2,4,5])?
            6 != 2, return false
          IsSubtreeSolution(null, [2,4,5]) → returns false
          IsSubtreeSolution(null, [2,4,5]) → returns false
          return false || false = false
        
        So IsSubtreeSolution(4, [2,4,5]) = false
      
      Similar checks for remaining nodes all return false
  
  IsSubtreeSolution(3, [2,4,5]) also returns false
  
Result: false ✓

The problem: node 4 in root has an extra child (6) not in subRoot.
SameTree catches this mismatch when comparing structures.
```

## Example Walkthrough - Single Node Match: root=[1,2,3], subRoot=[3]

```
root:          subRoot:
    1              3
   / \

Call IsSubtreeSolution(1, [3]):
  SameTree(1, [3])?
    1 != 3, return false
  
  IsSubtreeSolution(1.left=[2], [3]) || IsSubtreeSolution(1.right=[3], [3])
  
  Call IsSubtreeSolution(2, [3]):
    SameTree(2, [3])? 2 != 3, false
    IsSubtreeSolution(null, [3]) || IsSubtreeSolution(null, [3])
    Both return false
    return false
  
  Call IsSubtreeSolution(3, [3]):
    SameTree(3, [3])?
      3 == 3 ✓
      SameTree(null, null) → true
      SameTree(null, null) → true
      return true ✓
    
    return true (found leaf node 3!)

Result: true ✓
```

## Example Walkthrough - Empty SubRoot: root=[1,2,3], subRoot=null

```
Call IsSubtreeSolution([1,2,3], null):
  subRoot == null? Yes
  return true immediately

Result: true ✓

No further traversal needed.
```

---

## Complexity Summary

- **Time:** O(N * M)
  - N = nodes in root, M = nodes in subRoot
  - Worst case: Visit all N nodes, and at each call SameTree which is O(M)
  - Best case: O(M) if first node matches
  - Early exit via short-circuit helps average case

- **Space:** O(H_root + H_subRoot)
  - IsSubtreeSolution recursion stack: O(H_root)
  - SameTree recursion stack: O(H_subRoot)
  - Both can be on stack simultaneously
  - Worst case (skewed): O(N + M)
  - Best case (balanced): O(log N + log M)

The solution is straightforward and efficient for the problem constraints (≤100 nodes). The O(N*M) time is acceptable because checking each position is necessary to ensure no subtree is missed.
