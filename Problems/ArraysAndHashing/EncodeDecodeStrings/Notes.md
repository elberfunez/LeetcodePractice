# Notes - Encode and Decode Strings

## Pattern / Approach

Use **length-prefix encoding** with a delimiter. Instead of using a delimiter to separate strings (which fails if the string contains that delimiter), encode the **length of each string before the string itself**. During decoding, read the length first, then extract exactly that many characters.

## Data Structure(s) Used

- **StringBuilder** — For efficient string concatenation during encoding (avoids O(n²) string copying)
- **List<string>** — For storing decoded strings
- **string.IndexOf() and string.Substring()** — For parsing the encoded string during decoding

## Thought Process

**Key insight:** The core problem with delimiter-based encoding is that delimiters can appear in the strings themselves. By prefixing each string with its length, we eliminate ambiguity—we know exactly how many characters to consume, regardless of their content.

**Why this works:**
- Delimiter becomes a "length marker" not a true delimiter
- Length tells us precisely where one string ends and the next begins
- Works with ALL 256 ASCII characters, including special ones

**Algorithm:**
1. **Encode:** For each string, append `{length}#{string}` to the result
2. **Decode:** Parse through the encoded string:
   - Find the first `#`
   - Parse the number before it (that's the length)
   - Extract exactly that many characters
   - Move to the next string's length prefix

## Complexity

**Time:**
- **Encode:** O(N) where N = total characters across all strings (single pass)
- **Decode:** O(N) where N = length of encoded string (single pass with substring operations)

**Space:**
- **Encode:** O(N) for the encoded result string
- **Decode:** O(N) for storing all decoded strings

## Edge Cases

- **Empty list:** Encode to empty string, decode empty string back to empty list
- **Single empty string:** `[""]` → `"0#"` → `[""]` (length 0 with delimiter)
- **String with delimiter character:** `["a#b"]` → `"3#a#b"` (the # inside the string is just data, not parsed as delimiter)
- **String with any ASCII character:** All 256 characters preserved by length prefix
- **Multiple empty strings:** `["", ""]` → `"0#0#"` → `["", ""]`
- **Very long string:** Up to 199 characters, handled fine by length parsing

## Key Insight

**Length-prefix encoding is the fundamental approach for encoding variable-length strings over a wire!**

This is used in real protocols:
- Network packets often have a `Length` field before the payload
- HTTP Content-Length header
- TLV (Tag-Length-Value) encoding in many protocols

The elegance: By knowing the length upfront, you eliminate all ambiguity. The delimiter `#` becomes metadata, not a string delimiter.

## Pattern Recognition for Similar Problems

🚩 **Red flags for ENCODING/SERIALIZATION problems:**
1. **"Encode and decode"** → LENGTH-PREFIX ⭐⭐⭐
2. **"Serialize/deserialize"** → Consider length-prefix or TLV format
3. **"Variable-length items"** → LENGTH-PREFIX avoids delimiter conflicts
4. **"Network protocol"** → Often uses length fields
5. **"Multiple strings with special characters"** → LENGTH-PREFIX

**Similar Problems:** Serialize/Deserialize BST, Serialize/Deserialize N-ary Tree, LRU Cache (serialization), any encoding problem

---

## My Solution Approach

### **Step 1: Initialize StringBuilder for Efficient Concatenation**
```csharp
StringBuilder sb = new();
```

StringBuilder avoids the O(n²) problem of string concatenation. Each time you concatenate a string in C#, it creates a new string object, copying all previous content. With many strings, this becomes O(n²). StringBuilder builds the result efficiently in O(n).

### **Step 2: Encode - Iterate Through Strings**
```csharp
foreach (string s in strs)
{
    sb.Append($"{s.Length}#{s}");
}
```

For each string:
1. **Get the length** — `s.Length`
2. **Add delimiter** — `#` (marks the separation between length and content)
3. **Add the string** — `s` (the actual content)

Example: "Hello" becomes `"5#Hello"`, "World" becomes `"5#World"`
Combined: `"5#Hello5#World"`

### **Step 3: Encode - Return Result**
```csharp
return sb.ToString();
```

Convert StringBuilder to string. Total time: O(N) where N = total characters.

### **Step 4: Decode - Initialize Result and Pointer**
```csharp
List<string> res = new();
int i = 0;
```

- `res` — Stores the decoded strings
- `i` — Current position in the encoded string (like a cursor)

### **Step 5: Decode - Parse Each String**
```csharp
while (i < s.Length)
{
    int delim = s.IndexOf("#", i);
    int len = int.Parse(s.Substring(i, delim - i));
    i = delim + 1;
    res.Add(s.Substring(i, len));
    i += len;
}
```

**Breaking it down:**

**5a. Find the length marker:**
```csharp
int delim = s.IndexOf("#", i);
```
Search for `#` starting from position `i`. This gives us the position of the delimiter.

**5b. Parse the length:**
```csharp
int len = int.Parse(s.Substring(i, delim - i));
```
- `s.Substring(i, delim - i)` — Extract from position `i` up to (but not including) the `#`
- Example: if `i=0` and `delim=1`, we get `"5"` from `"5#Hello5#World"`
- `int.Parse()` — Convert `"5"` to integer `5`

**5c. Move past the delimiter:**
```csharp
i = delim + 1;
```
Position `i` now points to the first character of the actual string content.

**5d. Extract the string:**
```csharp
res.Add(s.Substring(i, len));
```
Extract exactly `len` characters starting from position `i`.
- Example: `s.Substring(1, 5)` from `"5#Hello5#World"` gives `"Hello"`

**5e. Move to the next string's length:**
```csharp
i += len;
```
Advance by the number of characters we just extracted. Now `i` points to the start of the next length field.

### **Step 6: Decode - Return Result**
```csharp
return res;
```

### **Why This Works**

1. **No ambiguity** — Length tells us exactly how many characters to consume
2. **Handles all characters** — Even if the string contains `#`, it's just data because we consume by length, not by delimiter
3. **Single pass** — Both encode and decode are O(N)
4. **Reversible** — Encoding and decoding are inverse operations

### **Example Walkthrough - Encode**

```
Input: ["Hello", "World"]

Process "Hello":
  length = 5
  append "5#Hello"
  
Process "World":
  length = 5
  append "5#World"
  
Result: "5#Hello5#World"
```

### **Example Walkthrough - Decode**

```
Input: "5#Hello5#World"

i = 0
Loop 1:
  delim = IndexOf("#", 0) = 1
  len = Parse("5") = 5
  i = 1 + 1 = 2
  res.Add(Substring(2, 5)) = "Hello"
  i = 2 + 5 = 7
  
i = 7
Loop 2:
  delim = IndexOf("#", 7) = 8
  len = Parse("5") = 5
  i = 8 + 1 = 9
  res.Add(Substring(9, 5)) = "World"
  i = 9 + 5 = 14
  
i = 14, which equals s.Length, so loop exits

Result: ["Hello", "World"]
```

### **Example Walkthrough - Edge Case with Special Characters**

```
Input: ["a#b", "c"]

Encode:
  "a#b" → length=3 → "3#a#b"
  "c" → length=1 → "1#c"
  Result: "3#a#c1#c"

Decode:
  i=0: delim=1, len=3, extract 3 chars → "a#b" ✓
  i=4: delim=5, len=1, extract 1 char → "c" ✓
  Result: ["a#b", "c"]

Note: The "#" inside "a#b" is just data. We ignore it because we consume by LENGTH, not by delimiter.
```

### **Example Walkthrough - Empty String**

```
Input: [""]

Encode:
  "" → length=0 → "0#"
  Result: "0#"

Decode:
  i=0: delim=1, len=0, extract 0 chars → "" ✓
  i=2: i >= s.Length, exit
  Result: [""]
```

### **Complexity Summary**
- **Time:** O(N) for both encode and decode, where N = total characters
- **Space:** O(N) for the encoded/decoded result

### **Why StringBuilder?**

Without StringBuilder:
```csharp
string result = "";
foreach (string s in strs)
{
    result = result + s.Length + "#" + s;  // Creates new string each time!
}
```
This is O(n²) because each concatenation copies all previous content.

With StringBuilder:
```csharp
StringBuilder sb = new();
foreach (string s in strs)
{
    sb.Append($"{s.Length}#{s}");  // Appends without copying
}
return sb.ToString();
```
This is O(n) because we build incrementally.

### **Key Takeaway**

Length-prefix encoding is **superior to delimiter-based encoding** when:
- Strings can contain any character
- You need to handle special characters reliably
- You're building a protocol or serialization format

Real-world use: HTTP responses use `Content-Length`, binary protocols use TLV (Tag-Length-Value), and network packets have length fields—all using this same principle!
