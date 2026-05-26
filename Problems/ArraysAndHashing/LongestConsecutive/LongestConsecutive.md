# Longest Consecutive Sequence

**LeetCode Problem #128**  
**Difficulty:** Medium  
**Topics:** Array, Hash Set, Union Find

## Description

Given an array of integers `nums`, return the length of the longest consecutive sequence of elements that can be formed.

A consecutive sequence is a sequence of elements in which each element is exactly 1 greater than the previous element. The elements do not have to be consecutive in the original array.

**You must write an algorithm that runs in O(n) time.**

## Examples

**Example 1:**
```
Input: nums = [2,20,4,10,3,4,5]
Output: 4
Explanation: The longest consecutive sequence is [2, 3, 4, 5].
```

**Example 2:**
```
Input: nums = [0,3,2,5,4,6,1,1]
Output: 7
Explanation: The longest consecutive sequence is [0, 1, 2, 3, 4, 5, 6].
```

## Constraints

- `0 <= nums.length <= 1000`
- `-10^9 <= nums[i] <= 10^9`

## Key Constraint

The **O(n) time requirement** is crucial:
- ❌ Sorting would be O(n log n)
- ❌ Nested loops would be O(n²)
- ✓ Hash Set lookup is O(1) average case → O(n) total

## Observations

1. Elements can appear in any order in the array
2. Duplicates should be handled (count only once)
3. The sequence elements must be consecutive values, not consecutive positions
4. Need to find the longest such sequence
5. With O(n) constraint, must use a data structure with O(1) lookup

## Approach Hint

Think about using a Set to enable O(1) lookups. Then, for each number, check if it could be the START of a sequence (i.e., num-1 is not in the set). If it is a start, count how long the sequence extends.
