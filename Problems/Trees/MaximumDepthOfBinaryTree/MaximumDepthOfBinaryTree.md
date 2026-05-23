# Maximum Depth of Binary Tree

**Difficulty:** Easy  
**Topic:** Trees

---

## Description

Given the root of a binary tree, return its depth.

The depth of a binary tree is defined as the number of nodes along the longest path from the root node down to the farthest leaf node.

---

## Examples

### Example 1:

```
Input: root = [1,2,3,null,null,4]

Output: 3

Explanation: The tree structure is:
    1
   / \
  2   3
     /
    4

The longest path from root to leaf is 1 → 3 → 4, which has 3 nodes.
```

### Example 2:

```
Input: root = []

Output: 0

Explanation: Empty tree has depth 0.
```

---

## Constraints

- `0 <= The number of nodes in the tree <= 100`
- `-100 <= Node.val <= 100`

---
