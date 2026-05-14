# Remove Nth Node From End of List

**Difficulty:** Medium  
**Topic:** Linked List

---

## Description

You are given the beginning of a linked list `head`, and an integer `n`.

Remove the nth node **from the end** of the list and return the beginning of the list.

---

## Examples

### Example 1:

```
Input: head = [1,2,3,4], n = 2

Output: [1,2,4]

Explanation: Remove the 2nd node from the end (node with value 3).
Original: 1 -> 2 -> 3 -> 4 -> null
Result:   1 -> 2 -> 4 -> null
```

### Example 2:

```
Input: head = [5], n = 1

Output: []

Explanation: Remove the only node (the 1st from end).
```

### Example 3:

```
Input: head = [1,2], n = 2

Output: [2]

Explanation: Remove the 2nd node from the end (which is the head).
```

---

## Constraints

- The number of nodes in the list is `sz`
- `1 <= sz <= 30`
- `0 <= Node.val <= 100`
- `1 <= n <= sz`
