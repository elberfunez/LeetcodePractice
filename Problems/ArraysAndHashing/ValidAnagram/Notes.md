# Notes - Valid Anagram

## Pattern / Approach

Use a **Dictionary to count character frequencies**. Build a frequency map from the first string, then validate that the second string has the exact same character counts.

## Data Structure(s) Used

- **Dictionary<char, int>** — Maps each character to its frequency (how many times it appears)

## Thought Process

**Key insight:** Two strings are anagrams if and only if they have the same character frequencies, regardless of order.

**Algorithm:**
1. Quick check: if lengths differ, they can't be anagrams
2. Count frequency of each character in string `s`
3. For each character in string `t`:
   - If it doesn't exist in the frequency map → not an anagram
   - Decrement the frequency count
   - If frequency goes negative → we have more of this character in `t` than in `s` → not an anagram
4. If we complete without issues → they're anagrams

```
Example: s = "racecar", t = "carrace"

Build frequency from s:
  'r' → 2
  'a' → 2
  'c' → 2
  'e' → 1

Process t character by character:
  'c' → exists, freq['c'] = 2-1 = 1 ✓
  'a' → exists, freq['a'] = 2-1 = 1 ✓
  'r' → exists, freq['r'] = 2-1 = 1 ✓
  'r' → exists, freq['r'] = 1-1 = 0 ✓
  'a' → exists, freq['a'] = 1-1 = 0 ✓
  'c' → exists, freq['c'] = 1-1 = 0 ✓
  'e' → exists, freq['e'] = 1-1 = 0 ✓

All frequencies reached 0, no character went negative → ANAGRAM ✓
```

## Complexity

**Time:** O(N) — Build frequency map O(N) + validate string O(N) = O(2N) = O(N)  
**Space:** O(1) — Dictionary stores at most 26 lowercase letters (constant space for this problem)

## Edge Cases

- **Different lengths** (`"a"`, `"ab"`): Return false immediately
- **Single character** (`"a"`, `"a"`): Same length 1, process normally
- **All same character** (`"aaa"`, `"aaa"`): All chars match, all frequencies reach 0
- **Empty vs non-empty**: Constraint says 1 <= length, but would fail length check
- **One anagram pair, extra char** (`"ab"`, `"abc"`): Different lengths, return false at start
- **Partially valid** (`"abc"`, `"def"`): First char 'a' not in freq → return false early

## Key Insight

**Frequency counting is the fundamental approach for anagrams!**

This solution demonstrates:
1. **Length check optimization** — Fast-path rejection for impossible cases
2. **Frequency map building** — Count what we have in the first string
3. **Validation by consumption** — Decrement as we see characters in the second string
4. **Early termination** — Return false immediately if:
   - A character doesn't exist in the frequency map (extra char in t)
   - A character's frequency goes negative (more instances in t than s)

The elegance is that by the end, if all counts reached 0, it's guaranteed to be an anagram.

## Pattern Recognition for Similar Problems

🚩 **Red flags for CHARACTER FREQUENCY problems:**
1. **"Anagram"** → CHARACTER FREQUENCY ⭐⭐⭐
2. **"Same characters"** or **"valid rearrangement"** → CHARACTER FREQUENCY
3. **"Count characters"** or **"character frequency"** → DICTIONARY
4. **"Permutation"** (of a string) → CHARACTER FREQUENCY
5. **"Group anagrams"** → CHARACTER FREQUENCY + Grouping

**Similar Problems:** "Group Anagrams", "Ransom Note", "Valid Sudoku", "First Unique Character"

---

## My Solution Approach

### **Step 1: Length Check Optimization**
```csharp
if (s.Length != t.Length) return false;
```

Fast-path rejection: anagrams must have the same length. If lengths differ, it's impossible. Return immediately without processing.

### **Step 2: Initialize Frequency Dictionary**
```csharp
Dictionary<char, int> freq = new(); // <character, count>
```

Maps each character to how many times it appears in string `s`.

### **Step 3: Build Frequency Map from First String**
```csharp
foreach (char c in s)
{
    if (!freq.ContainsKey(c))
    {
        freq[c] = 1;
    }
    else
    {
        freq[c]++;
    } 
}
```

I count occurrences of each character in `s`. This establishes what I "expect" to see in `t`.

**Alternative (cleaner):** Could use `TryGetValue` or `GetValueOrDefault` like in other solutions, but this explicit approach is clear.

### **Step 4: Validate Second String**
```csharp
foreach (char c in t)
{
    if (!freq.ContainsKey(c)) return false; // no way it could be anagram
    else freq[c]--;

    if (freq[c] < 0) return false; // no way it could be anagram
}
```

For each character in `t`:
- **Check existence:** If the character doesn't exist in frequency map, it's in `t` but not in `s` → not an anagram
- **Decrement count:** I've "used" one instance of this character
- **Check overflow:** If count goes negative, I have more instances of this character in `t` than in `s` → not an anagram

### **Step 5: Success**
```csharp
return true;
```

If I complete the loop without returning false, all characters matched perfectly → they're anagrams!

### **Why This Works**

1. **Length requirement** — Anagrams must have the same length
2. **Frequency counting** — Captures the essence of "same characters"
3. **Two-pass approach** — First pass counts, second pass validates
4. **Early exits** — Return false immediately when validation fails
5. **Mathematical guarantee** — If all frequencies hit exactly 0, strings are anagrams

### **Example Walkthrough - Is Anagram**

```
s = "racecar", t = "carrace"

Length check: 7 == 7 ✓

Build frequency from s:
  c='r' → freq = {r:1}
  c='a' → freq = {r:1, a:1}
  c='c' → freq = {r:1, a:1, c:1}
  c='e' → freq = {r:1, a:1, c:1, e:1}
  c='c' → freq = {r:1, a:1, c:2, e:1}
  c='a' → freq = {r:1, a:2, c:2, e:1}
  c='r' → freq = {r:2, a:2, c:2, e:1}

Validate with t:
  c='c' → exists, freq[c]=2-1=1, not negative ✓
  c='a' → exists, freq[a]=2-1=1, not negative ✓
  c='r' → exists, freq[r]=2-1=1, not negative ✓
  c='r' → exists, freq[r]=1-1=0, not negative ✓
  c='a' → exists, freq[a]=1-1=0, not negative ✓
  c='c' → exists, freq[c]=1-1=0, not negative ✓
  c='e' → exists, freq[e]=1-1=0, not negative ✓

All characters processed, none went negative
return true ✓ ANAGRAM
```

### **Example Walkthrough - Not an Anagram**

```
s = "jar", t = "jam"

Length check: 3 == 3 ✓

Build frequency from s:
  c='j' → freq = {j:1}
  c='a' → freq = {j:1, a:1}
  c='r' → freq = {j:1, a:1, r:1}

Validate with t:
  c='j' → exists, freq[j]=1-1=0 ✓
  c='a' → exists, freq[a]=1-1=0 ✓
  c='m' → NOT in freq → return false ✓
          (m is in t but not in s, so not anagram)
```

### **Example Walkthrough - Length Mismatch**

```
s = "a", t = "ab"

Length check: 1 != 2 → return false immediately ✓
(No need to build frequency map or validate)
```

### **Complexity Summary**
- **Time:** O(N) where N = length of strings
  - Length check: O(1)
  - Build frequency: O(N)
  - Validate: O(N)
  - Total: O(N)
- **Space:** O(1) for this problem
  - Dictionary holds at most 26 lowercase letters (constant)
  - Would be O(k) in general where k = alphabet size

### **Why Dictionary Over Array?**

For this specific problem (lowercase English letters only):
- Could use `int[26]` array for O(1) space guaranteed
- Dictionary is more general (works with any characters)
- Dictionary is clearer (character → count is explicit)

For production code, array might be slightly faster, but dictionary is more readable and flexible.
