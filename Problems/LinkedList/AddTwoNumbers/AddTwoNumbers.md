# Add Two Numbers

**LeetCode Problem #2**  
**Difficulty:** Medium  
**Topics:** Linked List, Math

## Description

You are given two non-empty linked lists, `l1` and `l2`, where each represents a non-negative integer.

The digits are stored in **reverse order**, e.g. the number 321 is represented as `1 -> 2 -> 3` in the linked list.

Each of the nodes contains a single digit. You may assume the two numbers do not contain any leading zeros, except the number 0 itself.

**Return the sum of the two numbers as a linked list.**

## Examples

**Example 1:**
```
Input: l1 = [1,2,3], l2 = [4,5,6]
Output: [5,7,9]

Explanation: 
  321 + 654 = 975
  Represented as: 5 -> 7 -> 9
```

**Example 2:**
```
Input: l1 = [9], l2 = [9]
Output: [8,1]

Explanation:
  9 + 9 = 18
  Represented as: 8 -> 1
```

## Constraints

- `1 <= l1.length, l2.length <= 100`
- `0 <= Node.val <= 9`

## Key Observations

1. Lists are in **reverse order** (least significant digit first)
   - This makes addition natural—process from head to tail
   - Carries flow from left to right in the list
   
2. Lists can have **different lengths**
   - One list may be longer than the other
   - Continue processing the longer list with carry
   
3. **Carry handling** is important
   - When sum >= 10, create a node with digit % 10
   - Propagate carry to next iteration
   - Don't forget final carry if it exists after both lists are exhausted

## Approach Hint

Simulate addition like you would do by hand:
1. Start from the head of both lists (least significant digits)
2. Add corresponding digits plus any carry from previous iteration
3. Create new node with result digit (sum % 10)
4. Update carry for next iteration (sum / 10)
5. Move to next nodes in both lists
6. Continue until both lists are exhausted and no carry remains
