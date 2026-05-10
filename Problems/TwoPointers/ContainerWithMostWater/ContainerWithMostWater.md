# Container With Most Water

**Difficulty:** Medium  
**Topic:** Two Pointers

---

## Description

You are given an integer array `heights` where `heights[i]` represents the height of the i-th bar.

You may choose any two bars to form a container. Return the maximum amount of water a container can store.

**Water capacity formula:** `min(height[i], height[j]) * (j - i)`
- Height is limited by the shorter bar
- Width is the distance between the two bars

---

## Examples

### Example 1:

```
Input: height = [1,7,2,5,4,7,3,6]

Output: 36

Explanation: The vertical lines are at positions [1,7] with area = min(7,6) * (7-1) = 36
```

### Example 2:

```
Input: height = [2,2,2]

Output: 4

Explanation: Any two bars have area = min(2,2) * (distance) = 4
```

### Example 3:

```
Input: height = [1,2]

Output: 1

Explanation: The only option is area = min(1,2) * 1 = 1
```

---

## Constraints

- `2 <= height.length <= 1000`
- `0 <= height[i] <= 1000`
