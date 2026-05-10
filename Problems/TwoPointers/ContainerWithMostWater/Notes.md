# Notes - Container With Most Water

## Pattern / Approach

Two pointers starting from the **widest position** (ends of array), moving **inward** toward each other. At each step, calculate the area and track the maximum.

## Data Structure(s) Used

- Two pointers (`l` and `r`)
- Variable to track `maxArea`
- No extra data structures

## Thought Process

**The key insight:** Start with maximum width and work inward.

Why? Because:
1. The widest container is at the start: `l=0, r=length-1`
2. Area = `min(height[l], height[r]) * distance`
3. As we move inward, distance ALWAYS decreases
4. So to find a better area, we need height to increase
5. Move the **shorter bar** pointer inward — it's the bottleneck

**Strategy:**
- If `heights[l] < heights[r]` → left bar is limiting, move `l` right
- If `heights[l] >= heights[r]` → right bar is limiting, move `r` left
- Track the maximum area throughout

```
Example: heights = [1,7,2,5,4,7,3,6]

Initial:  l=0(1), r=7(6)
          area = min(1,6) * 7 = 7, maxArea = 7
          1 < 6 → move l++

Step 2:   l=1(7), r=7(6)
          area = min(7,6) * 6 = 36, maxArea = 36
          7 >= 6 → move r--

Step 3:   l=1(7), r=6(3)
          area = min(7,3) * 5 = 15
          7 >= 3 → move r--

...continue until l >= r

Result: maxArea = 36
```

## Complexity

**Time:** O(N) — Single pass with two pointers from ends to middle  
**Space:** O(1) — Only using two pointer variables and maxArea

## Edge Cases

- **All same height** (`[2,2,2]`): Every pair gives same area, last pair is `min(2,2) * 1 = 2`
- **Two bars** (`[1,2]`): Only option, area = `min(1,2) * 1 = 1`
- **Increasing then decreasing** (`[1,2,3,4,5,4,3,2,1]`): Maximum is at peak, area = `min(5,5) * 1 = 5`
- **One very tall bar** (`[1,1000,1]`): Second bar is bottleneck, area = `min(1000,1) * 2 = 2`

## Key Insight

**Moving the shorter bar is optimal!** This is the critical insight that makes the algorithm work:

- Width always decreases as pointers move inward
- To beat current `maxArea`, we need height to increase
- The **shorter bar is the bottleneck** — increasing it might give a better area
- The **taller bar can't help** — moving toward it just decreases width without increasing min height

Example:
```
[1, 7, 2, 5]
 L        R
area = min(1, 5) * 3 = 3 (limited by left bar height=1)

If we move RIGHT (taller side): [1, 7, 2]
area = min(1, 2) * 2 = 2 (WORSE! width decreased, height still limited by left)

If we move LEFT (shorter side): [7, 2, 5]
area = min(7, 5) * 2 = 10 (BETTER! might find taller bar on left side)
```

## Pattern Recognition for Similar Problems

🚩 **Red flags for TWO POINTERS (start from ends):**
1. **"Maximum area/distance between two elements"** → TWO POINTERS from ends ⭐⭐
2. **"Widest container"** or **"maximize/minimize distance"** → TWO POINTERS from ends
3. When you need to explore from both ends working inward → TWO POINTERS
4. When width/distance decreases but you want to optimize another dimension → TWO POINTERS

**Similar Problems:** "Trapping Rain Water", "Max Area of Island" (variation), "Two Sum II"

---

## Your Solution Approach

### **Step 1: Initialize Pointers**
```csharp
int l = 0;
int r = heights.Length - 1;
int maxArea = 0;
```

Start at both ends (widest possible container) and track the maximum area found.

### **Step 2: Calculate Area at Current Position**
```csharp
int length = r - l;
int width = Math.Min(heights[l], heights[r]);  // Height is bottleneck
int area = length * width;
```

Area formula: `distance * min(left_height, right_height)`

### **Step 3: Update Maximum**
```csharp
if (area > maxArea)
{
    maxArea = area;
}
```

Track the best area seen so far.

### **Step 4: Move the Shorter Pointer**
```csharp
if (heights[l] < heights[r]) 
    l++;          // Left bar is shorter, move it inward
else 
    r--;          // Right bar is shorter (or equal), move it inward
```

This is the **critical decision**. Moving the shorter bar gives us a chance to find a taller bar that might compensate for the decreased width.

### **Step 5: Repeat Until Pointers Meet**
```csharp
while (l < r)
{
    // ... repeat steps 2-4
}
```

Continue until the two pointers meet, exploring all promising container positions.

### **Why This Works**

1. **We start with maximum width** — Any other pair of bars will have less or equal distance
2. **We only move the shorter bar** — This is the only way to potentially improve the area
3. **We track the global maximum** — No matter which bars we explore, we keep the best result
4. **Linear time complexity** — Each bar is visited at most once by either pointer

### **Example Walkthrough**

```
Input: [1,7,2,5,4,7,3,6]

Initial: l=0, r=7
         area = min(1,6) * 7 = 7, maxArea = 7
         1 < 6 → move l++ (left is shorter)

Step 2:  l=1, r=7
         area = min(7,6) * 6 = 36, maxArea = 36 ✓
         7 >= 6 → move r-- (right is shorter/equal)

Step 3:  l=1, r=6
         area = min(7,3) * 5 = 15
         7 >= 3 → move r--

Step 4:  l=1, r=5
         area = min(7,7) * 4 = 28
         7 >= 7 → move r--

Step 5:  l=1, r=4
         area = min(7,4) * 3 = 12
         7 >= 4 → move r--

Step 6:  l=1, r=3
         area = min(7,5) * 2 = 10
         7 >= 5 → move r--

Step 7:  l=1, r=2
         area = min(7,2) * 1 = 2
         7 >= 2 → move r--

Result: maxArea = 36 (found at step 2)
```

### **Complexity**
- **Time**: O(N) — Two pointers traverse the array once
- **Space**: O(1) — Only using a few variables
