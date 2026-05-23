# Subtree of Another Tree

**Difficulty:** Easy  
**Topic:** Trees

---

## Description

Given the roots of two binary trees `root` and `subRoot`, return `true` if there is a subtree of `root` with the same structure and node values of `subRoot` and `false` otherwise.

A subtree of a binary tree `tree` is a tree that consists of a node in `tree` and all of this node's descendants. The tree `tree` could also be considered as a subtree of itself.

---

## Examples

### Example 1:

```
Input: root = [1,2,3,4,5], subRoot = [2,4,5]

Output: true

Explanation: The subtree rooted at node 2 matches subRoot exactly.
```

### Example 2:

```
Input: root = [1,2,3,4,5,null,null,6], subRoot = [2,4,5]

Output: false

Explanation: Node 4 has an extra child (6) not in subRoot.
```

---

## Constraints

- `1 <= The number of nodes in both trees <= 100`
- `-100 <= root.val, subRoot.val <= 100`

---
