# Implement Trie (Prefix Tree)

**LeetCode Problem #208**  
**Difficulty:** Medium  
**Topics:** Trie, Hash Table, String, Design

## Description

A prefix tree (also known as a trie) is a tree data structure used to efficiently store and retrieve keys in a set of strings. Some applications of this data structure include auto-complete and spell checker systems.

Implement the `PrefixTree` class:

- `PrefixTree()` - Initializes the prefix tree object.
- `void Insert(string word)` - Inserts the string `word` into the prefix tree.
- `bool Search(string word)` - Returns `true` if the string `word` is in the prefix tree (i.e., was inserted before), and `false` otherwise.
- `bool StartsWith(string prefix)` - Returns `true` if there is a previously inserted string `word` that has the prefix `prefix`, and `false` otherwise.

## Example

```
Input:
["PrefixTree", "insert", "dog", "search", "dog", "search", "do", "startsWith", "do", "insert", "do", "search", "do"]

Output:
[null, null, true, false, true, null, true]

Explanation:
PrefixTree prefixTree = new PrefixTree();
prefixTree.Insert("dog");
prefixTree.Search("dog");    // return true
prefixTree.Search("do");     // return false
prefixTree.StartsWith("do"); // return true
prefixTree.Insert("do");
prefixTree.Search("do");     // return true
```

## Constraints

- `1 <= word.length, prefix.length <= 1000`
- `word` and `prefix` consist of lowercase English letters.
- At most `3 * 10^4` calls in total will be made to `Insert`, `Search`, and `StartsWith`.

## Approach

A Trie (prefix tree) is typically implemented using:
1. A tree structure where each node represents a character
2. Each node contains a dictionary/map of child nodes
3. A boolean flag to mark the end of a valid word

For each operation:
- **Insert**: Navigate through the tree, creating nodes as needed, and mark the final node as end-of-word
- **Search**: Navigate through the tree and return whether the full word was found and is marked as end-of-word
- **StartsWith**: Navigate through the tree and return whether the prefix path exists (no need to check end-of-word)

## Complexity Analysis

- **Time:**
  - Insert: O(m), where m is word length
  - Search: O(m), where m is word length
  - StartsWith: O(m), where m is prefix length
- **Space:** O(ALPHABET_SIZE * N * M), where N is number of words and M is average word length
