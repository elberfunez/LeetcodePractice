# Notes - Longest Consecutive Sequence

## Pattern / Approach

Use a **HashSet for O(1) lookups** combined with **smart sequence detection**. The key insight: only start counting from the beginning of a sequence (where num-1 doesn't exist in the set). This eliminates redundant work and achieves O(n) time instead of O(n²).

## Data Structure(s) Used

- **HashSet<int>** — Stores all unique numbers for O(1) lookup. Automatically deduplicates!
- **int counter** — Tracks the maximum sequence length found
- **int seqCounter** — Counts the current sequence length

## Thought Process

**Key insight:** The O(n) constraint rules out nested loops. Using a HashSet gives us O(1) lookups, but we need to avoid checking every number as a potential sequence starter—that would still be O(n²).

**The optimization:** Only start counting from sequence beginnings (numbers where num-1 doesn't exist). This way:
- We skip numbers that are in the middle of sequences
- Each number is processed at most once
- Total work is O(n)

**Algorithm:**
1. Add all numbers to a HashSet (deduplicates, enables O(1) lookup)
2. For each unique number:
   - Check if it's the START of a sequence (num-1 not in set)
   - If yes, count how far it extends (num, num+1, num+2, ...)
   - Track the maximum length found
3. Return the longest sequence

## Complexity

**Time:** O(n)
- HashSet creation: O(n)
- Main loop: O(n) iterations, but each number is visited at most O(1) times in the while loop
- Total: O(n)

**Space:** O(n) for the HashSet

## Edge Cases

- **Empty array:** Returns 0 (loop never runs, counter stays 0)
- **Single element:** Returns 1 (is its own sequence start)
- **All duplicates:** Returns 1 (HashSet deduplicates, single number is both start and end)
- **No consecutive elements:** Returns 1 (each number is its own sequence)
- **Negative numbers:** Handled naturally (e.g., -3, -2, -1 form a sequence)

## Key Insight

**"Only process candidates that meet a condition"** is a powerful optimization pattern.

By checking `!hs.Contains(num - 1)` first, we skip redundant work. Without this check:
- For [2, 3, 4, 5], we'd count from 2 (length 4), then from 3 (length 3), then from 4 (length 2), then from 5 (length 1) = O(n²)
- With the check, we only count from 2 = O(n)

---

## My Solution Approach

### **Step 1: Create HashSet from Array**

```csharp
HashSet<int> hs = new(nums);
```

This constructor shorthand is elegant! It's equivalent to:
```csharp
HashSet<int> hs = new HashSet<int>();
foreach (int num in nums)
{
    hs.Add(num);
}
```

**Why this step:**
- O(1) lookup time instead of O(n) array search
- Automatically removes duplicates (e.g., [4, 4, 5] → {4, 5})
- Allows us to check "does this number exist?" in constant time

### **Step 2: Initialize Counter**

```csharp
int counter = 0;
```

Tracks the longest sequence found so far. Starts at 0 for edge cases like empty array.

### **Step 3: Iterate Through Unique Numbers**

```csharp
foreach (int num in hs)
{
    bool isBegOfSeq = !hs.Contains(num - 1);
    if (isBegOfSeq)
    {
        // Count the sequence
    }
}
```

**The optimization:** By checking `!hs.Contains(num - 1)`, we identify sequence starts.

For input [2, 20, 4, 10, 3, 4, 5]:
- num=2: Does 1 exist? No → START ✓
- num=3: Does 2 exist? Yes → SKIP (not a start)
- num=4: Does 3 exist? Yes → SKIP (not a start)
- num=5: Does 4 exist? Yes → SKIP (not a start)
- num=10: Does 9 exist? No → START ✓
- num=20: Does 19 exist? No → START ✓

Only 3 sequence counts instead of 6! This is where O(n) comes from.

### **Step 4: Count Sequence Length**

```csharp
int seqCounter = 1;
int curNum = num;
while (hs.Contains(curNum + 1))
{
    seqCounter++;
    curNum = curNum + 1;
}
```

Once we identify a sequence start:
- Initialize counter to 1 (the starting number itself)
- Use `curNum` as an iterator to move through the sequence
- Keep incrementing while the next number exists in the set

**Example:** Starting at 2 with set {2, 3, 4, 5, 10, 20}
```
seqCounter = 1, curNum = 2
  Does 3 exist? Yes → seqCounter = 2, curNum = 3
  Does 4 exist? Yes → seqCounter = 3, curNum = 4
  Does 5 exist? Yes → seqCounter = 4, curNum = 5
  Does 6 exist? No → EXIT LOOP
  
seqCounter = 4 ✓
```

### **Step 5: Track Maximum**

```csharp
counter = Math.Max(counter, seqCounter);
```

After counting each sequence, update the global maximum. This ensures we return the longest one found.

### **Step 6: Return Result**

```csharp
return counter;
```

Counter holds the length of the longest consecutive sequence.

---

## Example Walkthrough - Full

```
Input: nums = [2, 20, 4, 10, 3, 4, 5]

Step 1: Create HashSet
hs = {2, 3, 4, 5, 10, 20}  (duplicates removed, order doesn't matter)

Step 2: Initialize
counter = 0

Step 3-4: Find sequences
Iteration 1 - num = 2:
  isBegOfSeq = !hs.Contains(1) = true ✓
  seqCounter = 1, curNum = 2
  Does 3 exist? Yes → seqCounter=2, curNum=3
  Does 4 exist? Yes → seqCounter=3, curNum=4
  Does 5 exist? Yes → seqCounter=4, curNum=5
  Does 6 exist? No → Exit loop
  counter = Math.Max(0, 4) = 4

Iteration 2 - num = 3:
  isBegOfSeq = !hs.Contains(2) = false → SKIP

Iteration 3 - num = 4:
  isBegOfSeq = !hs.Contains(3) = false → SKIP

Iteration 4 - num = 5:
  isBegOfSeq = !hs.Contains(4) = false → SKIP

Iteration 5 - num = 10:
  isBegOfSeq = !hs.Contains(9) = true ✓
  seqCounter = 1, curNum = 10
  Does 11 exist? No → Exit loop
  counter = Math.Max(4, 1) = 4

Iteration 6 - num = 20:
  isBegOfSeq = !hs.Contains(19) = true ✓
  seqCounter = 1, curNum = 20
  Does 21 exist? No → Exit loop
  counter = Math.Max(4, 1) = 4

Return counter = 4 ✓
```

## Example Walkthrough - With Negatives

```
Input: nums = [-3, -1, -2, 0, 1]

Step 1: Create HashSet
hs = {-3, -2, -1, 0, 1}

Step 3-4: Find sequences
num = -3:
  isBegOfSeq = !hs.Contains(-4) = true ✓
  Count: -3, -2, -1, 0, 1
  seqCounter = 5
  counter = 5

num = -2:
  isBegOfSeq = !hs.Contains(-3) = false → SKIP

num = -1:
  isBegOfSeq = !hs.Contains(-2) = false → SKIP

num = 0:
  isBegOfSeq = !hs.Contains(-1) = false → SKIP

num = 1:
  isBegOfSeq = !hs.Contains(0) = false → SKIP

Return counter = 5 ✓
```

## Example Walkthrough - Edge Cases

**Empty array:**
```
Input: []
hs = {}
counter = 0
Loop never executes
Return 0 ✓
```

**All duplicates:**
```
Input: [5, 5, 5, 5]
hs = {5}
num = 5:
  isBegOfSeq = !hs.Contains(4) = true ✓
  seqCounter = 1 (only 5 exists, 6 doesn't)
  counter = 1
Return 1 ✓
```

---

## Why This Works

1. **HashSet deduplicates** — Handles [4, 4, 5] automatically
2. **O(1) lookups** — `hs.Contains()` is constant time
3. **Smart sequence detection** — Only start from sequence beginnings
4. **Single pass per sequence** — Each number processed at most once globally
5. **Negative numbers work** — No special handling needed, just basic comparisons

## Complexity Breakdown

Let's prove it's truly O(n):

```
HashSet creation:     O(n)
For loop:             O(n) iterations

Inside the loop:
  hs.Contains check:  O(1) per iteration
  While loop:         Each number enters at most once total
  
Total inner work:     O(n)

Total:                O(n) ✓
```

The while loop is key: `curNum` iterates through the sequence, but each unique number is visited at most once across ALL iterations. This is O(n) total, not O(n) per starting number.

## Key Takeaway

This problem beautifully demonstrates **algorithmic optimization through selective processing**:

Without the `!hs.Contains(num - 1)` check → O(n²)  
With the check → O(n)

The same technique applies to:
- **Merge intervals** — Only process interval starts
- **Activity selection** — Only process valid candidates
- **Graph problems** — Only process unvisited nodes

Always ask: "Can I skip redundant candidates by checking a condition first?"
