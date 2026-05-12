# Notes - Valid Parentheses

## Pattern / Approach

Use a **Stack** with a **mapping dictionary**. Push opening brackets onto the stack, and when you encounter closing brackets, check if they match the most recent opening bracket at the top of the stack.

## Data Structure(s) Used

- **Stack<char>** — stores opening brackets as we encounter them
- **Dictionary<char, char>** — maps closing brackets to their corresponding opening brackets

## Thought Process

Brackets must match in **LIFO (Last In First Out)** order — the last bracket opened must be the first one closed. This is exactly what a stack does!

**Key insight:** The most recent unmatched opening bracket is at the top of the stack, and the next closing bracket should match it.

**Algorithm:**
1. **Quick validation:** Odd-length strings can't have matching pairs, return false immediately
2. Create a dictionary mapping closing brackets to opening brackets (e.g., `')' → '('`)
3. For each character:
   - If it's a **closing bracket** (in dictionary) → pop from stack and check if it matches
   - If it's an **opening bracket** → push to stack
4. At the end, stack must be empty (all brackets matched)

```
Example: "([{}])"

'(' → opening → push → Stack: [()]

'[' → opening → push → Stack: [(,[]

'{' → opening → push → Stack: [(,[,{]

'}' → closing → pop '{', matches ✓ → Stack: [(,[]

']' → closing → pop '[', matches ✓ → Stack: [()]

')' → closing → pop '(', matches ✓ → Stack: []

Final: Stack empty ✓ → valid
```

## Complexity

**Time:** O(N) — Single pass through string, each character processed once  
**Space:** O(N) — Stack can hold up to N/2 opening brackets in worst case

## Edge Cases

- **Odd length** (`"("`, `"(])"`) — Length is odd, return false immediately
- **Only closing brackets** (`"])]"`): Stack will be empty when trying to pop, return false
- **Only opening brackets** (`"([{"`): Stack not empty at end, return false
- **Mismatched types** (`"{]"`): Closing bracket doesn't match popped opening bracket
- **Wrong order** (`"([)]"`): Even though each type matches, closing order violates nesting

## Key Insight

**TryGetValue + Pop in the condition = cleaner code!**

Instead of:
```csharp
if (rules.ContainsKey(c)) {
    if (stack.Count == 0) return false;
    if (stack.Peek() == rules[c]) {
        stack.Pop();
    }
}
```

Use one elegant line:
```csharp
if (pairs.TryGetValue(c, out char opening)) {
    if (stack.Count == 0 || stack.Pop() != opening) {
        return false;
    }
}
```

Benefits:
- **TryGetValue** is O(1) and returns both the key existence AND the value
- **Pop in the condition** eliminates the separate Peek() call
- **Combined check** with `||` short-circuits early if stack is empty

## Pattern Recognition for Similar Problems

🚩 **Red flags for STACK problems:**
1. **"Valid parentheses/brackets"** → STACK ⭐⭐⭐
2. **"Matching pairs in order"** → STACK ⭐⭐
3. **"Remove/delete invalid characters"** → STACK
4. **"Nesting or LIFO behavior"** → STACK
5. **"Most recent X"** or **"undo"** operations → STACK

**Similar Problems:** "Min Stack", "Remove Duplicate Letters", "Valid Parentheses II", "Remove Outer Parentheses"

---

## My Solution Approach

### **Step 1: Early Termination - Even Length Check**
```csharp
if (s.Length % 2 != 0) return false; // must be even length
```

Optimization: Odd-length strings can never have matching pairs. Return immediately without processing.

### **Step 2: Create the Mapping Dictionary**
```csharp
var pairs = new Dictionary<char, char> {
    { ')', '(' },
    { ']', '[' },
    { '}', '{' }
};
```

Maps each **closing bracket** to its **matching opening bracket**. Using closing brackets as keys lets me easily identify them with `TryGetValue`.

### **Step 3: Initialize the Stack**
```csharp
var stack = new Stack<char>();
```

Stores opening brackets. When closing brackets arrive, I pop and check matches.

### **Step 4: Process Each Character with TryGetValue**
```csharp
foreach (char c in s) {
    if (pairs.TryGetValue(c, out char opening)) {
        // This is a closing bracket
        if (stack.Count == 0 || stack.Pop() != opening) {
            return false;
        }
    } else {
        // This is an opening bracket
        stack.Push(c);
    }
}
```

**TryGetValue advantage:**
- Returns `true` if `c` is a key in the dictionary (closing bracket)
- Outputs the **value** (matching opening bracket) in the `out` parameter
- Cleaner than `ContainsKey(c)` + `c[pairs]` (two lookups)

**The logic:**
- **Closing bracket found** → Check if it matches the top of stack (pop and compare in one line)
- **Opening bracket** → Push to stack, wait for its matching close

### **Step 5: Final Validation**
```csharp
return stack.Count == 0;
```

All opening brackets must be matched and popped. If the stack still has brackets, there were unmatched openings → invalid.

### **Why This Works**

1. **Even length check** — Fast-path for obviously invalid strings
2. **TryGetValue** — Efficient single dictionary lookup that identifies AND extracts
3. **Pop in condition** — `stack.Pop() != opening` combines retrieval, matching, and removal
4. **Short-circuit with ||** — `stack.Count == 0 || stack.Pop() != opening` returns false without popping if stack is empty
5. **Stack LIFO** — Enforces correct nesting order
6. **Final check** — Catches any unmatched opening brackets

### **Example Walkthrough - Valid Case**

```
Input: "([{}])"
Length: 6 (even) ✓

pairs = { ')':'(', ']':'[', '}':'{' }
stack = []

'(' → !pairs.TryGetValue('(') → opening → push → Stack: [()]

'[' → !pairs.TryGetValue('[') → opening → push → Stack: [(,[)]

'{' → !pairs.TryGetValue('{') → opening → push → Stack: [(,[,{]

'}' → pairs.TryGetValue('}', out '(') = true
    → stack.Count = 3 > 0, stack.Pop() = '{', '{' == '{' ✓
    → Stack: [(,[]

']' → pairs.TryGetValue(']', out '[') = true
    → stack.Count = 2 > 0, stack.Pop() = '[', '[' == '[' ✓
    → Stack: [()]

')' → pairs.TryGetValue(')', out '(') = true
    → stack.Count = 1 > 0, stack.Pop() = '(', '(' == '(' ✓
    → Stack: []

Return: stack.Count == 0 → true ✓ VALID
```

### **Example Walkthrough - Invalid Cases**

**Case 1: Odd length**
```
Input: "("
Length: 1 (odd) → return false immediately ✓
```

**Case 2: Closing without opening**
```
Input: "]]"
Length: 2 (even) ✓

']' → pairs.TryGetValue(']') = true
    → stack.Count = 0 → return false ✓ (short-circuit on first condition)
```

**Case 3: Wrong order**
```
Input: "[()"
Length: 3 (odd) → return false immediately ✓
```

**Case 4: Mismatched closing**
```
Input: "(]"
Length: 2 (even) ✓

'(' → opening → push → Stack: [()]

']' → pairs.TryGetValue(']', out '[') = true
    → stack.Count = 1 > 0, stack.Pop() = '('
    → '[' != '(' → return false ✓
```

### **Complexity Summary**
- **Time:** O(N) — Each character processed once
- **Space:** O(N) — Stack size up to N/2 opening brackets in worst case
