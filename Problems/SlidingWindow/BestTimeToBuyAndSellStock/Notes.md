# Notes - Best Time to Buy and Sell Stock

## Pattern / Approach

Single-pass greedy approach: track the minimum price seen so far, and for each price, calculate the profit if I sold at that price (current price - minimum price). Keep track of the maximum profit found throughout the pass.

## Data Structure(s) Used

- **Two variables** (minPrice, maxPrice) — Track the lowest price seen and the best profit found so far
- **Array iteration** — Single pass through the prices array

## Thought Process

**Key insight:** I don't need to check all possible buy/sell pairs. For each sell point, the best buy point is always the lowest price I've seen **before** that point.

**Algorithm:**
1. Initialize minPrice to the first price (the earliest we could have bought)
2. Initialize maxPrice to 0 (no profit if we don't trade)
3. For each price in the array:
   - Update minPrice if current price is lower (found a cheaper buy opportunity)
   - Calculate profit if I sold at current price: `current price - minPrice`
   - Update maxPrice if this profit is better
4. Return maxPrice

```
Example: prices = [10,1,5,6,7,1]

Initialize: minPrice = 10, maxPrice = 0

Price 10:
  minPrice = min(10, 10) = 10
  profit = 10 - 10 = 0
  maxPrice = max(0, 0) = 0

Price 1:
  minPrice = min(10, 1) = 1 (found better buy!)
  profit = 1 - 1 = 0
  maxPrice = max(0, 0) = 0

Price 5:
  minPrice = min(1, 5) = 1 (5 is not lower)
  profit = 5 - 1 = 4
  maxPrice = max(0, 4) = 4

Price 6:
  minPrice = min(1, 6) = 1
  profit = 6 - 1 = 5
  maxPrice = max(4, 5) = 5

Price 7:
  minPrice = min(1, 7) = 1
  profit = 7 - 1 = 6 ✓
  maxPrice = max(5, 6) = 6

Price 1:
  minPrice = min(1, 1) = 1
  profit = 1 - 1 = 0
  maxPrice = max(6, 0) = 6

Return: 6 ✓
```

## Complexity

**Time:** O(N) where N = length of prices array
- Single pass through the array
- Each operation (comparison, subtraction) is O(1)
- Total: O(N)

**Space:** O(1)
- Only two variables (minPrice, maxPrice)
- No extra data structures

## Edge Cases

- **Single element** (`[5]`): minPrice=5, profit=5-5=0, returns 0 (can't buy and sell on same day)
- **Strictly decreasing** (`[10,8,7,5,2]`): minPrice keeps updating but profit never increases, returns 0
- **Strictly increasing** (`[1,2,3,4,5]`): Buy at 1, sell at 5, profit=4
- **All same price** (`[5,5,5]`): minPrice=5, profit always 0, returns 0
- **Single profitable opportunity** (`[3,2,4,1,5]`): Buy at 1, sell at 5, profit=4

## Key Insight

**I only need to track one minimum, not all pairs!**

The breakthrough: for each potential sell price, the optimal buy price is always the **lowest price before it**. I don't need to compare all (buy, sell) pairs — just remember the minimum I've seen and calculate profit against it for each new price.

This transforms the problem from O(N²) brute force (check every pair) to O(N) greedy (single pass).

Why this works:
- **Minimum principle** — The lowest price before any point is the best buy for that sell point
- **Single pass** — I process each price once and make local optimal decisions (greedy)
- **Correctness** — The global maximum profit must occur at some (minPrice, current price) pair
- **Efficiency** — No need to store all prices or compare pairs

## Pattern Recognition for Similar Problems

🚩 **Red flags for GREEDY / SINGLE-PASS problems:**
1. **"Maximum/minimum profit"** → Often GREEDY ⭐⭐⭐
2. **"Track best/worst seen so far"** → GREEDY
3. **"Find best pair/transaction"** + **can only do once** → GREEDY
4. **"No need to look ahead"** → Often GREEDY
5. **"Best time to buy and sell"** → GREEDY ⭐⭐⭐

**Similar Problems:** "Best Time to Buy and Sell Stock II" (multiple transactions), "Stock Span", "Best Time to Buy and Sell with Cooldown"

---

## My Solution Approach

### **Step 1: Initialize Tracking Variables**
```csharp
int minPrice = prices[0];
int maxPrice = 0;
```

I initialize minPrice to the first price (earliest buy opportunity) and maxPrice to 0 (no profit if I don't trade).

### **Step 2: Single Pass Through Prices**
```csharp
foreach (int p in prices)
{
```

I iterate through each price once. The order matters — I can only sell after I buy.

### **Step 3: Update Minimum Price**
```csharp
minPrice = Math.Min(minPrice, p);
```

As I go, I track the lowest price I've seen so far. This is the best buy point for any future sell.

### **Step 4: Calculate Profit at Current Price**
```csharp
maxPrice = Math.Max(maxPrice, p - minPrice);
```

For the current price as a sell point, I calculate profit: `current price - minimum price seen so far`.

I immediately update maxPrice if this profit is the best I've found.

**Why this order matters:** I update minPrice first, then calculate profit. This ensures I don't try to buy and sell on the same day (minPrice gets updated before the profit calculation uses it for future iterations).

### **Step 5: Return Maximum Profit**
```csharp
return maxPrice;
```

After processing all prices, I return the maximum profit found. If no profitable trade exists, maxPrice remains 0.

### **Why This Works**

1. **Greedy choice** — At each price, I ask "what's the best profit if I sell here?"
2. **Minimum tracking** — The best buy for any sell is always the lowest price before it
3. **Single pass** — No need to look ahead or store historical data
4. **Correctness guarantee** — The global maximum must occur at some (minPrice, price) pair
5. **Efficiency** — O(N) time, O(1) space — optimal for this problem

### **Example Walkthrough - Profitable Case**

```
Input: prices = [10,1,5,6,7,1]

Initialize:
  minPrice = 10
  maxPrice = 0

Iteration 1 (p=10):
  minPrice = min(10, 10) = 10
  profit = 10 - 10 = 0
  maxPrice = max(0, 0) = 0

Iteration 2 (p=1):
  minPrice = min(10, 1) = 1 ← Found cheaper buy!
  profit = 1 - 1 = 0
  maxPrice = max(0, 0) = 0

Iteration 3 (p=5):
  minPrice = min(1, 5) = 1 ← Keep tracking minimum
  profit = 5 - 1 = 4
  maxPrice = max(0, 4) = 4 ← New best profit

Iteration 4 (p=6):
  minPrice = min(1, 6) = 1
  profit = 6 - 1 = 5
  maxPrice = max(4, 5) = 5 ← Better profit

Iteration 5 (p=7):
  minPrice = min(1, 7) = 1
  profit = 7 - 1 = 6 ✓
  maxPrice = max(5, 6) = 6 ← Best profit!

Iteration 6 (p=1):
  minPrice = min(1, 1) = 1
  profit = 1 - 1 = 0
  maxPrice = max(6, 0) = 6 ← No change

Return: 6 ✓
(Buy at price 1, sell at price 7, profit = 6)
```

### **Example Walkthrough - No Profit Case**

```
Input: prices = [10,8,7,5,2]

Initialize:
  minPrice = 10
  maxPrice = 0

Iteration 1 (p=10):
  minPrice = min(10, 10) = 10
  profit = 10 - 10 = 0
  maxPrice = max(0, 0) = 0

Iteration 2 (p=8):
  minPrice = min(10, 8) = 8 ← New minimum
  profit = 8 - 8 = 0
  maxPrice = max(0, 0) = 0

Iteration 3 (p=7):
  minPrice = min(8, 7) = 7 ← New minimum
  profit = 7 - 7 = 0
  maxPrice = max(0, 0) = 0

Iteration 4 (p=5):
  minPrice = min(7, 5) = 5 ← New minimum
  profit = 5 - 5 = 0
  maxPrice = max(0, 0) = 0

Iteration 5 (p=2):
  minPrice = min(5, 2) = 2 ← New minimum
  profit = 2 - 2 = 0
  maxPrice = max(0, 0) = 0

Return: 0 ✓
(Prices strictly decreasing, no profitable trade possible)
```

### **Complexity Summary**

- **Time:** O(N)
  - Single loop through prices array
  - Each iteration: min/max comparison O(1), subtraction O(1)
  - Total: O(N)

- **Space:** O(1)
  - Only two variables (minPrice, maxPrice)
  - Constant memory regardless of input size

### **Why Not Brute Force?**

Brute force would check all possible (buy, sell) pairs:
```csharp
// O(N²) approach - slower
int maxProfit = 0;
for (int i = 0; i < prices.Length; i++) {
    for (int j = i + 1; j < prices.Length; j++) {
        maxProfit = Math.Max(maxProfit, prices[j] - prices[i]);
    }
}
return maxProfit;
```

My greedy approach is better because:
- **Faster:** O(N) vs O(N²)
- **Simpler:** Single loop instead of nested loops
- **Intuitive:** "What's the best buy before each sell?" is the right way to think about it
