# Top K Frequent Elements

**Difficulty:** Medium  
**Topic:** Arrays & Hashing

---

## Description

Given an integer array `nums` and an integer `k`, return the `k` most frequent elements within the array.

The test cases are generated such that the answer is always unique.

You may return the output in any order.

---

## Examples

### Example 1:

```
Input: nums = [1,2,2,3,3,3], k = 2

Output: [2,3]

Explanation: 
- 1 appears 1 time
- 2 appears 2 times
- 3 appears 3 times
- The 2 most frequent elements are 2 and 3
```

### Example 2:

```
Input: nums = [7,7], k = 1

Output: [7]

Explanation: 7 appears 2 times (the only unique element), so return [7]
```

---

## Constraints

- `1 <= nums.length <= 10^4`
- `-1000 <= nums[i] <= 1000`
- `1 <= k <= number of distinct elements in nums`
