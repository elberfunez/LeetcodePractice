# Two Integer Sum II

**Difficulty:** Medium  
**Topic:** Two Pointers

---

## Description

Given an array of integers `numbers` that is sorted in non-decreasing order.

Return the indices (1-indexed) of two numbers, `[index1, index2]`, such that they add up to a given target number `target` and `index1 < index2`. Note that `index1` and `index2` cannot be equal, therefore you may not use the same element twice.

There will always be exactly one valid solution.

Your solution must use `O(1)` additional space.

---

## Examples

### Example 1:

```
Input: numbers = [1,2,3,4], target = 3

Output: [1,2]

Explanation:
The sum of 1 and 2 is 3. Since we are assuming a 1-indexed array, index1 = 1, index2 = 2. We return [1, 2].
```

### Example 2:

```
Input: numbers = [2,7,11,15], target = 9

Output: [1,2]
```

### Example 3:

```
Input: numbers = [-1,0], target = -1

Output: [1,2]
```

---

## Constraints

- `2 <= numbers.length <= 1000`
- `-1000 <= numbers[i] <= 1000`
- `-1000 <= target <= 1000`
- There will always be exactly one valid solution
