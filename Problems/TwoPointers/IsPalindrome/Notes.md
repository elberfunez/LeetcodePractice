# Notes - Valid Palindrome

## Pattern / Approach

Two pointers moving inward from both ends. Since a palindrome reads the same forward and backward, characters at opposite ends must match.

## Data Structure(s) Used

None — only using two pointer indices

## Thought Process

A palindrome is defined as reading the same forwards and backwards. Start from both ends:
- `left` pointer at index 0
- `right` pointer at index `length - 1`

Move inward comparing characters. Key insight: **Skip non-alphanumeric characters** since the problem says to ignore them.

```
Example: "Was it a car or a cat I saw?"
         ↑                           ↑
       left                        right
       'W'                         '?'  (skip ?)
         ↑                       ↑
       'W'                       'w'  (case-insensitive match ✓)
```

**Algorithm:**
1. Skip non-alphanumeric on left: `while !IsLetterOrDigit(s[l]) l++`
2. Skip non-alphanumeric on right: `while !IsLetterOrDigit(s[r]) r--`
3. Compare characters (case-insensitive): `ToLower(s[l]) == ToLower(s[r])`
4. If mismatch → return false
5. If match → move both pointers inward: `l++, r--`
6. Continue until pointers meet

## Complexity

**Time:** O(N) — Single pass through string  
**Space:** O(1) — Only two pointers

## Edge Cases

- **Single character** (`"a"`): Valid palindrome by definition, left meets right immediately
- **Only non-alphanumeric** (`".,!"`): Skip all, pointers meet → valid palindrome
- **Empty/all spaces** (`"   "`): After skipping spaces, pointers meet → valid
- **Odd length** (`"abc"`): Pointers will cross, still works (`l < r` loop exits)
- **Case sensitivity** (`"Aa"`): Must use `ToLower()` for comparison

## Key Insight

**Non-alphanumeric characters are the trick!** The solution is simple: skip them entirely. When you encounter punctuation or spaces, just move that pointer without comparing. The `while` loops skip non-alphanumeric chars **before** each comparison.

This is efficient because:
- We process each character at most twice (once from left, once from right)
- We never store anything → O(1) space
- We stop as soon as we find a mismatch

## Pattern Recognition for Similar Problems

🚩 **Red flags for TWO POINTERS:**
1. **"Check if palindrome"** → TWO POINTERS ⭐⭐
2. **"Compare ends"** + **"work inward"** → TWO POINTERS
3. **"Valid Palindrome"**, **"Palindrome II"** → TWO POINTERS
4. When the problem asks you to ignore certain characters/elements → two pointers

**Similar Problems:** "Valid Palindrome II", "Palindrome Linked List", "Is Subsequence"
