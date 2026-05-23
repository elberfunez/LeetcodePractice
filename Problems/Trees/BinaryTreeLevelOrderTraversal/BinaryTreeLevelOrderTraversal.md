# Binary Tree Level Order Traversal

**Difficulty:** Medium  
**Topic:** Trees

---

## Description

Given a binary tree `root`, return the level order traversal of it as a nested list, where each sublist contains the values of nodes at a particular level in the tree, from left to right.

Level order traversal visits nodes level by level, from top to bottom, left to right.

---

## Examples

### Example 1:

```
Input: root = [1,2,3,4,5,6,7]

Output: [[1],[2,3],[4,5,6,7]]

Explanation: Tree structure:
       1           ← Level 1: [1]
      / \
     2   3         ← Level 2: [2,3]
    / \ / \
   4  5 6  7       ← Level 3: [4,5,6,7]
```

### Example 2:

```
Input: root = [1]

Output: [[1]]

Explanation: Single node tree.
```

### Example 3:

```
Input: root = []

Output: []

Explanation: Empty tree.
```

---

## Constraints

- `0 <= The number of nodes in the tree <= 1000`
- `-1000 <= Node.val <= 1000`

---
