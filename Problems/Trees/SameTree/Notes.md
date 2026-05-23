# Notes - Same Tree

## Pattern / Approach

Recursive structural comparison: simultaneously check that two trees have the same structure and the same values. At each node pair, verify: (1) neither is null, (2) values match, (3) left subtrees match, (4) right subtrees match. Return false immediately on any mismatch.

## Data Structure(s) Used

- **Recursion** — Call stack implicitly traverses both trees in parallel
- **Boolean short-circuit logic** — Early exit on mismatch saves unnecessary recursive calls

## Thought Process

**Key insight:** Two trees are "the same" if:
1. Structure matches (same null/non-null positions at each level)
2. Values match at corresponding positions

I can check both conditions simultaneously by recursing through both trees together. At each step:
- If both are null, they're the same subtree (base case)
- If only one is null, structures don't match (mismatch)
- If values differ, nodes don't match (mismatch)
- Otherwise, check if both subtrees recursively match

```
Comparison process for [1,2,3] vs [1,2,3]:

        1                1
       / \              / \
      2   3            2   3
      
Compare node 1 vs node 1:
  Both exist ✓
  Values match (1==1) ✓
  Recurse on left (2 vs 2) and right (3 vs 3)
  
Compare node 2 vs node 2:
  Both exist ✓
  Values match (2==2) ✓
  Recurse on left (null vs null) and right (null vs null)
  
Compare null vs null:
  Both null ✓
  Return true
  
All comparisons pass → return true ✓
```

## Complexity

**Time:** O(min(N, M)) where N = nodes in p, M = nodes in q
- In best case (early mismatch): O(1) if roots differ
- In worst case (identical or near-identical trees): O(min(N,M)) as we must check all comparable nodes
- Short-circuit AND prevents checking deeper if mismatch found

**Space:** O(min(H1, H2)) where H1 = height of p, H2 = height of q
- Recursion call stack depth = max height traversed
- In case of mismatch, might not traverse full height
- Balanced trees: O(log min(N,M))
- Skewed trees: O(min(N,M))

## Edge Cases

- **Both empty** (null, null):
  - First check catches this: return true immediately
  - ✓

- **One empty** ([1,2], null):
  - Second check catches: p != null, q == null → return false
  - ✓

- **Same values, different structure** ([4,7], [4,null,7]):
  - Roots match (4==4)
  - Left subtrees: 7 vs null → mismatch → return false
  - ✓

- **Same structure, different values** ([1,2,3], [1,3,2]):
  - Roots match (1==1)
  - Left subtrees: 2 vs 3 → values don't match → return false
  - ✓

- **Single node, same** ([5], [5]):
  - Nodes match (5==5)
  - Both have null children
  - Return true ✓

- **Single node, different** ([5], [6]):
  - Nodes don't match (5!=6) → return false
  - ✓

## Key Insight

**Recursion checking both trees in parallel is elegant!**

Why this approach works:
- **Simultaneous traversal** — Checks structure and values together
- **Early exit** — Boolean AND stops on first mismatch (short-circuit evaluation)
- **Simple base cases** — null/null is same, any single null is different
- **Natural recursion** — Each subtree is itself a "same tree" problem
- **No explicit queues or stacks** — Recursion handles tree navigation

The code is clean because the recursive structure mirrors the problem structure: "Are these two trees the same? Yes, if their roots match AND their left subtrees match AND their right subtrees match."

## Pattern Recognition for Similar Problems

🚩 **Red flags for TREE COMPARISON problems:**
1. **"Are two trees the same?"** → RECURSIVE PARALLEL TRAVERSAL ⭐⭐⭐
2. **"Are two trees structurally identical?"** → Check structure only (ignore values)
3. **"One tree is subtree of another?"** → More complex, iterate one tree and call IsSameTree at each node
4. **"Compare two trees level-by-level"** → BFS both in parallel
5. **"Find differences between trees"** → Modify to return difference count or list

**Similar Problems:** "Subtree of Another Tree" (use IsSameTree as helper), "Symmetric Tree" (compare node with its mirror), "Merge Two Binary Trees" (combine while comparing)

---

## My Solution Approach

### **Step 1: Base Case - Both Null**
```csharp
if (p == null && q == null) return true;
```

Two null references are trivially the same (empty subtree = empty subtree).

### **Step 2: Structure Mismatch Check**
```csharp
if (p == null || q == null) return false;
```

If exactly one is null, the structures differ (one has a node, the other doesn't at that position).

### **Step 3: Value Check**
```csharp
if (p.val != q.val) return false;
```

Both nodes exist, but if their values don't match, the trees aren't the same.

### **Step 4: Recursive Subtree Comparison**
```csharp
return IsSameTreeSolution(p.left, q.left) && IsSameTreeSolution(p.right, q.right);
```

Both nodes exist and match. Now check if the subtrees match:
- Left subtrees must be the same
- AND right subtrees must be the same

Both conditions must be true (AND operator), and both are checked due to C#'s left-to-right evaluation (though && is short-circuit, both are typically evaluated).

### **Why This Works**

1. **All cases covered** — null/null, one null, value mismatch, recursive match
2. **Order of checks matters** — Null checks first (avoid NullReferenceException on p.val)
3. **Short-circuit helps** — Early return on any mismatch saves recursion
4. **Recursive structure mirrors problem** — "Same tree" problem reduces to "same left subtree" + "same right subtree"

---

## Example Walkthrough - Example 1: [1,2,3] vs [1,2,3]

```
p = [1,2,3]          q = [1,2,3]
     1                    1
    / \                  / \
   2   3                2   3

Call IsSameTree(p=1, q=1):
  p==null && q==null? No
  p==null || q==null? No
  p.val != q.val? 1 != 1? No
  
  Recurse: IsSameTree(p.left=2, q.left=2) AND IsSameTree(p.right=3, q.right=3)
  
  Call IsSameTree(p=2, q=2):
    p==null && q==null? No
    p==null || q==null? No
    p.val != q.val? 2 != 2? No
    
    Recurse: IsSameTree(null, null) AND IsSameTree(null, null)
    
    Call IsSameTree(null, null):
      p==null && q==null? Yes
      return true
    
    return true AND true = true
  
  Call IsSameTree(p=3, q=3):
    p==null && q==null? No
    p==null || q==null? No
    p.val != q.val? 3 != 3? No
    
    Recurse: IsSameTree(null, null) AND IsSameTree(null, null)
    
    Call IsSameTree(null, null):
      return true (base case)
    
    return true AND true = true
  
  return true AND true = true

Result: true ✓

Call tree:
IsSameTree(1,1)
├─ IsSameTree(2,2) → true
│  ├─ IsSameTree(null,null) → true
│  └─ IsSameTree(null,null) → true
└─ IsSameTree(3,3) → true
   ├─ IsSameTree(null,null) → true
   └─ IsSameTree(null,null) → true
```

## Example Walkthrough - Example 2: [4,7] vs [4,null,7]

```
p = [4,7]            q = [4,null,7]
    4                    4
   /                      \
  7                        7

Call IsSameTree(p=4, q=4):
  p==null && q==null? No
  p==null || q==null? No
  p.val != q.val? 4 != 4? No
  
  Recurse: IsSameTree(p.left=7, q.left=null) AND IsSameTree(p.right=null, q.right=7)
  
  Call IsSameTree(p=7, q=null):
    p==null && q==null? No (p is not null)
    p==null || q==null? Yes (q is null)
    return false ✗
  
  Short-circuit: AND sees false, doesn't even evaluate right side
  return false

Result: false ✓

The mismatch is caught immediately: 7 exists on left in p, but is null on left in q.
```

## Example Walkthrough - Example 3: [1,2,3] vs [1,3,2]

```
p = [1,2,3]          q = [1,3,2]
     1                    1
    / \                  / \
   2   3                3   2

Call IsSameTree(p=1, q=1):
  p==null && q==null? No
  p==null || q==null? No
  p.val != q.val? 1 != 1? No
  
  Recurse: IsSameTree(p.left=2, q.left=3) AND IsSameTree(p.right=3, q.right=2)
  
  Call IsSameTree(p=2, q=3):
    p==null && q==null? No
    p==null || q==null? No
    p.val != q.val? 2 != 3? Yes
    return false ✗
  
  Short-circuit: AND sees false from left comparison
  return false

Result: false ✓

The values at the same positions don't match: 2 vs 3 at left position.
```

## Example Walkthrough - Both Empty []

```
p = null             q = null

Call IsSameTree(null, null):
  p==null && q==null? Yes
  return true

Result: true ✓
```

---

## Complexity Summary

- **Time:** O(min(N, M))
  - Best case: O(1) if roots differ
  - Worst case: O(min(N,M)) if trees are identical or nearly identical
  - Short-circuit AND provides early exit on mismatch

- **Space:** O(min(H1, H2))
  - Call stack depth = height of recursion
  - Balanced trees: O(log min(N,M))
  - Skewed trees: O(min(N,M))
  - No additional data structures

The recursive approach is optimal for this problem — simple, clean, and efficient.