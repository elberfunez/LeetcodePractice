# Valid Binary Search Tree

**Difficulty:** Medium  
**Topic:** Trees

---

## Description

Given the root of a binary tree, return `true` if it is a valid binary search tree, otherwise return `false`.

A valid binary search tree satisfies the following constraints:

- The left subtree of every node contains only nodes with keys less than the node's key.
- The right subtree of every node contains only nodes with keys greater than the node's key.
- Both the left and right subtrees are also binary search trees.

---

## Examples

### Example 1:

```
Input: root = [2,1,3]

Output: true

Explanation: Tree structure:
    2
   / \
  1   3

Node 2: left (1) < 2 < right (3) ✓
Valid BST.
```

### Example 2:

```
Input: root = [1,2,3]

Output: false

Explanation: Tree structure:
    1
   / \
  2   3

Node 1: left (2) > 1, violates BST property ✗
Not a valid BST.
```

---

## Constraints

- `1 <= The number of nodes in the tree <= 1000`
- `-1000 <= Node.val <= 1000`

---
