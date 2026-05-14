# Merge Two Sorted Linked Lists

**Difficulty:** Easy  
**Topic:** Linked List

---

## Description

You are given the heads of two sorted linked lists `list1` and `list2`.

Merge the two lists into one sorted linked list and return the head of the new sorted linked list.

The new list should be made up of nodes from `list1` and `list2`.

---

## Examples

### Example 1:

```
Input: list1 = [1,2,4], list2 = [1,3,5]

Output: [1,1,2,3,4,5]

Explanation: The two lists are merged into one sorted list.
```

### Example 2:

```
Input: list1 = [], list2 = [1,2]

Output: [1,2]

Explanation: One list is empty, return the other list.
```

### Example 3:

```
Input: list1 = [], list2 = []

Output: []

Explanation: Both lists are empty, return an empty list.
```

---

## Constraints

- `0 <= The length of each list <= 100`
- `-100 <= Node.val <= 100`
- Both `list1` and `list2` are sorted in non-decreasing order
