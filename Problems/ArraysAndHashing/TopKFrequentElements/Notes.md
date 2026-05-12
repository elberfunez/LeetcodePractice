# Notes - Top K Frequent Elements

## Pattern / Approach

Use **frequency counting with sorting**: build a frequency map to count occurrences of each number, then sort entries by frequency in descending order and extract the top `k` keys.

## Data Structure(s) Used

- **Dictionary<int, int>** — Maps each number to its frequency (count of occurrences)
- **LINQ OrderByDescending** — Efficiently sorts dictionary entries by frequency
- **Array** — Return type containing the k most frequent elements

## Thought Process

**Key insight:** Find the top k elements by their frequency — count occurrences first, then rank by count.

**Algorithm:**
1. Count frequency of each number in the array using a dictionary
2. Sort all unique numbers by their frequency in descending order
3. Take the top k numbers from the sorted list
4. Return as an array

This is straightforward: more frequent numbers appear higher in the sorted order.

```
Example: nums = [1,2,2,3,3,3], k = 2

Step 1 - Build frequency map:
  1 → appears 1 time
  2 → appears 2 times
  3 → appears 3 times

Step 2 - Sort by frequency (descending):
  3 (freq=3)
  2 (freq=2)
  1 (freq=1)

Step 3 - Take top k=2:
  3, 2

Return: [3, 2] (or [2, 3] — order doesn't matter)
```

## Complexity

**Time:** O(N + M log M) where N = length of nums, M = number of distinct elements
- Build frequency map: O(N)
- Sort M entries: O(M log M) — dominated by LINQ's OrderByDescending
- Take and select: O(k) — negligible
- Total: O(N + M log M)

**Space:** O(M) where M = number of distinct elements
- Dictionary stores at most M entries (one per unique number)
- M is bounded by nums.length, so worst case O(N)

## Edge Cases

- **k = 1** (single most frequent): Just return the element with highest frequency
- **k = number of distinct elements** (all elements): Return all unique numbers
- **All same frequency** (e.g., `[1,2,3,4]`, k=2): Any 2 elements work (order doesn't matter)
- **Single unique element** (`[5,5,5]`, k=1): Return [5]
- **Array of length 1** (`[x]`, k=1): Return [x]

## Key Insight

**Frequency mapping is fundamental for "find top k" problems!**

Once you have frequencies, sorting by frequency is the natural next step. The beauty is:
- **Dictionary** captures "what elements exist and how often"
- **Sorting** ranks them by frequency automatically
- **Selection** takes just the top k

More advanced approaches (heap, bucket sort) optimize this further, but this frequency + sort approach is clean and efficient enough for most cases.

## Pattern Recognition for Similar Problems

🚩 **Red flags for TOP-K or FREQUENCY RANKING problems:**
1. **"Top K elements"** → FREQUENCY COUNTING + SORTING ⭐⭐⭐
2. **"K most frequent"** → FREQUENCY COUNTING + SORTING
3. **"Sort by count"** or **"rank by occurrence"** → FREQUENCY COUNTING
4. **"Find the top/bottom N"** → Often involves counting then ranking
5. **"K least frequent"** → Same approach, reverse sort order

**Similar Problems:** "Kth Largest Element in Array", "Top K Words", "Least Number of Unique Integers After Removal"

---

## My Solution Approach

### **Step 1: Initialize Frequency Dictionary**
```csharp
Dictionary<int, int> freq = new();
```

Maps each number to how many times it appears in the array.

### **Step 2: Count Frequencies**
```csharp
foreach (int num in nums)
{
    if (freq.ContainsKey(num))
    {
        freq[num]++;
    }
    else
    {
        freq[num] = 1;
    }
}
```

I iterate through all numbers and increment their frequency count. This builds a complete frequency map.

**Note:** Could also use `TryGetValue` for a more compact pattern:
```csharp
foreach (int num in nums)
{
    freq[num] = freq.GetValueOrDefault(num) + 1;
}
```

But my explicit approach is clear and readable.

### **Step 3: Sort by Frequency (Descending)**
```csharp
freq.OrderByDescending(kvp => kvp.Value)
```

LINQ's `OrderByDescending` sorts dictionary entries by their **Value** (frequency) from highest to lowest. Most frequent elements appear first.

### **Step 4: Take Top K Elements**
```csharp
.Take(k)
```

Only keep the first k entries from the sorted list.

### **Step 5: Extract Keys and Return**
```csharp
.Select(x => x.Key)
.ToArray()
```

Extract just the numbers (keys) from the key-value pairs and convert to array. The frequencies are discarded; we only need the element values.

### **Why This Works**

1. **Frequency counting** — Captures occurrence count for each unique number
2. **Descending sort** — Places most frequent numbers at the start
3. **Top k selection** — Take exactly k elements
4. **Key extraction** — Return just the numbers, not their frequencies
5. **Simple and clear** — No complex logic, straightforward flow

### **Example Walkthrough - Example 1**

```
Input: nums = [1,2,2,3,3,3], k = 2

Initialize: freq = {}

Count frequencies:
  num=1 → freq[1] = 1
  num=2 → freq[2] = 1
  num=2 → freq[2] = 2
  num=3 → freq[3] = 1
  num=3 → freq[3] = 2
  num=3 → freq[3] = 3

After counting: freq = {1→1, 2→2, 3→3}

Sort descending by frequency:
  3 (freq=3)
  2 (freq=2)
  1 (freq=1)

Take k=2:
  3, 2

Extract keys and convert to array:
  [3, 2]

(Order doesn't matter, so [2, 3] is equally valid)
```

### **Example Walkthrough - Example 2**

```
Input: nums = [7,7], k = 1

Initialize: freq = {}

Count frequencies:
  num=7 → freq[7] = 1
  num=7 → freq[7] = 2

After counting: freq = {7→2}

Sort descending by frequency:
  7 (freq=2)

Take k=1:
  7

Extract keys:
  [7]
```

### **Complexity Summary**

- **Time:** O(N + M log M)
  - Building frequency map: O(N) to iterate through all elements
  - Sorting M distinct elements: O(M log M)
  - Take and Select: O(k) — dominated by sorting
  - Total: O(N + M log M)

- **Space:** O(M)
  - Dictionary stores at most M unique numbers
  - In worst case (all distinct), M = N, so O(N) space

### **Alternative Approaches**

**1. Min-Heap (Priority Queue) — O(N log k)**
- Keep only k elements in a heap
- More efficient if k << M (k is much smaller than number of distinct elements)
- Avoids sorting all M elements, only keeps top k

**2. Bucket Sort — O(N)**
- Frequencies range from 1 to N
- Create buckets for each frequency level
- Place numbers in their frequency buckets
- Collect from highest frequency buckets to get top k
- Optimal when k is a significant fraction of N

My straightforward sort approach is elegant and efficient for most practical cases. It prioritizes clarity over the micro-optimizations that heap or bucket sort offer.
