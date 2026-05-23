# Notes - Valid Binary Search Tree

## Pattern / Approach

Range-based recursive validation: Instead of checking just parent-child relationships, track the valid range [min, max) that each node must fall within. As you recurse, tighten the range: left children must be in [min, node.val), right children must be in [node.val, max). If any node violates its range, return false immediately.

## Data Structure(s) Used

- **Recursion** — Call stack to traverse and validate tree
- **Range parameters (min, max)** — Track valid bounds for each subtree
- **long type** — Use long.MinValue/MaxValue to handle edge cases with Integer.MIN_VALUE/-MAX_VALUE nodes

## Thought Process

**Key insight:** The naive approach checks if left < parent and right > parent, but that's not enough. A node in a left subtree must be less than NOT JUST its parent, but ALL ancestors above it.

**Example of the problem:**
```
    5
   / \
  1   6
     /
    4

At node 4:
- 4 < 6? Yes ✓
- But 4 < 5? No ✗
- Node 4 is in left subtree of 5, so must be < 5
- Simple parent check fails here!
```

**The breakthrough:** Instead of tracking parent values, track the valid range. Each node must be within its range.

```
    5                  Range: (-∞, ∞)
   / \
  1   6              Left: (-∞, 5)  Right: (5, ∞)
 / \ / \
0  3 4  7           

At node 4:
  Must be in range (5, ∞)? No, 4 < 5 ✗
  Immediately return false!
```

**Why this works:**
1. Every ancestor constraint is encoded in the range
2. Node's value must be strictly within [min, max)
3. When going left: new_max = node.val (tighten upper bound)
4. When going right: new_min = node.val (tighten lower bound)
5. If any node violates its range, BST property is broken

## Complexity

**Time:** O(N) where N = number of nodes
- Visit each node exactly once
- Each node does constant work (range check, two recursive calls)
- Total: O(N)
- Best case: O(N) still (must check all nodes)
- No early exit that saves time

**Space:** O(H) where H = height of tree
- Recursion call stack depth = tree height
- Worst case (skewed tree): O(N)
- Best case (balanced tree): O(log N)
- No additional data structures beyond recursion

## Edge Cases

- **Empty tree** (root = null):
  - IsValidBSTSolution calls Validate(null, ...)
  - Validate returns true (base case)
  - Result: true ✓

- **Single node** ([5]):
  - Validate(5, long.MinValue, long.MaxValue)
  - 5 in range? Yes (MinValue < 5 < MaxValue)
  - Both children null → true
  - Result: true ✓

- **Node with value Integer.MIN_VALUE** ([MIN_VALUE]):
  - Must use long.MinValue as initial min, not int.MinValue
  - Otherwise would fail range check
  - Result: true ✓

- **Node with value Integer.MAX_VALUE** ([MAX_VALUE]):
  - Must use long.MaxValue as initial max
  - Result: true ✓

- **Valid BST** ([2,1,3]):
  - Node 2: in (-∞, ∞)? Yes
  - Node 1: in (-∞, 2)? Yes
  - Node 3: in (2, ∞)? Yes
  - Result: true ✓

- **Invalid: left > parent** ([1,2,3]):
  - Node 1: in (-∞, ∞)? Yes
  - Node 2: in (-∞, 1)? No, 2 > 1
  - Return false ✓

- **Invalid: right in left subtree** ([5,1,6,null,null,4,7]):
  - Node 4: in (5, ∞)? No, 4 < 5
  - Return false ✓

- **Left-skewed valid** ([3,2,null,1]):
  - All nodes validated with tightening upper bounds
  - Result: true ✓

- **Right-skewed valid** ([1,null,2,null,3]):
  - All nodes validated with tightening lower bounds
  - Result: true ✓

## Key Insight

**The range-tracking trick is elegant!**

Why this approach is superior:
- **Encodes all ancestor constraints** — Range represents the path's constraints
- **Clean validation** — Single check `min < val < max` covers everything
- **Recursive elegance** — Each level tightens the range naturally
- **No parent tracking** — Don't need to store parent references
- **Handles edge values** — long.MinValue/MaxValue prevent boundary errors
- **Early termination** — Stops at first violation

The insight was recognizing that "valid for subtree" means "valid within a range" not just "valid with immediate parent."

## Pattern Recognition for Similar Problems

🚩 **Red flags for BST VALIDATION problems:**
1. **"Is this a valid BST?"** → RANGE VALIDATION ⭐⭐⭐
2. **"Check all ancestor constraints"** → Range approach handles this
3. **"Recursive subtree validation"** → Range narrows at each level
4. **"Edge cases with MIN/MAX values"** → Use long for boundaries
5. **"Efficient single-pass check"** → Range validation is O(N) optimal

**Similar Problems:** "Validate BST and count nodes" (same approach, add counter), "Insert into BST" (similar range logic), "Kth Smallest in BST" (different approach, but BST knowledge helpful)

---

## My Solution Approach

### **Step 1: Main Method Entry Point**
```csharp
public bool IsValidBSTSolution(TreeNode root)
{
    return Validate(root, long.MinValue, long.MaxValue);
}
```

Call the recursive helper with the root and initial range: (-∞, ∞). Every node in the tree must fall within this range (which they do).

**Why long.MinValue/MaxValue?** Because node values can be int.MinValue or int.MaxValue. Using long prevents boundary issues.

### **Step 2: Base Case - Null Node**
```csharp
if (node == null) return true;
```

Empty trees are valid BSTs. This is the recursion terminator.

### **Step 3: Check Current Node**
```csharp
if (node.val <= min || node.val >= max) return false;
```

Node must be STRICTLY within its range:
- `node.val <= min`: Node equals or is less than lower bound → invalid
- `node.val >= max`: Node equals or exceeds upper bound → invalid

**Why strict inequalities (<=, >=)?** BST requires strict ordering: left < parent < right, not ≤ or ≥. This prevents duplicate values (which some BST definitions allow, but LeetCode doesn't).

### **Step 4: Validate Left Subtree**
```csharp
bool leftValid = Validate(node.left, min, node.val);
```

Left subtree must satisfy: min < all nodes < node.val

New range: [min, node.val) — node.val becomes the new upper bound (all left descendants must be < node.val).

### **Step 5: Validate Right Subtree**
```csharp
bool rightValid = Validate(node.right, node.val, max);
```

Right subtree must satisfy: node.val < all nodes < max

New range: (node.val, max] — node.val becomes the new lower bound (all right descendants must be > node.val).

### **Step 6: Return Combined Result**
```csharp
return leftValid && rightValid;
```

Node is valid only if BOTH subtrees are valid AND the node itself passed range check.

### **Why This Works**

1. **Range encodes constraints** — Each node's valid range represents all ancestor requirements
2. **Tight bounds** — Range becomes increasingly restrictive as we descend
3. **Early exit** — Return false immediately if any node violates its range
4. **Recursive structure** — Naturally validates entire tree by validating subtrees
5. **Handles all cases** — Edge values, duplicates, skewed trees all work

---

## Example Walkthrough - Example 1: [2,1,3]

```
Tree:
    2
   / \
  1   3

Call IsValidBSTSolution(2):
  return Validate(2, long.MinValue, long.MaxValue)

Call Validate(node=2, min=MinValue, max=MaxValue):
  node != null? Yes
  2 <= MinValue || 2 >= MaxValue? No (2 is in valid range)
  
  Left subtree: Validate(1, MinValue, 2)
    node=1, min=MinValue, max=2
    node != null? Yes
    1 <= MinValue || 1 >= 2? No (1 < 2, in range)
    
    Left: Validate(null, MinValue, 1) → true
    Right: Validate(null, 1, 2) → true
    return true && true = true
  
  Right subtree: Validate(3, 2, MaxValue)
    node=3, min=2, max=MaxValue
    node != null? Yes
    3 <= 2 || 3 >= MaxValue? No (3 > 2, in range)
    
    Left: Validate(null, 2, 3) → true
    Right: Validate(null, 3, MaxValue) → true
    return true && true = true
  
  return true && true = true

Final: true ✓

Range validation:
  Node 2: Range (-∞, ∞) ✓
  Node 1: Range (-∞, 2) ✓
  Node 3: Range (2, ∞) ✓
```

## Example Walkthrough - Example 2: [1,2,3]

```
Tree:
    1
   / \
  2   3

Call IsValidBSTSolution(1):
  return Validate(1, long.MinValue, long.MaxValue)

Call Validate(node=1, min=MinValue, max=MaxValue):
  node != null? Yes
  1 <= MinValue || 1 >= MaxValue? No (1 is valid)
  
  Left subtree: Validate(2, MinValue, 1)
    node=2, min=MinValue, max=1
    node != null? Yes
    2 <= MinValue || 2 >= 1? YES! (2 >= 1 is true)
    return false ✗
  
  leftValid = false
  (rightValid not evaluated due to short-circuit AND, but would be true)
  
  return false && rightValid = false

Final: false ✓

Why: Node 2 is in left subtree of 1, so must be < 1
     But 2 > 1, violates BST property
     Caught immediately by range check: 2 >= 1
```

## Example Walkthrough - Invalid Right in Left: [5,1,6,null,null,4,7]

```
Tree:
       5
      / \
     1   6
        / \
       4   7

Call Validate(5, MinValue, MaxValue):
  5 in range? Yes
  
  Left: Validate(1, MinValue, 5)
    1 in range (-∞, 5)? Yes
    Left: Validate(null) → true
    Right: Validate(null) → true
    return true
  
  Right: Validate(6, 5, MaxValue)
    6 in range (5, ∞)? Yes
    
    Left: Validate(4, 5, 6)
      4 in range (5, 6)? NO! 4 <= 5
      return false ✗
    
    leftValid = false
    return false

Final: false ✓

Why: Node 4 is in right subtree of 5 (range must be > 5)
     But 4 < 5, violates BST property
     Caught by range check: 4 <= 5
```

## Example Walkthrough - Single Node with Extreme Value: [Integer.MIN_VALUE]

```
Call Validate(MinValue, long.MinValue, long.MaxValue):
  MinValue != null? Yes
  MinValue <= long.MinValue || MinValue >= long.MaxValue?
    MinValue <= long.MinValue? Yes (they're equal)
    Return false ✗

Wait, this would fail! But it shouldn't...

Actually, int.MinValue is -2147483648
long.MinValue is -9223372036854775808

So: -2147483648 <= -9223372036854775808? No!
    -2147483648 >= long.MaxValue? No!
    
So the check passes ✓
The node is valid because we used long boundaries!

Final: true ✓

This is why long.MinValue/MaxValue are critical!
```

## Example Walkthrough - Skewed Tree: [1,null,2,null,3]

```
Tree:
1
 \
  2
   \
    3

Call Validate(1, MinValue, MaxValue):
  1 in range? Yes
  Left: Validate(null) → true
  Right: Validate(2, 1, MaxValue)
    2 in range (1, ∞)? Yes
    Left: Validate(null) → true
    Right: Validate(3, 2, MaxValue)
      3 in range (2, ∞)? Yes
      Left: Validate(null) → true
      Right: Validate(null) → true
      return true
    return true
  return true

Final: true ✓

Each level narrows the lower bound:
  Node 1: Range (MinValue, MaxValue)
  Node 2: Range (1, MaxValue)
  Node 3: Range (2, MaxValue)
All valid!
```

---

## Complexity Summary

**Time: O(N)**
- N = total nodes
- Visit each node exactly once
- Each node: constant work (range check, two recursive calls)
- No early exit saves time (must validate entire tree)
- Total: O(N)

**Space: O(H)**
- H = height of tree
- Recursion call stack depth = tree height
- Balanced tree: O(log N)
- Skewed tree: O(N)
- No additional data structures beyond recursion

**Why range validation beats alternatives:**
- **In-order traversal approach:** O(N) time, O(H) space, but less intuitive
- **Parent tracking:** O(N) time, O(H) space, but needs to remember parent
- **Your approach:** O(N) time, O(H) space, cleaner and more elegant

Your solution leverages the beautiful insight that "valid for subtree" = "within range." This makes the code simple, efficient, and correct.
