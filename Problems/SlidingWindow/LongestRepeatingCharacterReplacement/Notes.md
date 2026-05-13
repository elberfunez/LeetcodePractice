# Notes - Longest Repeating Character Replacement

## Pattern / Approach

Sliding window with character frequency tracking: expand the window by moving the right pointer, track the frequency of each character, and shrink from the left when the number of replacements needed exceeds k. The key insight is that the replacements needed = window_length - max_frequency.

## Data Structure(s) Used

- **Dictionary<char, int>** — Maps each character to its frequency in the current window
- **Two pointers** (left, right) — Define the sliding window boundaries
- **Integer** (result) — Tracks the longest valid window found

## Thought Process

**Key insight:** In any window, the number of characters I need to replace to make them all the same is: window_length - max_frequency_in_window.

For example, in "AAAB":
- Window length = 4
- Max frequency = 3 (three 'A's)
- Replacements needed = 4 - 3 = 1 (replace the 'B' with 'A')

So my algorithm:
1. Track character frequencies in the current window using a Dictionary
2. For each position, calculate: replacements_needed = window_length - max_frequency
3. If replacements_needed > k, shrink the window from the left
4. Track the maximum valid window size

```
Example: s = "AAABABB", k = 1

Window [A]:           freq={A:1}, length=1, max=1, need=1-1=0 ≤ 1 ✓, result=1
Window [A,A]:         freq={A:2}, length=2, max=2, need=2-2=0 ≤ 1 ✓, result=2
Window [A,A,A]:       freq={A:3}, length=3, max=3, need=3-3=0 ≤ 1 ✓, result=3
Window [A,A,A,B]:     freq={A:3,B:1}, length=4, max=3, need=4-3=1 ≤ 1 ✓, result=4
Window [A,A,B,A]:     freq={A:3,B:1}, length=5, max=3, need=5-3=2 > 1 ✗
  Shrink: remove s[0]='A', freq={A:2,B:1}, length=4, max=2, need=4-2=2 > 1 ✗
  Shrink: remove s[1]='A', freq={A:1,B:1}, length=3, max=1, need=3-1=2 > 1 ✗
  Shrink: remove s[2]='A', freq={A:1,B:1}, length=2, max=1, need=2-1=1 ≤ 1 ✓, result=4
Window [A,B,B]:       freq={A:1,B:2}, length=3, max=2, need=3-2=1 ≤ 1 ✓, result=4
Window [B,B,A]:       freq={A:1,B:2}, length=3, max=2, need=3-2=1 ≤ 1 ✓, result=4
Window [B,A,B]:       freq={A:1,B:2}, length=3, max=2, need=3-2=1 ≤ 1 ✓, result=4

Return: 4? No wait, that should be 5... Let me recalculate...

Actually at Window [A,A,B,A,B]:
  freq={A:3,B:2}, length=5, max=3, need=5-3=2 > 1, so we need to shrink

But there's a valid window of length 5. Let me reconsider...

Actually "AAABABB" with k=1 should give 5. One interpretation: "AAAAB" where we replace the B with A, giving length 5.
Window positions 0-4: "AAABA", freq={A:4,B:1}, length=5, max=4, need=5-4=1 ≤ 1 ✓
So result=5 ✓
```

## Complexity

**Time:** O(N × 26) where N = length of string
- Outer loop: N iterations for right pointer
- Inner while loop: each character is removed at most once (amortized)
- d.Values.Max(): O(26) since at most 26 uppercase letters
- Total: O(N × 26) = O(N) practically

**Space:** O(26) = O(1)
- Dictionary stores at most 26 unique characters (uppercase English only)
- Constant space regardless of input size

## Edge Cases

- **Single character** (`"A"`, k=0): Window length 1, max=1, need=0, returns 1
- **All same character** (`"AAAA"`, k=0): Never need replacements, returns entire string length
- **k=0** (no replacements): Only valid if window is already one character
- **k equals string length**: Can replace everything, returns entire string length
- **All different characters** (`"ABCD"`, k=1): Can have window of 2 (replace 1 character to match the other)

## Key Insight

**The replacement formula is the game-changer!**

Instead of asking "can I make this window one character?", I ask "how many replacements would I need?"

Replacements needed = window_length - max_frequency

Why this works:
- If max frequency is 3, then 2 characters are "different"
- To make all characters the same, I replace those 2 different ones
- This is always the minimum number of replacements needed
- If this number ≤ k, the window is valid

This insight transforms the problem from checking each window validity individually to using a mathematical property that guides the window shrinking.

## Pattern Recognition for Similar Problems

🚩 **Red flags for SLIDING WINDOW + CONSTRAINT problems:**
1. **"Longest substring with at most K of something"** → SLIDING WINDOW ⭐⭐⭐
2. **"At most K changes/replacements"** → SLIDING WINDOW with frequency tracking ⭐⭐
3. **"Make all characters same with K operations"** → Often SLIDING WINDOW
4. **"Window validity depends on element counts"** → SLIDING WINDOW + Dictionary
5. **"Maximize length with K modifications"** → SLIDING WINDOW

**Similar Problems:** "Longest Substring with At Most K Distinct Characters", "Max Consecutive Ones III", "Fruit Into Baskets"

---

## My Solution Approach

### **Step 1: Initialize Tracking**
```csharp
Dictionary<char, int> d = new();
int l = 0;
int result = 0;
```

I create a Dictionary to track character frequencies in the current window, initialize the left pointer, and result to track the longest valid window.

### **Step 2: Expand Window and Track Frequencies**
```csharp
for (int r = 0; r < s.Length; r++)
{
    if (!d.ContainsKey(s[r]))
    {
        d[s[r]] = 1;
    }
    else
    {
        d[s[r]]++;
    }
```

For each right pointer position, I add the character to the frequency dictionary. This expands the window to include the new character.

### **Step 3: Calculate Replacements Needed and Shrink if Invalid**
```csharp
while ((r - l + 1) - d.Values.Max() > k)
{
    d[s[l]]--;
    l++;
}
```

I calculate: replacements_needed = window_length - max_frequency.

If this exceeds k, the window is invalid. I shrink from the left by:
- Decrementing the frequency of the left character
- Moving the left pointer right

This continues until the window is valid.

**Why this works:** The max frequency tells me how many characters are already the "target" character. The rest need to be replaced. If there are too many "rest" characters, I shrink.

### **Step 4: Update Maximum Window**
```csharp
result = Math.Max(result, (r - l + 1));
```

After each expansion (and potential shrinking), I update the result with the current window size if it's the best so far.

### **Why This Works**

1. **Frequency tracking** — Knows the most common character in the window
2. **Replacement formula** — Correctly calculates minimum replacements needed
3. **Greedy expansion** — Always try to expand if possible
4. **Shrink when invalid** — Only shrink when replacements exceed k
5. **Optimal window** — Maximum valid window size is the answer

### **Example Walkthrough - Example 1**

```
Input: s = "XYYX", k = 2

Initialize: d = {}, l = 0, result = 0

r=0 (s[0]='X'):
  d['X'] = 1 → d = {X:1}
  window_len = 1, max_freq = 1, need = 1-1 = 0 ≤ 2 ✓
  result = max(0, 1) = 1

r=1 (s[1]='Y'):
  d['Y'] = 1 → d = {X:1, Y:1}
  window_len = 2, max_freq = 1, need = 2-1 = 1 ≤ 2 ✓
  result = max(1, 2) = 2

r=2 (s[2]='Y'):
  d['Y'] = 2 → d = {X:1, Y:2}
  window_len = 3, max_freq = 2, need = 3-2 = 1 ≤ 2 ✓
  result = max(2, 3) = 3

r=3 (s[3]='X'):
  d['X'] = 2 → d = {X:2, Y:2}
  window_len = 4, max_freq = 2, need = 4-2 = 2 ≤ 2 ✓
  result = max(3, 4) = 4

Return: 4 ✓
(Replace both 'X's with 'Y' to get "YYYY", or both 'Y's with 'X' to get "XXXX")
```

### **Example Walkthrough - Example 2**

```
Input: s = "AAABABB", k = 1

Initialize: d = {}, l = 0, result = 0

r=0 (s[0]='A'):
  d['A'] = 1 → d = {A:1}
  window_len = 1, max_freq = 1, need = 0 ≤ 1 ✓
  result = 1

r=1 (s[1]='A'):
  d['A'] = 2 → d = {A:2}
  window_len = 2, max_freq = 2, need = 0 ≤ 1 ✓
  result = 2

r=2 (s[2]='A'):
  d['A'] = 3 → d = {A:3}
  window_len = 3, max_freq = 3, need = 0 ≤ 1 ✓
  result = 3

r=3 (s[3]='B'):
  d['B'] = 1 → d = {A:3, B:1}
  window_len = 4, max_freq = 3, need = 1 ≤ 1 ✓
  result = 4

r=4 (s[4]='A'):
  d['A'] = 4 → d = {A:4, B:1}
  window_len = 5, max_freq = 4, need = 1 ≤ 1 ✓
  result = 5

r=5 (s[5]='B'):
  d['B'] = 2 → d = {A:4, B:2}
  window_len = 6, max_freq = 4, need = 2 > 1 ✗
  While loop:
    d['A']-- → d = {A:3, B:2}, l = 1
    window_len = 5, max_freq = 3, need = 2 > 1 ✗
    d['A']-- → d = {A:2, B:2}, l = 2
    window_len = 4, max_freq = 2, need = 2 > 1 ✗
    d['A']-- → d = {A:1, B:2}, l = 3
    window_len = 3, max_freq = 2, need = 1 ≤ 1 ✓ (exit while)
  result = max(5, 3) = 5

r=6 (s[6]='B'):
  d['B'] = 3 → d = {A:1, B:3}
  window_len = 4, max_freq = 3, need = 1 ≤ 1 ✓
  result = max(5, 4) = 5

Return: 5 ✓
(Window "AABAB" from index 2-6 has length 5, with 1 replacement of 'A' to 'B' makes "BBBBB")
```

### **Complexity Summary**

- **Time:** O(N × 26)
  - Outer loop: N iterations
  - Inner while loop: total of N removals across all iterations (amortized)
  - d.Values.Max(): O(26) per call since only 26 uppercase letters
  - Total: O(N × 26) = O(N) for practical purposes

- **Space:** O(26) = O(1)
  - Dictionary stores at most 26 characters
  - Constant space regardless of string size

### **Why This Approach Works**

The core idea: I don't need to check if the window "looks right" — I use a mathematical property (replacement formula) to determine validity.

Instead of: "Can I make this window all one character?"
I ask: "How many characters would I need to replace?"

This transforms the problem into a simple numerical comparison: if replacements_needed ≤ k, the window is valid.
