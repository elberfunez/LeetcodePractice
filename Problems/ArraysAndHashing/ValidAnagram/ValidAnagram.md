# Valid Anagram

**Difficulty:** Easy  
**Topic:** Arrays & Hashing

---

## Description

Given two strings `s` and `t`, return `true` if the two strings are anagrams of each other, otherwise return `false`.

An **anagram** is a string that contains the exact same characters as another string, but the order of the characters can be different.

---

## Examples

### Example 1:

```
Input: s = "racecar", t = "carrace"

Output: true

Explanation: "racecar" and "carrace" contain the same characters
```

### Example 2:

```
Input: s = "jar", t = "jam"

Output: false

Explanation: "jar" and "jam" have different characters
```

### Example 3:

```
Input: s = "a", t = "a"

Output: true

Explanation: Single character matches exactly
```

---

## Constraints

- `1 <= s.length, t.length <= 5 * 10^4`
- `s` and `t` consist of lowercase English letters
