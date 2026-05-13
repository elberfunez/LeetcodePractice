# Longest Repeating Character Replacement

**Difficulty:** Medium  
**Topic:** Sliding Window

---

## Description

You are given a string `s` consisting of only uppercase English characters and an integer `k`. You can choose up to `k` characters of the string and replace them with any other uppercase English character.

After performing at most `k` replacements, return the length of the **longest substring** which contains only one distinct character.

---

## Examples

### Example 1:

```
Input: s = "XYYX", k = 2

Output: 4

Explanation: Either replace the 'X's with 'Y's to get "YYYY", or replace the 'Y's with 'X's to get "XXXX".
```

### Example 2:

```
Input: s = "AAABABB", k = 1

Output: 5

Explanation: Replace the 'B' at index 5 with 'A' to get "AAAAABB" then take "AAAAA".
Or replace 'A' at index 2 to get "AABABB" then take "AABAA" which has length 5.
```

---

## Constraints

- `1 <= s.length <= 1000`
- `0 <= k <= s.length`
- `s` consists of only uppercase English characters
