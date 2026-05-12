# Binary Search

**Difficulty:** Easy  
**Topic:** Binary Search

---

## Description

You are given an array of distinct integers `nums`, sorted in ascending order, and an integer `target`.

Implement a function to search for `target` within `nums`. If it exists, then return its index, otherwise, return `-1`.

Your solution must run in **O(log n)** time.

---

## Examples

### Example 1:

```
Input: nums = [-1,0,2,4,6,8], target = 4

Output: 3

Explanation: 4 is at index 3 in the array
```

### Example 2:

```
Input: nums = [-1,0,2,4,6,8], target = 3

Output: -1

Explanation: 3 is not in the array, return -1
```

---

## Constraints

- `1 <= nums.length <= 10000`
- `-10000 < nums[i], target < 10000`
- All integers in `nums` are unique
- `nums` is sorted in ascending order
