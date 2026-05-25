# Insert into a Binary Search Tree

**Difficulty:** Medium  
**Topic:** Trees

---

## Description

You are given the root node of a binary search tree (BST) and a value `val` to insert into the tree. Return the root node of the BST after the insertion. It is guaranteed that the new value does not exist in the original BST.

Note: There may exist multiple valid ways for the insertion, as long as the tree remains a BST after insertion. You can return any of them.

---

## Examples

### Example 1:

```
Input: root = [5,3,9,1,4], val = 6

Output: [5,3,9,1,4,6]

Explanation: Tree structure:
    5
   / \
  3   9
 / \
1   4

After inserting 6:
    5
   / \
  3   9
 / \ /
1   4 6

6 is less than 9, so it goes in the left subtree of 9.
```

### Example 2:

```
Input: root = [5,3,6,null,4,null,10,null,null,7], val = 9

Output: [5,3,6,null,4,null,10,null,null,7,null,null,9]

Explanation: 9 gets inserted as the left child of 10.
```

---

## Constraints

- `0 <= The number of nodes in the tree <= 10,000`
- `-100,000,000 <= val, Node.val <= 100,000,000`
- All the values `Node.val` are unique
- It's guaranteed that `val` does not exist in the original BST

---
