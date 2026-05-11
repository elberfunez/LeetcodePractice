# Notes - Contains Duplicate

## Pattern / Approach

Use a **HashSet** to track numbers we've seen. As we iterate through the array, try to add each number to the set. If a number is already in the set (Add returns false), we've found a duplicate.

## Data Structure(s) Used

- **HashSet<int>** — Stores unique numbers we've encountered. HashSet.Add() returns `false` if the element already exists.

## Thought Process

**Key insight:** A HashSet only stores unique values. If we try to add a value that's already in the set, it fails (returns false).

This is perfect for duplicate detection:
1. Keep a set of numbers we've seen
2. For each number in the array:
   - Try to add it to the set
   - If add fails → we've seen this number before → **duplicate found!**
   - If add succeeds → continue
3. If we get through the whole array without duplicates → return false

```
Example: [1, 2, 3, 3]

seen = {}
num=1 → seen.Add(1) = true → seen = {1}
num=2 → seen.Add(2) = true → seen = {1, 2}
num=3 → seen.Add(3) = true → seen = {1, 2, 3}
num=3 → seen.Add(3) = false ✗ (3 already in set!)
        → return true (duplicate found!) ✓
```

## Complexity

**Time:** O(N) — Single pass through array, each Add() is O(1) average case  
**Space:** O(N) — HashSet stores up to N unique values in worst case

## Edge Cases

- **Empty array** (`[]`): Length < 2, return false immediately
- **Single element** (`[1]`): Length < 2, return false
- **Two same elements** (`[1, 1]`): First 1 adds successfully, second 1 fails → return true
- **All unique** (`[1, 2, 3, 4]`): All adds succeed, return false at end
- **All same** (`[5, 5, 5, 5]`): First 5 adds, rest fail → return true at second element
- **Large numbers** (`[1000000000, -1000000000]`): Works fine, HashSet works with any int

## Key Insight

**HashSet.Add() elegantly combines existence check + insertion!**

Instead of:
```csharp
if (seen.Contains(num)) {
    return true;  // found duplicate
}
seen.Add(num);
```

Just use:
```csharp
if (!seen.Add(num)) {
    return true;  // Add returns false if already exists
}
```

Benefits:
- **One operation** instead of two (no separate Contains check)
- **Cleaner code** — More readable intent
- **Slightly more efficient** — One lookup instead of potentially two
- **Idiomatic C#** — This is how pros write it

## Pattern Recognition for Similar Problems

🚩 **Red flags for HASH SET problems:**
1. **"Contains X"** or **"find if exists"** → HASH SET ⭐⭐⭐
2. **"Duplicate"** → HASH SET ⭐⭐⭐
3. **"Unique elements"** or **"distinct count"** → HASH SET
4. **"O(N) time, O(N) space"** → Often HASH SET
5. **"Track what we've seen"** → HASH SET

**When to use HashSet vs Dictionary:**
- **HashSet** → Only need to track existence (is it there? yes/no)
- **Dictionary** → Need to store values with associated data

**Similar Problems:** "Valid Sudoku" (use sets to track row/col/box), "Two Sum" (use dictionary for values), "Happy Number" (track cycles with set)

---

## Your Solution Approach

### **Step 1: Edge Case - Minimum Length**
```csharp
if (nums.Length < 2) return false;
```

Optimization: Can't have duplicates with 0 or 1 element. Return immediately.

### **Step 2: Create HashSet**
```csharp
HashSet<int> seen = new();
```

Will track numbers we've already encountered.

### **Step 3: Iterate and Try to Add**
```csharp
foreach (int num in nums)
{
    if (!seen.Add(num)) 
    {
        return true;
    }
}
```

**The magic is `!seen.Add(num)`:**
- `seen.Add(num)` returns `true` if the element was successfully added
- `seen.Add(num)` returns `false` if the element already existed
- `!` negates it: `!true = false`, `!false = true`

So we return `true` (duplicate found) when Add returns false (element already exists).

### **Step 4: No Duplicates Found**
```csharp
return false;
```

If we complete the loop without finding a duplicate, no duplicates exist.

### **Why This Works**

1. **HashSet properties** — Only stores unique values
2. **Add() return value** — False means element already exists
3. **Single pass** — One iteration through the array
4. **Early exit** — Return immediately when duplicate found
5. **Clean code** — Minimal and readable logic

### **Example Walkthrough - Has Duplicate**

```
Input: [1, 2, 3, 3]

Length = 4 >= 2 ✓
seen = {}

num=1:
  seen.Add(1) → true (1 not in set, added)
  !true = false → continue
  seen = {1}

num=2:
  seen.Add(2) → true (2 not in set, added)
  !true = false → continue
  seen = {1, 2}

num=3:
  seen.Add(3) → true (3 not in set, added)
  !true = false → continue
  seen = {1, 2, 3}

num=3:
  seen.Add(3) → false (3 already in set, NOT added)
  !false = true → return true ✓ DUPLICATE FOUND!
```

### **Example Walkthrough - No Duplicate**

```
Input: [1, 2, 3, 4]

Length = 4 >= 2 ✓
seen = {}

num=1: seen.Add(1) → true, continue, seen = {1}
num=2: seen.Add(2) → true, continue, seen = {1, 2}
num=3: seen.Add(3) → true, continue, seen = {1, 2, 3}
num=4: seen.Add(4) → true, continue, seen = {1, 2, 3, 4}

Loop completes without returning
return false ✓ NO DUPLICATES
```

### **Example Walkthrough - Edge Case**

```
Input: []

Length = 0 < 2 → return false immediately ✓
(Can't have duplicates in empty array)
```

Another edge case:

```
Input: [5]

Length = 1 < 2 → return false immediately ✓
(Single element can't be a duplicate)
```

### **Complexity Summary**
- **Time:** O(N) — Each element processed once, Add() is O(1) average
- **Space:** O(N) — HashSet grows with unique elements (worst case all unique = N elements)

### **Why HashSet Over Dictionary?**

For this problem, HashSet is perfect:
- We only care about **"is it there?"** not **"what's the value?"**
- HashSet is lighter than Dictionary (no key-value overhead)
- `Add()` return value gives us the info we need

If the problem asked "map each number to its index", we'd use Dictionary instead.
