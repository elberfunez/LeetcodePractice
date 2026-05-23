# Same Tree

**Difficulty:** Easy  
**Topic:** Trees

---

## Description

Given the roots of two binary trees `p` and `q`, return `true` if the trees are equivalent, otherwise return `false`.

Two binary trees are considered equivalent if they share the exact same structure and the nodes have the same values.

---

## Examples

### Example 1:

```
Input: p = [1,2,3], q = [1,2,3]

Output: true

Explanation: Both trees have the same structure and values.
```

### Example 2:

```
Input: p = [4,7], q = [4,null,7]

Output: false

Explanation: Tree p has 7 on the left, tree q has 7 on the right.
Different structure.
```

### Example 3:

```
Input: p = [1,2,3], q = [1,3,2]

Output: false

Explanation: Same structure, but values at different positions.
```

---

## Constraints

- `0 <= The number of nodes in both trees <= 100`
- `-100 <= Node.val <= 100`

---
