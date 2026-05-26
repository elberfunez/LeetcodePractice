# Encode and Decode Strings

**LeetCode Problem #271**  
**Difficulty:** Medium  
**Topics:** Array, String, Design

## Description

Design an algorithm to encode a list of strings to a string. The encoded string is then sent over the network and is decoded back to the original list of strings.

Machine 1 (sender) has the function:

```csharp
public string Encode(IList<string> strs)
{
    // ... your code
    return encoded_string;
}
```

Machine 2 (receiver) has the function:

```csharp
public List<string> Decode(string s)
{
    // ... your code
    return decoded_strs;
}
```

So Machine 1 does:
```csharp
string encoded_string = encode(strs);
```

and Machine 2 does:
```csharp
List<string> decoded_strs = decode(encoded_string);
```

`decoded_strs` in Machine 2 should be the same as the input `strs` in Machine 1.

## Examples

**Example 1:**
```
Input: strs = ["Hello","World"]
Output: ["Hello","World"]

Explanation:
Solution solution = new Solution();
string encoded_string = solution.Encode(strs);

// Machine 1 ---encoded_string---> Machine 2

List<string> decoded_strs = solution.Decode(encoded_string);
```

**Example 2:**
```
Input: strs = [""]
Output: [""]
```

## Constraints

- `0 <= strs.length < 100`
- `0 <= strs[i].length < 200`
- `strs[i]` contains any possible characters out of 256 valid ASCII characters.

## Follow-up

Could you write a generalized algorithm to work on any possible set of characters?

## Key Considerations

- The encoded string must preserve the exact strings when decoded
- Edge cases: empty strings, strings with special characters, empty list
- The encoding scheme must be unambiguous and reversible
- Need to handle strings that might contain the delimiter or special characters
