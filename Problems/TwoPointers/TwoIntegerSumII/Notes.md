# Notes - Two Integer Sum II
First thing to notice is this problem states that its 1 indexed!!
So make sure whatever result you give that its based on a 1 indexed array.

## Pattern / Approach
Two pointers 

One beginning on the left and another on the right
## Data Structure(s) Used
None
## Thought Process

**Initialize two pointers:**
- `left` at index 0 (will return as 1-indexed)
- `right` at index `length - 1` (will return as 1-indexed)

**Example with array `[1, 3, 4, 5, 7, 10, 11]`:**
```
Index (0-indexed):  0   1   2   3   4   5    6
                   [1] [3] [4] [5] [7] [10] [11]
                    ↑                         ↑
                  left                      right
```

**Algorithm:**
1. Calculate sum of `numbers[left] + numbers[right]`
2. If sum equals target → return `[left+1, right+1]` (convert to 1-indexed)
3. If sum is too small → move `left` pointer right (increase sum)
4. If sum is too large → move `right` pointer left (decrease sum)

## Complexity
**Time:** O(N)  
**Space:** O(1) — Only using two pointers, no extra data structures

## Edge Cases

- **Minimum array length** (`[a, b]` with 2 elements): Should still work, left and right will find the pair
- **Negative numbers** (`[-5, 5]`): Algorithm doesn't care about sign, just sum comparison
- **All same numbers** (`[1, 1, 1, 1]` target=2): Will find first two valid indices
- **Target at extremes**: Smallest sum (first + second) or largest sum (second-to-last + last)

## Key Insight

**The sorted array is crucial!** Because the array is sorted:
- If sum is too large → move right pointer LEFT to decrease sum (smaller numbers are on left)
- If sum is too small → move left pointer RIGHT to increase sum (larger numbers are on right)
- This "narrows" the search space with each comparison

The algorithm works in **O(N) time** with only **O(1) space** because we can eliminate half the problem space at each step by moving one pointer, rather than checking all pairs like we'd need with a hash map.

## Pattern Recognition for Similar Problems

🚩 **Red flags that indicate TWO POINTERS might be the answer:**
1. **"Find a pair"** → Could be hash map OR two pointers
2. **"Find a pair" + "sorted array"** → **TWO POINTERS** ⭐
3. **"No additional space allowed" / "O(1) space"** + **sorted array** → **TWO POINTERS** ⭐⭐
4. **"Container with Most Water"**, **"Valid Palindrome"**, **"Merge Sorted Lists"** → Two pointers
5. When you see **constraints about space**, think two pointers first on sorted data

**Similar Problems:** "Valid Palindrome", "Container with Most Water", "3Sum", "4Sum"
