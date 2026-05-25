# Binary Tree Postorder Traversal

**Difficulty:** Easy  
**Topic:** Trees

---

## Description

You are given the root of a binary tree, return the postorder traversal of its nodes' values.

Postorder traversal visits nodes in this order: **Left subtree → Right subtree → Current node**

This is also known as Left-Right-Root (LRR) traversal.

---

## Examples

### Example 1:

```
Input: root = [1,2,3,4,5,6,7]

Output: [4,5,2,6,7,3,1]

Explanation: Tree structure:
       1
      / \
     2   3
    / \ / \
   4  5 6  7

Postorder: Left(4,5,2) → Right(6,7,3) → Root(1)
           [4,5,2,6,7,3,1]
```

### Example 2:

```
Input: root = [1,2,3,null,4,5,null]

Output: [4,2,5,3,1]

Explanation: Tree structure:
      1
     / \
    2   3
     \  /
      4 5

Postorder: 4, 2, 5, 3, 1
```

### Example 3:

```
Input: root = []

Output: []

Explanation: Empty tree.
```

---

## Constraints

- `0 <= number of nodes in the tree <= 100`
- `-100 <= Node.val <= 175`

---

## Follow Up

Recursive solution is trivial, could you do it iteratively?

---
