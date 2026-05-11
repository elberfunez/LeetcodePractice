# Group Anagrams

**Difficulty:** Medium  
**Topic:** Arrays & Hashing

---

## Description

Given an array of strings `strs`, group all anagrams together into sublists. You may return the output in any order.

An **anagram** is a string that contains the exact same characters as another string, but the order of the characters can be different.

---

## Examples

### Example 1:

```
Input: strs = ["act","pots","tops","cat","stop","hat"]

Output: [["hat"],["act", "cat"],["stop", "pots", "tops"]]

Explanation: 
- "act" and "cat" are anagrams
- "pots", "tops", "stop" are anagrams
- "hat" has no anagrams
```

### Example 2:

```
Input: strs = ["x"]

Output: [["x"]]

Explanation: Single string in its own group
```

### Example 3:

```
Input: strs = [""]

Output: [[""]]

Explanation: Empty string in its own group
```

---

## Constraints

- `1 <= strs.length <= 1000`
- `0 <= strs[i].length <= 100`
- `strs[i]` is made up of lowercase English letters
