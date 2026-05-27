# Notes - Add Two Numbers

## Pattern / Approach

This is **elementary school addition simulated with linked lists**. You add digit by digit from right to left, just like you learned in school. The only trick: these lists are already in reverse order, so you add them left to right instead!

## The Core Idea

Imagine adding two numbers by hand:
```
    321
  +  654
  ------
```

You start from the right (least significant digit):
- 1 + 4 = 5 (no carry)
- 2 + 5 = 7 (no carry)  
- 3 + 6 = 9 (no carry)
- Result: 975

Now in this problem, the lists are **already in this order**:
```
l1: 1 → 2 → 3  (represents 321)
l2: 4 → 5 → 6  (represents 654)
```

So you just **process left to right**, which naturally adds the least significant digits first!

## High-Level Algorithm

1. **Walk through both lists simultaneously** with two pointers
2. **At each step:** 
   - Get the current digit from each list (use 0 if list ended)
   - Add them together plus any carry from the previous step
   - Extract the ones digit to create a new node
   - Keep the tens digit as the carry for next step
3. **Keep going** until both lists are empty AND there's no carry left
4. **Return the result list** (built as we went)

## Why Reverse Order is Perfect

Normal numbers: **rightmost digit is least significant**  
These lists: **leftmost (head) is least significant**

This alignment is intentional—it makes addition straightforward without needing to reverse anything!

## Data Structure(s) Used

- **ListNode dummy** — A fake starting node that makes building the result easier
- **ListNode cur** — Points to the last node in result (we append to it)
- **int carry** — Tracks if there's a "1" to add to the next digit (0 or 1, or higher in edge cases)

## Complexity

**Time:** O(max(len(l1), len(l2)))
- Process each node once, linear iteration through both lists

**Space:** O(max(len(l1), len(l2)))
- Result list is at most max(len1, len2) + 1 nodes (for final carry)

## Edge Cases

- **Different lengths:** Handled by treating missing nodes as 0
- **Final carry:** Loop condition `carry > 0` ensures we create final digit node if needed
- **Both single digits:** Simple case, works perfectly
- **All zeros:** Returns [0], handled naturally

## Key Insight

**The dummy node pattern eliminates edge cases!** Without it, you'd need special handling for the first node. With it, every node is appended the same way—elegant and bug-free.

---

## My Solution Approach

### **Step 1: Create Dummy Node & Initialize**

```csharp
ListNode dummy = new ListNode();
ListNode cur = dummy;
int carry = 0;
```

**Why a dummy node?**
- Simplifies building the result list
- First real result node becomes `dummy.next`
- All nodes appended the same way: `cur.next = new ListNode(digit)`
- No special case for the first node

Think of the dummy as a "starter node" that gets discarded at the end.

### **Step 2: Loop While Work Exists**

```csharp
while (l1 != null || l2 != null || carry > 0)
```

Three conditions for continuing:
1. **`l1 != null`** — Left list still has digits
2. **`l2 != null`** — Right list still has digits
3. **`carry > 0`** — There's a carry to propagate (e.g., 9+9=18, leaves carry of 1)

This is why we need `carry > 0`: If both lists are exhausted but there's a final carry, we still need one more iteration to create the final digit node.

### **Step 3: Get Values with Null-Coalescing**

```csharp
int val1 = l1?.val ?? 0;
int val2 = l2?.val ?? 0;
```

**The null-coalescing operator (`??`):**
- `l1?.val` — Safe navigation: access `val` only if `l1` is not null, otherwise null
- `?? 0` — If left side is null, use 0
- Combined: "Get `l1.val` if `l1` exists, otherwise use 0"

This elegantly handles **different list lengths**. If one list runs out, treat its digits as 0.

Example:
```
l1 = [9, 9, 9]  (999)
l2 = [1]        (1)

When l2 is exhausted:
  val1 = 9 (from l1)
  val2 = 0 (l2 is null, so use 0)
```

### **Step 4: Calculate Sum, Digit, and New Carry**

```csharp
int sum = val1 + val2 + carry;
int digit = sum % 10;
carry = sum / 10;
```

This is standard arithmetic:
- **`sum`** — Total of current digits plus carry from previous iteration
- **`digit = sum % 10`** — Last digit of the sum (0-9)
  - Example: 15 % 10 = 5
- **`carry = sum / 10`** — The tens place (becomes carry for next iteration)
  - Example: 15 / 10 = 1 (integer division)

**Example:**
```
val1=9, val2=8, carry=0
  sum = 9 + 8 + 0 = 17
  digit = 17 % 10 = 7    (node value is 7)
  carry = 17 / 10 = 1    (carry to next iteration)
```

### **Step 5: Append New Node**

```csharp
cur.next = new ListNode(digit);
cur = cur.next;
```

Create a new node with the digit and move the cursor forward. Every node creation is the same—no special cases because of the dummy node.

### **Step 6: Advance Both Pointers**

```csharp
l1 = l1?.next;
l2 = l2?.next;
```

Again using null-coalescing logic:
- If `l1` is not null, move to `l1.next`
- If `l1` is null, it stays null (and `val1` will be 0 next iteration)

This allows the loop to gracefully handle lists of different lengths without crashing.

### **Step 7: Return Result**

```csharp
return dummy.next;
```

Skip the dummy node and return the actual first digit. The result list is built from `dummy.next` onward.

---

## Example Walkthrough - Basic

```
Input: l1 = [1,2,3] (represents 321), l2 = [4,5,6] (represents 654)

Initialize:
  dummy = ListNode(), cur = dummy
  carry = 0

Iteration 1:
  val1 = 1, val2 = 4
  sum = 1 + 4 + 0 = 5
  digit = 5, carry = 0
  cur.next = ListNode(5), cur = cur.next
  l1 = l1.next (→ 2), l2 = l2.next (→ 5)

Iteration 2:
  val1 = 2, val2 = 5
  sum = 2 + 5 + 0 = 7
  digit = 7, carry = 0
  cur.next = ListNode(7), cur = cur.next
  l1 = l1.next (→ 3), l2 = l2.next (→ 6)

Iteration 3:
  val1 = 3, val2 = 6
  sum = 3 + 6 + 0 = 9
  digit = 9, carry = 0
  cur.next = ListNode(9), cur = cur.next
  l1 = l1.next (→ null), l2 = l2.next (→ null)

Check loop condition:
  l1 == null, l2 == null, carry == 0
  Exit loop

Return dummy.next → [5,7,9] ✓
```

## Example Walkthrough - With Carry

```
Input: l1 = [9], l2 = [9]

Initialize:
  dummy = ListNode(), cur = dummy
  carry = 0

Iteration 1:
  val1 = 9, val2 = 9
  sum = 9 + 9 + 0 = 18
  digit = 18 % 10 = 8, carry = 18 / 10 = 1
  cur.next = ListNode(8), cur = cur.next
  l1 = null, l2 = null

Check loop condition:
  l1 == null ✓, l2 == null ✓, carry == 1 ✗ (still have carry!)
  Continue!

Iteration 2:
  val1 = 0 (l1 is null), val2 = 0 (l2 is null)
  sum = 0 + 0 + 1 = 1
  digit = 1 % 10 = 1, carry = 1 / 10 = 0
  cur.next = ListNode(1), cur = cur.next
  l1 = null (still null), l2 = null (still null)

Check loop condition:
  l1 == null ✓, l2 == null ✓, carry == 0 ✓
  Exit loop

Return dummy.next → [8,1] ✓ (represents 18)
```

## Example Walkthrough - Different Lengths

```
Input: l1 = [9,9,9,9,9,9,9] (9999999), l2 = [9,9,9,9] (9999)

The null-coalescing handles this perfectly:

When l2 is exhausted (becomes null):
  val1 = digit from l1
  val2 = 0 (because l2 is null)
  
Then sum = val1 + 0 + carry
And we continue normally, "adding" with 0s for missing digits

Final result: [8, 9, 9, 9, 0, 0, 0, 1]
Representing: 10009998 ✓
```

### **Why This Works**

1. **Dummy node** — Eliminates edge cases, all nodes created uniformly
2. **Null-coalescing** — Elegantly handles different list lengths
3. **Reverse order** — Natural for addition (process least-significant first)
4. **Carry loop condition** — `carry > 0` ensures final carry is not lost
5. **Pointer safety** — Both pointers safely become null and stay null

### **Key Takeaway**

The **dummy node pattern** is a game-changer for linked list problems! It transforms "building a list" from error-prone to elegant. Use it whenever you're:
- Merging lists
- Creating a new result list
- Inserting at the head
- Avoiding special-case handling for the first node

The dummy node is discarded but pays dividends in code clarity and correctness.
