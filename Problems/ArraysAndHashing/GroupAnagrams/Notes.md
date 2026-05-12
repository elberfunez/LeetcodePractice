# Notes - Group Anagrams

## Pattern / Approach

Use a **sorted-string key** approach: sort each string's characters to create a canonical form (key), then group all strings with the same key together using a **Dictionary**.

## Data Structure(s) Used

- **Dictionary<string, List<string>>** — Maps sorted string (key) to list of all anagrams with that sorted form
- **List<string>** — Stores strings grouped by their anagram

## Thought Process

**Key insight:** All anagrams have the same sorted form!

For example:
- "act" sorted → "act"
- "cat" sorted → "act"
- "tac" sorted → "act"

So we can group by sorted form:
1. For each string, sort its characters to get a canonical key
2. Use this key to group strings together
3. All strings that sort to the same key are anagrams

```
Example: ["act", "pots", "tops", "cat", "stop", "hat"]

String  → Sorted (key)
"act"   → "act"
"pots"  → "opst"
"tops"  → "opst"
"cat"   → "act"
"stop"  → "opst"
"hat"   → "aht"

Grouping by key:
"act"  → ["act", "cat"]
"opst" → ["pots", "tops", "stop"]
"aht"  → ["hat"]

Return: [["act", "cat"], ["pots", "tops", "stop"], ["hat"]]
```

## Complexity

**Time:** O(N × K log K) where N = number of strings, K = max length of a string
- For each string: sort its characters (K log K)
- Across all strings: N × K log K
- Dictionary operations (add/lookup): O(1) average

**Space:** O(N × K) — Dictionary stores all strings, keys are sorted strings of length K

## Edge Cases

- **Empty array** (not per constraints, but would return empty list)
- **Single string** (["x"] → [["x"]])
- **Empty string** ([""] → [[""]])
- **All anagrams** (["abc", "bca", "cab"] → [["abc", "bca", "cab"]])
- **No anagrams** (["a", "b", "c"] → [["a"], ["b"], ["c"]])
- **Duplicate strings** (["abc", "abc"] → [["abc", "abc"]])

## Key Insight

**Sorted string as key = elegant grouping!**

Why this works:
- **Canonical form** — Sorting creates a unique representation for all anagrams
- **Dictionary lookup** — O(1) average for checking if key exists
- **Automatic grouping** — All anagrams naturally collect under the same key
- **Simple implementation** — No need to count frequencies or compare manually

Alternative approaches not used here:
- **Frequency counting:** Count character frequencies, use as key (similar complexity)
- **Sorting at end:** Collect all, sort the groups (adds O(N log N) overhead)
- **Prime product:** Multiply primes for each char (interesting but more complex)

This sorted-key approach is the most straightforward and efficient.

## Pattern Recognition for Similar Problems

🚩 **Red flags for GROUPING problems:**
1. **"Group X by Y"** → Use DICTIONARY/HASHMAP with key = Y
2. **"Anagrams"** → SORTED STRING KEY ⭐⭐⭐
3. **"Categorize"** or **"partition"** → DICTIONARY grouping
4. **"Find all combinations of..."** → Often uses grouping
5. **"Similar strings"** → Use a key to group

**Similar Problems:** "Valid Anagram" (check pair), "Anagram Substrings" (find all), "Isomorphic Strings" (similar grouping concept)

---

## My Solution Approach

### **Step 1: Initialize Dictionary for Grouping**
```csharp
var groups = new Dictionary<string, List<string>>();
```

Maps sorted string (canonical form) → list of all original strings with that form.

### **Step 2: Iterate Through Each String**
```csharp
foreach (var s in strs) 
{
```

I process each string to find its group.

### **Step 3: Create Canonical Key by Sorting**
```csharp
char[] chars = s.ToCharArray();
Array.Sort(chars);
string key = new string(chars);
```

**The magic:** I sort the string's characters to get the canonical form.

Example:
- "cat" → ['c', 'a', 't'] → sort → ['a', 'c', 't'] → "act" (key)
- "tac" → ['t', 'a', 'c'] → sort → ['a', 'c', 't'] → "act" (same key!)

### **Step 4: Get or Create Group (TryGetValue)**
```csharp
if (!groups.TryGetValue(key, out var group)) 
{
    group = new List<string>();
    groups[key] = group;
}
```

Efficient check and creation:
- `TryGetValue` returns false if key doesn't exist
- If it doesn't exist, create a new list and add to dictionary
- Either way, I now have a `group` list to add to

### **Step 5: Add String to Group**
```csharp
group.Add(s);
```

I add the original string to its anagram group (identified by sorted key).

### **Step 6: Return All Groups**
```csharp
return new List<List<string>>(groups.Values);
```

Convert dictionary values (all the groups) to a list and return.

### **Why This Works**

1. **Sorted key** — All anagrams have identical sorted forms
2. **Dictionary grouping** — Automatically collects anagrams under same key
3. **Original strings** — I store original order, not sorted
4. **Efficient lookup** — TryGetValue in O(1) average
5. **Simple logic** — No manual frequency counting needed

### **Example Walkthrough**

```
Input: ["act", "pots", "tops", "cat", "stop", "hat"]

Initialize: groups = {}

Process "act":
  chars = ['a', 'c', 't']
  sorted = ['a', 'c', 't']
  key = "act"
  "act" not in groups → create new list
  groups["act"] = ["act"]

Process "pots":
  chars = ['p', 'o', 't', 's']
  sorted = ['o', 'p', 's', 't']
  key = "opst"
  "opst" not in groups → create new list
  groups["opst"] = ["pots"]

Process "tops":
  chars = ['t', 'o', 'p', 's']
  sorted = ['o', 'p', 's', 't']
  key = "opst"
  "opst" EXISTS in groups!
  groups["opst"].Add("tops")
  groups["opst"] = ["pots", "tops"]

Process "cat":
  chars = ['c', 'a', 't']
  sorted = ['a', 'c', 't']
  key = "act"
  "act" EXISTS in groups!
  groups["act"].Add("cat")
  groups["act"] = ["act", "cat"]

Process "stop":
  chars = ['s', 't', 'o', 'p']
  sorted = ['o', 'p', 's', 't']
  key = "opst"
  "opst" EXISTS in groups!
  groups["opst"].Add("stop")
  groups["opst"] = ["pots", "tops", "stop"]

Process "hat":
  chars = ['h', 'a', 't']
  sorted = ['a', 'h', 't']
  key = "aht"
  "aht" not in groups → create new list
  groups["aht"] = ["hat"]

Final groups dictionary:
  "act"  → ["act", "cat"]
  "opst" → ["pots", "tops", "stop"]
  "aht"  → ["hat"]

Return groups.Values as List<List<string>>:
  [["act", "cat"], ["pots", "tops", "stop"], ["hat"]]
```

### **Example Walkthrough - Edge Case**

```
Input: ["x"]

Initialize: groups = {}

Process "x":
  chars = ['x']
  sorted = ['x']
  key = "x"
  "x" not in groups → create new list
  groups["x"] = ["x"]

Return: [["x"]]
```

Another edge case:

```
Input: [""]

Initialize: groups = {}

Process "":
  chars = []
  sorted = []
  key = ""
  "" not in groups → create new list
  groups[""] = [""]

Return: [[""]]
```

### **Complexity Summary**
- **Time:** O(N × K log K)
  - N strings, each sorted in O(K log K) where K = max length
  - Dictionary operations O(1) average
- **Space:** O(N × K)
  - Dictionary stores all strings and keys
  - In worst case (no anagrams), stores N strings

### **Why Sorted Key Over Frequency Counting?**

Both work, but sorted key is cleaner:
- **Sorted key:** Direct, intuitive, one operation per character
- **Frequency counting:** More bookkeeping, need to format key carefully

```csharp
// Your approach (cleaner):
string key = new string(s.ToCharArray().OrderBy(c => c).ToArray());

// Frequency approach (more verbose):
var freq = new int[26];
foreach (char c in s) freq[c - 'a']++;
string key = string.Concat(freq.Select((count, i) => 
    count > 0 ? $"{(char)('a' + i)}{count}" : ""));
```

Your approach wins on clarity!
