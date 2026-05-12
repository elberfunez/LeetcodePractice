# Notes - 3Sum
\
## Pattern / Approach

Two pointers applied AFTER fixing one element. Sort the array, then for each element, use two pointers to find pairs that sum to `0 - nums[i]`.

## Data Structure(s) Used

- Sorted array (requires sorting)
- HashSet or manual duplicate tracking (to avoid duplicate triplets)

## Thought Process

Finding three numbers that sum to 0 is hard with brute force (O(N³)). But we can **reduce it to a two-sum problem**:

1. **Sort the array** — Enables two-pointer technique
2. **Fix one element** at a time: `nums[i]`
3. **Find two elements** that sum to `target - nums[i]` (which is `0 - nums[i]`)
4. **Use two pointers** (left and right) for the remaining array

```
Example: nums = [-1, 0, 1, 2, -1, -4]
After sort: [-4, -1, -1, 0, 1, 2]
            [ 0   1   2  3  4  5]  (indices)

Fix nums[0] = -4, need two numbers that sum to 4
  [-4, -1, -1,  0,  1,  2]
    L                      R
  → No triplets sum to 0 with -4

Fix nums[1] = -1, need two numbers that sum to 1
  [-4, -1, -1,  0,  1,  2]
        L                  R
  → Found: -1 + 0 + 1 = 0 ✓
  → Found: -1 + (-1) + 2 = 0 ✓

Fix nums[2] = -1, skip (duplicate of i=1)

Fix nums[3] = 0, need two numbers that sum to 0
  [-4, -1, -1,  0,  1,  2]
              L           R
  → No valid pairs sum to 0
```

**Algorithm:**
1. Sort the array
2. For each index `i`:
   - Skip if duplicate (same as previous element)
   - Set `target = -nums[i]`
   - Use two pointers on `nums[i+1...end]` to find pairs summing to target
   - When you find a pair, skip duplicates before moving pointers
3. Return all unique triplets

## Complexity

**Time:** O(N²) — Sort is O(N log N), then for each element we do two-pointer pass O(N)  
**Space:** O(1) or O(N) depending on how you count the output (sorting may use extra space)

## Edge Cases

- **All zeros** (`[0, 0, 0]`): Result is `[[0, 0, 0]]`, must handle duplicate carefully
- **No valid triplets** (`[0, 1, 1]`): Return empty list
- **Negative numbers** (`[-1, -2, 3]`): Works fine, two pointers don't care about sign
- **Duplicate elements** (`[-1, -1, 2, 0, 1]`): Must skip duplicates at `i` and when moving pointers to avoid duplicate triplets
- **All negative** (`[-3, -2, -1]`): No valid triplet summing to 0

## Key Insight

**The duplicate handling is the hardest part!** You must skip duplicates in three places:
1. Skip duplicate `i` values: `if (i > 0 && nums[i] == nums[i-1]) continue`
2. Skip duplicate `left` values: `while (l < r && nums[l] == nums[l+1]) l++`
3. Skip duplicate `right` values: `while (l < r && nums[r] == nums[r-1]) r--`

Without this, you'll return duplicate triplets and fail the test.

The sorting also optimizes it: after sorting, if `nums[i] > 0`, no more valid triplets exist (remaining numbers are all positive).

## Pattern Recognition for Similar Problems

🚩 **Red flags for TWO POINTERS + SORTING:**
1. **"Find N numbers that sum to X"** → **TWO POINTERS** ⭐⭐
2. **"Return all unique combinations"** + **sorted data** → TWO POINTERS
3. **Need to avoid duplicates** + **sorted data** → TWO POINTERS
4. **3Sum, 4Sum, 3Sum Closest** → ALL use this pattern

**How to recognize:**
- If it says "find a pair/triplet/group" + "sum to X" → Think two pointers
- If it says "no duplicates in output" → You'll need to sort and skip duplicates
- If it asks for combinations (not permutations) → Two pointers after sorting

**Similar Problems:** "3Sum Closest", "4Sum", "Two Sum II"

---

## My Solution Approach

### **Step 1: Edge Case & Sorting**
```csharp
if (nums.Length < 3) return result;  // Can't make triplet with < 3 numbers
Array.Sort(nums);                    // Sort to enable two-pointer technique
```

Sorting is **crucial** — it allows me to use the two-pointer optimization and enables duplicate skipping.

### **Step 2: Fix One Element in a Loop**
```csharp
for (int i = 0; i < nums.Length - 2; i++) {
    // ... process each element as the "fixed" number
}
```

For each number `nums[i]`, find two other numbers that sum to `-nums[i]`.

### **Step 3: Smart Optimization**
```csharp
if (nums[i] > 0) break;  // If current number is positive, stop
```

This is a **game-changer**. Since the array is sorted, if `nums[i] > 0`, all remaining numbers are also positive. No way to sum to 0. Stop early.

### **Step 4: Skip Duplicate Fixed Elements**
```csharp
if (i > 0 && nums[i] == nums[i - 1]) continue;
```

If we've already processed this value, skip it. Otherwise we'd get duplicate triplets in our result.

### **Step 5: Set Target & Initialize Two Pointers**
```csharp
int target = -nums[i];          // What left + right must equal
int left = i + 1;              // Start after current element
int right = nums.Length - 1;   // Start at end
```

If `nums[left] + nums[right] == target`, then `nums[i] + nums[left] + nums[right] = 0`.

### **Step 6: Two-Pointer Loop with Three Branches**
```csharp
while (left < right) {
    int sum = nums[left] + nums[right];
    
    if (sum == target) {
        // ✓ Found triplet
        result.Add(new List<int> { nums[i], nums[left], nums[right] });
        
        // ⚠️ CRITICAL: Order of duplicate skipping matters!
        // Skip duplicates on LEFT first (while l < r is still true)
        while (left < right && nums[left] == nums[left + 1]) left++;
        
        // Then skip duplicates on RIGHT (while l < r is still true)
        while (left < right && nums[right] == nums[right - 1]) right--;
        
        // THEN move both pointers after duplicates are skipped
        left++;
        right--;
    } 
    else if (sum < target) {
        left++;   // Need larger sum, move left pointer right
    } 
    else {
        right--; // Need smaller sum, move right pointer left
    }
}
```

**This is the core logic:**
- **Match**: Add triplet, skip duplicates on BOTH sides, THEN move both pointers
- **Too small**: Move left right (get bigger numbers)
- **Too large**: Move right left (get smaller numbers)

### **⚠️ IMPORTANT: Duplicate Skipping Order**

The **order and placement** of duplicate skipping is **critical**:

```csharp
if (sum == needed)
{
    result.Add(new List<int> { nums[i], nums[l], nums[r] });
    
    // 1️⃣ Skip forward duplicates on LEFT side
    while (l < r && nums[l] == nums[l + 1]) l++;
    
    // 2️⃣ Skip backward duplicates on RIGHT side
    while (l < r && nums[r] == nums[r - 1]) r--;
    
    // 3️⃣ THEN move both pointers
    l++;
    r--;
}
```

**Why the order matters:**
- You must skip duplicates **BEFORE** moving the pointers, not after
- You must check `l < r` **while skipping**, not just once at the start
- Skip LEFT first, then RIGHT — both must complete before moving `l++` and `r--`
- If you move pointers first and skip after, you might miss valid triplets or create boundary issues

This ensures you explore all unique combinations without skipping valid pairs.

### **Why This Works**

1. **Sorted array** → Two pointers work (can eliminate half the search space per comparison)
2. **Fix one, solve for two** → Reduces O(N³) brute force to O(N²)
3. **Duplicate skipping** → Ensures no duplicate triplets in output
4. **Early termination** → If `nums[i] > 0`, break (can't sum to 0)

### **Example Walkthrough**

```
Input: [-1, 0, 1, 2, -1, -4]
After sort: [-4, -1, -1, 0, 1, 2]

i=0: nums[0]=-4, target=4
  left=1(-1), right=5(2) → -1+2=1 (too small) → left++
  left=2(-1), right=5(2) → -1+2=1 (too small) → left++
  left=3(0), right=5(2) → 0+2=2 (too small) → left++
  left=4(1), right=5(2) → 1+2=3 (too small) → left++
  left=5, right=5 → loop exits, no triplets

i=1: nums[1]=-1, target=1
  left=2(-1), right=5(2) → -1+2=1 ✓ Found! Add [-1,-1,2]
  Skip duplicates on right (none)
  left++, right--
  left=3(0), right=4(1) → 0+1=1 ✓ Found! Add [-1,0,1]
  left++, right--
  left=4, right=3 → loop exits

i=2: nums[2]=-1, skip (duplicate of i=1)

i=3: nums[3]=0, target=0
  left=4(1), right=5(2) → 1+2=3 (too large) → right--
  left=4, right=4 → loop exits

Result: [[-1,-1,2], [-1,0,1]] ✓
```

### **Complexity**
- **Time**: O(N²) — Sort is O(N log N), outer loop is O(N), inner two-pointer loop is O(N)
- **Space**: O(1) or O(N) depending on if you count the output
