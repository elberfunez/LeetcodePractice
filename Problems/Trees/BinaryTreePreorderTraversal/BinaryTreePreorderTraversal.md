# Binary Tree Preorder Traversal

**Difficulty:** Easy  
**Topic:** Trees

---

## Description

You are given the root of a binary tree, return the preorder traversal of its nodes' values.

Preorder traversal visits nodes in this order: **Current node → Left subtree → Right subtree**

This is also known as Root-Left-Right (RLR) traversal.

---

## Examples

### Example 1:

```
Input: root = [1,2,3,4,5,6,7]

Output: [1,2,4,5,3,6,7]

Explanation: Tree structure:
       1
      / \
     2   3
    / \ / \
   4  5 6  7

Preorder: Root(1) → Left(2,4,5) → Right(3,6,7)
          [1,2,4,5,3,6,7]
```

### Example 2:

```
Input: root = [1,2,3,null,4,5,null]

Output: [1,2,4,3,5]

Explanation: Tree structure:
      1
     / \
    2   3
     \  /
      4 5

Preorder: 1, 2, 4, 3, 5
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
