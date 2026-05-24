# Binary Tree Inorder Traversal

**Difficulty:** Easy  
**Topic:** Trees

---

## Description

You are given the root of a binary tree, return the inorder traversal of its nodes' values.

Inorder traversal visits nodes in this order: **Left subtree → Current node → Right subtree**

This is also known as Left-Root-Right (LRR) traversal.

---

## Examples

### Example 1:

```
Input: root = [1,2,3,4,5,6,7]

Output: [4,2,5,1,6,3,7]

Explanation: Tree structure:
       1
      / \
     2   3
    / \ / \
   4  5 6  7

Inorder: Left(4,2,5) → Root(1) → Right(6,3,7)
         [4,2,5,1,6,3,7]
```

### Example 2:

```
Input: root = [1,2,3,null,4,5,null]

Output: [2,4,1,5,3]

Explanation: Tree structure:
      1
     / \
    2   3
     \  /
      4 5

Inorder: 2, 4, 1, 5, 3
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
- `-100 <= Node.val <= 100`

---

## Follow Up

Recursive solution is trivial, could you do it iteratively?

---
