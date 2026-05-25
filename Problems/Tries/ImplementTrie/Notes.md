# Implement Trie (Prefix Tree) - Notes

## Key Insights

- A Trie is fundamentally a tree where each node represents a character
- The root node doesn't represent any character, just serves as entry point
- Each node needs a way to track its children (dictionary/map) and mark end-of-word status
- The power of a Trie: prefix searches are O(m) where m is prefix length, not dependent on dictionary size

## Data Structure Design

### TrieNode Class
```
- Children: Dictionary<char, TrieNode>  (maps character to child node)
- IsEndOfWord: bool                     (marks valid word termination)
```

### Why this design?
- Dictionary allows O(1) lookup for child nodes
- IsEndOfWord flag distinguishes "do" (end of word) from "dog" (has children, no end marker)
- This handles the difference between Search (must reach end-of-word node) and StartsWith (just check path exists)

## Algorithm Walkthrough

### Insert("dog")
1. Start at root
2. 'd' not in root.Children → create new TrieNode, move to it
3. 'o' not in d.Children → create new TrieNode, move to it  
4. 'g' not in o.Children → create new TrieNode, move to it
5. Mark g.IsEndOfWord = true

### Search("dog")
1. Start at root
2. Navigate: 'd' → 'o' → 'g'
3. Check if final node (g) has IsEndOfWord = true
4. Return true if end marker found, false otherwise

### StartsWith("do")
1. Start at root
2. Navigate: 'd' → 'o'
3. Path exists, return true (no need to check IsEndOfWord)

## Complexity Analysis

### Time Complexity
- Insert(word): O(m) where m = word.length (traverse tree, create nodes as needed)
- Search(word): O(m) where m = word.length (traverse tree, check end marker)
- StartsWith(prefix): O(m) where m = prefix.length (traverse tree only)

### Space Complexity
- O(ALPHABET_SIZE × N × M) where:
  - ALPHABET_SIZE = 26 (lowercase English)
  - N = number of unique words inserted
  - M = average word length
- In worst case: each word path unique, space = number of characters across all words × 26 pointers per node

## Edge Cases to Consider

1. **Single character words**: "a" should be searchable after insert
2. **Prefix vs full word**: insert("car"), then search("ca") should be false
3. **Overlapping words**: insert("car"), insert("card") - both should search correctly
4. **Empty operations**: startsWith("") on empty tree
5. **Long words**: up to 1000 characters per constraint

## Optimization Notes

- Current implementation uses Dictionary for children - suitable for sparse English alphabet
- For very dense alphabets, array[26] would be faster but less flexible
- Could add word count/frequency tracking if needed for future features
- Could implement prefix() method returning all words with given prefix

## Testing Strategy

1. Basic insert/search/startsWith
2. Overlapping word prefixes
3. Non-existent words and prefixes
4. Single character operations
5. Longer word sequences
6. Large number of insertions
