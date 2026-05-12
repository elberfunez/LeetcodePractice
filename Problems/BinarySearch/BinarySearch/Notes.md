# Notes - Binary Search

## Pattern / Approach

Two-pointer binary search: maintain left and right pointers at array boundaries, repeatedly divide the search space in half by comparing the middle element with the target, and adjust pointers to eliminate half the remaining elements each iteration.

## Data Structure(s) Used

- **Two pointers** (left, right) — Track the current search boundaries
- **Middle pointer** — Identifies the midpoint of current search range
- **Array indices** — Direct index-based access for O(1) lookups

## Thought Process

**Key insight:** Since the array is sorted, I can eliminate half the search space with each comparison.

**My algorithm:**
1. Start with left pointer at start (0) and right pointer at end (length - 1)
2. While left <= right (there's still a search space):
   - Calculate middle index: `m = l + (r - l) / 2`
   - Compare `nums[m]` with target:
     - If equal → found it, return index
     - If target > nums[m] → target is in right half, move left pointer right
     - If target < nums[m] → target is in left half, move right pointer left
3. If loop exits without finding target → return -1

```
Example: nums = [-1,0,2,4,6,8], target = 4

Initial: l=0, r=5
  m = 0 + (5-0)/2 = 2
  nums[2] = 2
  target (4) > 2 → search right half
  l = 3

Iteration 2: l=3, r=5
  m = 3 + (5-3)/2 = 4
  nums[4] = 6
  target (4) < 6 → search left half
  r = 3

Iteration 3: l=3, r=3
  m = 3 + (3-3)/2 = 3
  nums[3] = 4
  target == 4 → return 3 ✓
```

## Complexity

**Time:** O(log n) where n = array length
- Each iteration eliminates half the search space
- Maximum iterations = log₂(n)
- Comparison, calculation, and pointer updates all O(1)

**Space:** O(1)
- Only uses a few variables (left, right, middle)
- No extra data structures or recursion

## Edge Cases

- **Single element** (`[5]`, target=5): m=0, nums[0]==5, return 0 ✓
- **Target at start** ([-1, 0, 2], target=-1): Found on first check or quickly
- **Target at end** ([-1, 0, 2], target=2): Binary search will navigate to it
- **Target not found** ([-1, 0, 2], target=5): Loop exits, return -1
- **Negative numbers** ([-100, -50, 0, 50]: Still works, sorting is what matters
- **Large array**: O(log n) handles 10,000 elements efficiently (≤14 iterations)

## Key Insight

**Binary search is THE canonical O(log n) algorithm.**

Why this works:
- **Sorted array** — Enables me to make smart decisions about which half to search
- **Divide and conquer** — Each comparison eliminates half the remaining elements
- **No redundant work** — Never revisit eliminated portions
- **Pointer arithmetic** — `l + (r - l) / 2` avoids integer overflow
- **Condition is l <= r** — Handles single-element case correctly

Important: Using `(l + r) / 2` can overflow if l and r are very large. I used `l + (r - l) / 2` which is mathematically equivalent but safe.

## Pattern Recognition for Similar Problems

🚩 **Red flags for BINARY SEARCH problems:**
1. **"Sorted array"** + **"find X"** → BINARY SEARCH ⭐⭐⭐
2. **"O(log n)"** requirement → Usually BINARY SEARCH
3. **"First occurrence"** or **"last occurrence"** → BINARY SEARCH variant
4. **"Search in rotated array"** → BINARY SEARCH with twist
5. **"Find position to insert"** → BINARY SEARCH
6. **"Minimize/maximize something"** (on sorted data) → Often BINARY SEARCH

**Similar Problems:** "Search Insert Position", "Search in Rotated Sorted Array", "Find First and Last Position", "Peak Index in Mountain Array"

---

## My Solution Approach

### **Step 1: Initialize Pointers**
```csharp
int l = 0;
int r = nums.Length - 1;
```

I start with left pointer at the beginning and right pointer at the end. This defines my initial search space.

### **Step 2: Loop While Search Space Exists**
```csharp
while (l <= r) {
```

I continue searching as long as `l <= r`. When `l > r`, the search space is exhausted (I've checked all possibilities).

The condition `l <= r` ensures I handle the single-element case (when `l == r`, I still check that one element).

### **Step 3: Calculate Middle Index (Safely)**
```csharp
int m = l + (r - l) / 2;
```

I find the middle of the current search space.

**Important:** I use `l + (r - l) / 2` instead of `(l + r) / 2`:
- `(l + r) / 2` can overflow if l and r are very large
- `l + (r - l) / 2` is mathematically equivalent but avoids overflow

### **Step 4: Compare and Decide**
```csharp
if (nums[m] == target) {
    return m;
} else if (target > nums[m]) {
    l = m + 1;
} else {
    r = m - 1;
}
```

Three cases:
1. **Found it:** `nums[m] == target` → Return the index immediately
2. **Target is larger:** `target > nums[m]` → Target must be in the right half, so move left boundary to `m + 1`
3. **Target is smaller:** `target < nums[m]` → Target must be in the left half, so move right boundary to `m - 1`

I skip `m` itself when moving boundaries (`m + 1` and `m - 1`) because I already checked it.

### **Step 5: Target Not Found**
```csharp
return -1;
```

If the loop exits without returning, the target doesn't exist in the array.

### **Why This Works**

1. **Sorted array guarantee** — I can safely assume smaller values are left, larger values are right
2. **Elimination** — Each iteration throws away half the search space
3. **Convergence** — With n elements, maximum log₂(n) iterations before l > r
4. **Correctness** — I only return when I find the exact target
5. **Completeness** — I check all positions before declaring "not found"

### **Example Walkthrough - Example 1 (Found)**

```
Input: nums = [-1,0,2,4,6,8], target = 4

Initial state:
  l = 0, r = 5
  Array: [-1, 0, 2, 4, 6, 8]
         [ 0, 1, 2, 3, 4, 5] (indices)

Iteration 1:
  m = 0 + (5-0)/2 = 2
  nums[2] = 2
  Compare: target (4) > nums[2] (2)
  → Target is in right half
  → l = m + 1 = 3

Iteration 2:
  l = 3, r = 5
  m = 3 + (5-3)/2 = 4
  nums[4] = 6
  Compare: target (4) < nums[4] (6)
  → Target is in left half
  → r = m - 1 = 3

Iteration 3:
  l = 3, r = 3
  m = 3 + (3-3)/2 = 3
  nums[3] = 4
  Compare: nums[3] == target (4)
  → Found it! Return 3 ✓

Result: 3
```

### **Example Walkthrough - Example 2 (Not Found)**

```
Input: nums = [-1,0,2,4,6,8], target = 3

Initial state:
  l = 0, r = 5

Iteration 1:
  m = 2
  nums[2] = 2
  target (3) > 2 → l = 3

Iteration 2:
  l = 3, r = 5
  m = 4
  nums[4] = 6
  target (3) < 6 → r = 3

Iteration 3:
  l = 3, r = 3
  m = 3
  nums[3] = 4
  target (3) < 4 → r = 2

Check condition: l=3, r=2
  l > r → Exit loop

Return -1 (not found) ✓
```

### **Complexity Summary**

- **Time:** O(log n)
  - Each iteration eliminates half the search space
  - For 10,000 elements: max ~14 iterations (log₂ 10000 ≈ 13.3)
  - Comparison and pointer update: O(1) each
  - Total: O(log n)

- **Space:** O(1)
  - Only uses variables: l, r, m
  - No recursion, no extra data structures
  - Constant memory regardless of input size

### **Alternative Approaches I Considered**

**Linear Search (O(n)):** Slower for large arrays. Binary search is 100× faster for 10,000 elements.

**HashSet Lookup (O(1)):** Faster per lookup, but requires building the set O(n) first. For a single search, binary search is better. If many searches, HashSet wins.

**Built-in Arrays.BinarySearch:** Same O(log n) complexity. My implementation is the fundamental version that built-in methods use.
