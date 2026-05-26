# Product of Array Except Self

**LeetCode Problem #238**  
**Difficulty:** Medium  
**Topics:** Array, Prefix Sum

## Description

Given an integer array `nums`, return an array `output` where `output[i]` is the product of all the elements of `nums` except `nums[i]`.

Each product is guaranteed to fit in a 32-bit integer.

## Examples

**Example 1:**
```
Input: nums = [1,2,4,6]
Output: [48,24,12,8]

Explanation:
output[0] = 2 * 4 * 6 = 48
output[1] = 1 * 4 * 6 = 24
output[2] = 1 * 2 * 6 = 12
output[3] = 1 * 2 * 4 = 8
```

**Example 2:**
```
Input: nums = [-1,0,1,2,3]
Output: [0,-6,0,0,0]

Explanation:
output[0] = 0 * 1 * 2 * 3 = 0
output[1] = -1 * 1 * 2 * 3 = -6
output[2] = -1 * 0 * 2 * 3 = 0
output[3] = -1 * 0 * 1 * 3 = 0
output[4] = -1 * 0 * 1 * 2 = 0
```

## Constraints

- `2 <= nums.length <= 1000`
- `-20 <= nums[i] <= 20`

## Follow-up

Could you solve it in O(n) time without using the division operation?

**Note:** The division operation approach (total product divided by each element) doesn't work when there are zeros in the array.

## Key Observations

1. For each position i, we need the product of all elements except nums[i]
2. This can be computed as: product of all elements left of i × product of all elements right of i
3. We need to handle the zero case carefully
4. Division operation won't work due to potential zeros in the array
