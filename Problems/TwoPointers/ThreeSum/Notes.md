# Notes - 3Sum

## Pattern / Approach

Use **two pointers after sorting** combined with **fixing one element**. Sort the array, then for each element, use two pointers to find pairs that make the total sum equal to 0. The key is **skipping duplicates** to avoid duplicate triplets.

## Data Structure(s) Used

- Sorted array (enables two-pointer technique)
- Two pointer indices (left, right)
- Result list to store triplets

## Thought Process

Finding three numbers that sum to 0 is O(N³) with brute force. But we can **reduce it to a two-pointer problem**:

1. **Sort the array** — Enables two-pointer technique
2. **Fix one element** at a time: `nums[i]`
3. **Find two elements** that make the total sum = 0
4. **Use two pointers** on the remaining array to find pairs efficiently

```
Example: nums = [-1, 0, 1, 2, -1, -4]
After sort: [-4, -1, -1, 0, 1, 2]

Fix nums[0] = -4, need left+right = 4 (to make -4 + left + right = 0)
  No valid pairs sum to 4

Fix nums[1] = -1, need left+right = 1 (to make -1 + left + right = 0)
  Found: -1 + 0 + 1 = 0 ✓
  Found: -1 + (-1) + 2 = 0 ✓
  (Carefully skip duplicates to avoid duplicate triplets)

Fix nums[2] = -1, skip (duplicate of previous)
```

**Algorithm:**
1. Sort the array
2. For each index `i` from 0 to n-3:
   - Skip if `nums[i]` is duplicate of previous element
   - Initialize two pointers: `left = i+1`, `right = n-1`
   - While `left < right`:
     - Calculate `total = nums[i] + nums[left] + nums[right]`
     - If `total == 0`: Add triplet, skip all duplicates on both sides, move both pointers
     - If `total < 0`: Move left pointer right (need bigger numbers)
     - If `total > 0`: Move right pointer left (need smaller numbers)
3. Return all unique triplets

## Complexity

**Time:** O(N²)
- Sorting: O(N log N)
- Outer loop: O(N) iterations
- Inner two-pointer loop: O(N) per iteration
- Total: O(N log N) + O(N²) = O(N²)

**Space:** O(1) extra space (excluding output array)

## Edge Cases

- **All zeros** (`[0, 0, 0]`): Result is `[[0, 0, 0]]`, duplicate handling critical
- **No valid triplets** (`[0, 1, 1]`): Return empty list
- **Negative numbers** (`[-1, -2, 3]`): Two pointers work with negatives
- **Duplicate elements** (`[-1, -1, 2, 0, 1]`): Must skip duplicates at three places
- **All negative** (`[-3, -2, -1]`): No valid triplet sums to 0

## Key Insight

**Duplicate handling is the hardest part!** You must skip duplicates at exactly the right time and place, and check `left < right` while skipping to avoid boundary issues.

---

## My Solution Approach

### **Step 1: Sort the Array**
```csharp
Array.Sort(nums);
```

Sorting is **essential**—it enables:
- Two-pointer technique (move pointers based on comparison)
- Duplicate skipping (identical values are adjacent)
- Early termination optimization (if `nums[i] > 0`, no valid triplets exist)

### **Step 2: Fix One Element & Loop**
```csharp
for (int i = 0; i < nums.Length - 2; i++) {
    // Skip duplicate values for the first element to avoid duplicate triplets
    if (i > 0 && nums[i] == nums[i - 1]) {
        continue;
    }
    // ... find pairs summing to -nums[i]
}
```

For each `nums[i]`, we need to find two numbers that sum to `-nums[i]` so the total is 0.

**Duplicate skip:** If we've already processed this value, skip it entirely. Otherwise we'd get duplicate triplets.

### **Step 3: Initialize Two Pointers**
```csharp
int left = i + 1;
int right = nums.Length - 1;
```

- `left` starts right after `i` (can't reuse `nums[i]`)
- `right` starts at the end
- They converge toward each other

### **Step 4: Two-Pointer Loop with Three Cases**
```csharp
while (left < right) {
    int total = nums[i] + nums[left] + nums[right];
    
    if (total < 0) {
        // Sum too small, move left pointer right to increase sum
        left++;
    } else if (total > 0) {
        // Sum too large, move right pointer left to decrease sum
        right--;
    } else {
        // Found a valid triplet
        result.Add(new List<int> { nums[i], nums[left], nums[right] });
        
        // Skip all duplicate values to avoid duplicate triplets
        while (left < right && nums[left] == nums[left + 1]) {
            left++;
        }
        while (left < right && nums[right] == nums[right - 1]) {
            right--;
        }
        // Move both pointers to continue searching
        left++;
        right--;
    }
}
```

**Three cases:**

1. **`total < 0`** — Sum is too small, we need larger numbers. Since array is sorted, move left pointer RIGHT to increase the sum.

2. **`total > 0`** — Sum is too large, we need smaller numbers. Move right pointer LEFT to decrease the sum.

3. **`total == 0`** — Found a valid triplet! 
   - Add it to results
   - Skip all duplicate values on the LEFT side (while maintaining `left < right`)
   - Skip all duplicate values on the RIGHT side (while maintaining `left < right`)
   - Move both pointers to continue searching for more triplets

### **⚠️ CRITICAL: Duplicate Skipping Order**

The order is **essential**:

```csharp
// 1️⃣ First, skip all duplicate left values
while (left < right && nums[left] == nums[left + 1]) {
    left++;
}
// 2️⃣ Then, skip all duplicate right values  
while (left < right && nums[right] == nums[right - 1]) {
    right--;
}
// 3️⃣ THEN move both pointers
left++;
right--;
```

**Why this order matters:**
- Must skip duplicates **BEFORE** incrementing/decrementing the pointers
- Must check `left < right` **during** skipping to avoid boundary issues
- Skip LEFT first, then RIGHT — this ensures both complete before moving
- If you move first and skip after, you might miss valid triplets or crash

### **Example Walkthrough**

```
Input: [-1, 0, 1, 2, -1, -4]
After sort: [-4, -1, -1, 0, 1, 2]

i=0: nums[0]=-4, need total=0, so left+right must equal 4
  left=1(-1), right=5(2): total=-4+(-1)+2=-3 (too small) → left++
  left=2(-1), right=5(2): total=-4+(-1)+2=-3 (too small) → left++
  left=3(0), right=5(2): total=-4+0+2=-2 (too small) → left++
  left=4(1), right=5(2): total=-4+1+2=-1 (too small) → left++
  left=5, right=5: exit loop, no triplets found

i=1: nums[1]=-1, need total=0, so left+right must equal 1
  left=2(-1), right=5(2): total=-1+(-1)+2=0 ✓ Found!
    Add [-1, -1, 2]
    Skip left duplicates: nums[2]==-1 == nums[3]? No, so stop
    Skip right duplicates: nums[5]==2 == nums[4]? No, so stop
    left++, right-- → left=3, right=4
  
  left=3(0), right=4(1): total=-1+0+1=0 ✓ Found!
    Add [-1, 0, 1]
    Skip left duplicates: nums[3]==0 == nums[4]? No, so stop
    Skip right duplicates: nums[4]==1 == nums[3]? No, so stop
    left++, right-- → left=4, right=3
  
  left=4, right=3: exit loop (left >= right)

i=2: nums[2]=-1, skip (equals nums[1], duplicate of previous i)

i=3: nums[3]=0, need total=0, so left+right must equal 0
  left=4(1), right=5(2): total=0+1+2=3 (too large) → right--
  left=4, right=4: exit loop (left >= right)

Result: [[-1, -1, 2], [-1, 0, 1]] ✓
```

### **Why This Works**

1. **Sorted array** → Two pointers efficiently eliminate half the search space per comparison
2. **Fix one, solve for two** → Reduces O(N³) brute force to O(N²)
3. **Careful duplicate skipping** → Ensures no duplicate triplets in output
4. **Two-pointer movement is optimal** → Never needs to backtrack

### **Time Complexity Breakdown**

```
Sorting:              O(N log N)
Outer loop:           O(N) iterations
  Inner while loop:   O(N) total across all iterations (pointers only move, never reset)
Total:                O(N log N) + O(N²) = O(N²)
```

The inner loop is O(N) **total**, not O(N) per iteration, because left and right pointers move monotonically—they never reset or backtrack.
