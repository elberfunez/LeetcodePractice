# Notes - Two Sum

## Pattern / Approach

Use a **Hash Map (Dictionary)** to store values we've already seen and their indices. For each number, check if its complement (`target - num`) already exists in the hash map.

## Data Structure(s) Used

- **Dictionary<int, int>** — maps each number to its index. This allows O(1) lookup time to find if the complement exists.

## Thought Process

**Key insight:** If we need `a + b == target`, then `b == target - a`. So for each number, we're asking: "Have I already seen the number I need?"

Instead of checking all pairs (O(N²)), use a hash map to track numbers we've seen:
1. As we iterate through the array
2. Calculate what we're looking for: `target - nums[i]`
3. Check if it's in our map (O(1) lookup)
4. If found → return the pair
5. If not found → add current number to map for future lookups

```
Example: nums = [3, 4, 5, 6], target = 7

i=0: nums[0]=3
     rem = 7 - 3 = 4
     4 in map? No
     Add 3 → map = {3:0}

i=1: nums[1]=4
     rem = 7 - 4 = 3
     3 in map? Yes! (at index 0)
     Found pair: (3, 4) → return [0, 1] ✓
```

## Complexity

**Time:** O(N) — Single pass through array, each dictionary operation is O(1)  
**Space:** O(N) — Dictionary stores up to N numbers in worst case

## Edge Cases

- **Two same numbers** (`[5, 5], target=10`): Dictionary already has index of first 5, finds it for second 5
- **Duplicate numbers** (`[2, 2, 3], target=5`): Works fine, dictionary updates with latest index
- **Multiple valid pairs** (`[1, 2, 3, 4], target=5`): Returns the first valid pair found (1+4)
- **Negative numbers** (`[-3, 4, 3], target=1`): Works fine, hash map doesn't care about sign
- **Zero** (`[0, 5], target=5`): Works fine, 0 is a valid value in the map

## Key Insight

**Hash map trades space for speed!** This is the classic space-time tradeoff:

- **Brute force:** Check all pairs O(N²) time, O(1) space
- **Hash map:** Check each number once O(N) time, O(N) space ← **THIS APPROACH**
- **Sorted two pointers:** Works only if array is sorted, O(N log N) time (with sort), O(1) space

The hash map approach is **best when:**
- Array is unsorted
- You need O(N) time
- Space is not a constraint
- You want simplicity and clarity

## Pattern Recognition for Similar Problems

🚩 **Red flags for HASH MAP approach:**
1. **"Find two elements that sum to X"** → HASH MAP ⭐⭐⭐
2. **"Find if exists"** → HASH MAP (fast lookup)
3. **"Array is unsorted"** + **"find a pair"** → HASH MAP (can't use two pointers without sorting)
4. **"O(N) time required"** → HASH MAP (can achieve this)
5. **"Target sum"** → HASH MAP (complement approach)

**When to use what for "Two Sum":**
- **Unsorted array** → Hash map
- **Sorted array** → Two pointers (saves space)
- **Very large array, limited memory** → Two pointers (requires sorting)
- **Need to find ALL pairs** → Hash map is cleaner

**Similar Problems:** "Two Sum II" (sorted, use two pointers), "3Sum" (use sorting + two pointers), "Valid Sudoku" (hash map), "Contains Duplicate"

---

## Your Solution Approach

### **Step 1: Initialize Hash Map**
```csharp
Dictionary<int, int> d = new();
```

Will store `value → index` pairs. Use this to quickly check if a number has been seen.

### **Step 2: Iterate Through Array**
```csharp
for (int i = 0; i < nums.Length; i++)
{
```

Single pass through all numbers.

### **Step 3: Calculate the Complement**
```csharp
int rem = target - nums[i]; // this is what we'll check in the map
```

**The math:** If `a + b = target`, then `b = target - a`

So for current number `nums[i]`, the complement we're looking for is `rem`.

### **Step 4: Check if Complement Exists (TryGetValue)**
```csharp
if (d.TryGetValue(rem, out int j))
{
    return [j, i];
}
```

**TryGetValue is the key:**
- Returns `true` if `rem` exists in dictionary
- Outputs the index in the `out` parameter `j`
- **Cleaner than** checking `ContainsKey(rem)` then `d[rem]` (two lookups)

If found → we have our pair! Return indices immediately.

### **Step 5: Add Current Number to Map**
```csharp
else 
{
    d[nums[i]] = i;
}
```

If complement not found yet, add current number to map for future iterations to find.

### **Why This Works**

1. **Complement approach** — For each number, ask "have I seen what I need?"
2. **Hash map** — O(1) lookup makes checking instant
3. **Single pass** — Add to map as we go, no preprocessing needed
4. **Early return** — Stop immediately when pair is found
5. **Works on unsorted arrays** — No need to sort

### **Example Walkthrough**

```
Input: [3, 4, 5, 6], target = 7

map = {}

i=0: nums[0] = 3
     rem = 7 - 3 = 4
     d.TryGetValue(4) → false (4 not in map yet)
     d[3] = 0 → map = {3:0}

i=1: nums[1] = 4
     rem = 7 - 4 = 3
     d.TryGetValue(3) → true (3 is in map at index 0) ✓
     return [0, 1] ✓ FOUND!
```

Another example:

```
Input: [4, 5, 6], target = 10

map = {}

i=0: nums[0] = 4
     rem = 10 - 4 = 6
     d.TryGetValue(6) → false
     d[4] = 0 → map = {4:0}

i=1: nums[1] = 5
     rem = 10 - 5 = 5
     d.TryGetValue(5) → false (need two different indices)
     d[5] = 1 → map = {4:0, 5:1}

i=2: nums[2] = 6
     rem = 10 - 6 = 4
     d.TryGetValue(4) → true (4 is in map at index 0) ✓
     return [0, 2] ✓ FOUND!
```

Edge case with duplicate values:

```
Input: [5, 5], target = 10

map = {}

i=0: nums[0] = 5
     rem = 10 - 5 = 5
     d.TryGetValue(5) → false (5 not in map yet)
     d[5] = 0 → map = {5:0}

i=1: nums[1] = 5
     rem = 10 - 5 = 5
     d.TryGetValue(5) → true (5 is in map at index 0) ✓
     return [0, 1] ✓ FOUND! (using both 5's)
```

### **Complexity Summary**
- **Time:** O(N) — Single pass, each TryGetValue is O(1)
- **Space:** O(N) — Dictionary stores up to N-1 values in worst case

### **Comparison: Hash Map vs Two Pointers**

| Aspect | Hash Map (Your Solution) | Two Pointers (TwoIntegerSumII) |
|--------|------------------------|------------------------------|
| **Works on** | Unsorted arrays | Sorted arrays only |
| **Time** | O(N) | O(N) (but needs O(N log N) sort) |
| **Space** | O(N) | O(1) |
| **Best for** | Unsorted, when you can use space | Sorted, when space is limited |
| **Simplicity** | Very clean and intuitive | Requires understanding pointers |

Both are valid! Choose based on your constraints.
