# Notes - Longest Substring Without Repeating Characters

## Pattern / Approach

Sliding window with HashSet: maintain a window of unique characters by expanding the right pointer, and when I encounter a duplicate, shrink the window from the left until the duplicate is removed. Track the maximum window size throughout.

## Data Structure(s) Used

- **HashSet<char>** — Tracks characters in the current window for O(1) lookups and O(1) removals
- **Two pointers** (left, right) — Define the sliding window boundaries
- **Integer** (maxLen) — Tracks the longest substring length found

## Thought Process

**Key insight:** Instead of checking all possible substrings (O(n²)), I can use a sliding window that guarantees no duplicates. When I encounter a duplicate, I shrink from the left instead of starting over.

**Algorithm:**
1. Use left and right pointers to define a window
2. Expand the window by moving right pointer through the string
3. For each character:
   - If it's not in the set, add it and update max length
   - If it's in the set (duplicate found), remove characters from the left until the duplicate is gone
4. Return the maximum length found

```
Example: s = "zxyzxyz"

Window [z]:           length = 1, maxLen = 1
Window [z,x]:         length = 2, maxLen = 2
Window [z,x,y]:       length = 3, maxLen = 3
Window [z,x,y,z]:     z duplicate! shrink → [x,y,z], length = 3
Window [x,y,z,x]:     x duplicate! shrink → [y,z,x], length = 3
Window [y,z,x,y]:     y duplicate! shrink → [z,x,y], length = 3
Window [z,x,y,z]:     z duplicate! shrink → [x,y,z], length = 3

Return: 3 ✓ (substrings "zxy", "xyz", "yzy", "zyx" all have length 3)
```

## Complexity

**Time:** O(N) where N = length of string
- Right pointer moves through string once: N iterations
- Left pointer also moves through string once (worst case): N iterations
- Each character is added and removed from HashSet at most once
- HashSet operations (Add, Remove, Contains): O(1)
- Total: O(N) + O(N) = O(N)

**Space:** O(min(N, M)) where N = string length, M = charset size
- HashSet stores at most all unique characters in the window
- For ASCII: at most 128 characters, so often O(1) practically
- For general case: limited by string length, so O(N) worst case

## Edge Cases

- **Empty string** (`""`): maxLen stays 0, returns 0
- **Single character** (`"a"`): One iteration, maxLen = 1, returns 1
- **All duplicates** (`"aaaa"`): Constant shrinking, maxLen = 1, returns 1
- **All unique** (`"abcdef"`): Window grows to full length, maxLen = 6, returns 6
- **Duplicate at end** (`"abcab"`): When 'a' appears at index 3, shrink until first 'a' is removed
- **Mixed repeats** (`"au"`): After "a", then "au", no duplicates, returns 2

## Key Insight

**Sliding window eliminates the need to check all substrings!**

Brute force would check every substring: for each start position, check each ending position, validate no duplicates. That's O(n³) or at least O(n²).

My approach:
- The window is always valid (no duplicates) by construction
- I only move right to expand and left to shrink
- Maximum window size is the answer
- This guarantees O(n) because each character is processed at most twice

Why it works:
- **HashSet ensures uniqueness** — I can instantly tell if a character is in the window
- **Two pointers move forward only** — Never backtrack, so linear time
- **Optimal subproblem** — The answer is the maximum window I encounter
- **Greedy expansion** — Expand whenever possible (no duplicates), shrink only when forced (duplicate found)

## Pattern Recognition for Similar Problems

🚩 **Red flags for SLIDING WINDOW problems:**
1. **"Longest/shortest substring/subarray without X"** → SLIDING WINDOW ⭐⭐⭐
2. **"Find all subarrays with property X"** → Often SLIDING WINDOW
3. **"No duplicates"** or **"contains unique"** → SLIDING WINDOW ⭐⭐
4. **"Maximum/minimum length with constraint"** → SLIDING WINDOW
5. **"Contiguous sequence with condition"** → SLIDING WINDOW

**Similar Problems:** "Longest Substring with At Most K Distinct Characters", "Max Consecutive Ones", "Contains Duplicate II", "Sliding Window Maximum"

---

## My Solution Approach

### **Step 1: Initialize Window Structure**
```csharp
var charSet = new HashSet<char>();
int l = 0;
int maxLen = 0;
```

I create a HashSet to track characters in the current window, a left pointer to mark the window start, and maxLen to track the best window I've found.

### **Step 2: Expand Window with Right Pointer**
```csharp
for (int r = 0; r < s.Length; r++)
{
```

I iterate through the string with the right pointer, expanding the window one character at a time.

### **Step 3: Handle Duplicates - Shrink from Left**
```csharp
while (charSet.Contains(s[r]))
{
    charSet.Remove(s[l]);
    l++;
}
```

When I encounter a character that's already in the set (duplicate):
- I remove the character at the left pointer from the set
- I move the left pointer right
- I repeat until the duplicate is removed

This maintains the invariant: "the window contains no duplicates."

**Why this works:** The duplicate character is somewhere in the window. By removing characters from the left until the duplicate is gone, I'm guaranteed to remove the old occurrence without losing any valid substring information (I already tracked the max length for the previous state).

### **Step 4: Add Current Character**
```csharp
charSet.Add(s[r]);
```

After handling any duplicates, I add the current character to the set. Now the window is valid again.

### **Step 5: Track Maximum Length**
```csharp
maxLen = Math.Max(maxLen, r - l + 1);
```

The window length is `r - l + 1` (from left pointer to right pointer, inclusive). I update maxLen if this is better.

### **Step 6: Return Result**
```csharp
return maxLen;
```

After processing all characters, I return the longest window length found.

### **Why This Works**

1. **HashSet for uniqueness** — O(1) to check if character exists in window
2. **Two pointers maintain invariant** — Window never has duplicates by construction
3. **Left shrinks only when necessary** — Avoids redundant work
4. **Right expands greedily** — Explore all possibilities
5. **Linear time guarantee** — Each character is added and removed once

### **Example Walkthrough - Detailed**

```
Input: s = "zxyzxyz"

Initialize: charSet = {}, l = 0, maxLen = 0

r=0 (s[0]='z'):
  charSet.Contains('z')? No
  charSet.Add('z') → charSet = {z}
  maxLen = max(0, 0-0+1) = 1

r=1 (s[1]='x'):
  charSet.Contains('x')? No
  charSet.Add('x') → charSet = {z,x}
  maxLen = max(1, 1-0+1) = 2

r=2 (s[2]='y'):
  charSet.Contains('y')? No
  charSet.Add('y') → charSet = {z,x,y}
  maxLen = max(2, 2-0+1) = 3

r=3 (s[3]='z'):
  charSet.Contains('z')? Yes (duplicate!)
  While loop:
    charSet.Remove(s[0]='z') → charSet = {x,y}, l=1
    charSet.Contains('z')? No, exit while
  charSet.Add('z') → charSet = {x,y,z}
  maxLen = max(3, 3-1+1) = 3

r=4 (s[4]='x'):
  charSet.Contains('x')? Yes (duplicate!)
  While loop:
    charSet.Remove(s[1]='x') → charSet = {y,z}, l=2
    charSet.Contains('x')? No, exit while
  charSet.Add('x') → charSet = {y,z,x}
  maxLen = max(3, 4-2+1) = 3

r=5 (s[5]='y'):
  charSet.Contains('y')? Yes (duplicate!)
  While loop:
    charSet.Remove(s[2]='y') → charSet = {z,x}, l=3
    charSet.Contains('y')? No, exit while
  charSet.Add('y') → charSet = {z,x,y}
  maxLen = max(3, 5-3+1) = 3

r=6 (s[6]='z'):
  charSet.Contains('z')? Yes (duplicate!)
  While loop:
    charSet.Remove(s[3]='z') → charSet = {x,y}, l=4
    charSet.Contains('z')? No, exit while
  charSet.Add('z') → charSet = {x,y,z}
  maxLen = max(3, 6-4+1) = 3

Return: 3 ✓
(Valid substrings of length 3: "zxy", "xyz", "yzy", "zyx")
```

### **Example Walkthrough - All Duplicates**

```
Input: s = "xxxx"

Initialize: charSet = {}, l = 0, maxLen = 0

r=0 (s[0]='x'):
  charSet.Add('x') → charSet = {x}
  maxLen = max(0, 0-0+1) = 1

r=1 (s[1]='x'):
  charSet.Contains('x')? Yes
  While loop:
    charSet.Remove(s[0]='x') → charSet = {}, l=1
    charSet.Contains('x')? No, exit while
  charSet.Add('x') → charSet = {x}
  maxLen = max(1, 1-1+1) = 1

r=2 (s[2]='x'):
  charSet.Contains('x')? Yes
  While loop:
    charSet.Remove(s[1]='x') → charSet = {}, l=2
    charSet.Contains('x')? No, exit while
  charSet.Add('x') → charSet = {x}
  maxLen = max(1, 2-2+1) = 1

r=3 (s[3]='x'):
  charSet.Contains('x')? Yes
  While loop:
    charSet.Remove(s[2]='x') → charSet = {}, l=3
    charSet.Contains('x')? No, exit while
  charSet.Add('x') → charSet = {x}
  maxLen = max(1, 3-3+1) = 1

Return: 1 ✓
(Each 'x' is a substring of length 1, no duplicates possible)
```

### **Complexity Summary**

- **Time:** O(N)
  - Right pointer goes through string once: N iterations
  - Left pointer also goes through string once total (amortized): N moves
  - HashSet operations (add, remove, contains): O(1) each
  - Total: O(N)

- **Space:** O(min(N, charset_size))
  - HashSet stores unique characters in the current window
  - Maximum size is the number of unique characters in the string
  - For ASCII: typically O(1) since max 128 characters
  - General case: O(N) if all characters are unique

### **Why Not Brute Force?**

Brute force would check every possible substring:
```csharp
// O(N²) or O(N³) - much slower
int maxLen = 0;
for (int i = 0; i < s.Length; i++) {
    for (int j = i; j < s.Length; j++) {
        // Check if substring [i..j] has duplicates - O(N)
        if (IsNoDuplicates(s, i, j)) {
            maxLen = Math.Max(maxLen, j - i + 1);
        }
    }
}
```

My sliding window approach is better because:
- **Faster:** O(N) vs O(N²) or O(N³)
- **Smarter:** Window is guaranteed valid by construction
- **Practical:** Handles all edge cases efficiently
