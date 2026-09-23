# Maximum Consecutive Ones

## Problem

Given a binary array containing only `0` and `1`, find the maximum number of consecutive `1`s.

Return the maximum count.

---

## Example

### Input

```text
[1,1,0,1,1,1]
```

### Output

```text
3
```

The longest consecutive sequence of `1`s is:

```text
[1,1,1]
```

Therefore:

```text
Answer = 3
```

---

# Idea

Maintain two variables:

```text
currentCount
maxCount
```

### currentCount

Stores the number of consecutive `1`s in the current sequence.

### maxCount

Stores the largest consecutive sequence found so far.

---

# Algorithm

For every element:

### If the element is `1`

Increase the current streak:

```text
currentCount++
```

Then update the maximum:

```text
maxCount = max(maxCount, currentCount)
```

### If the element is `0`

The consecutive sequence is broken.

Reset:

```text
currentCount = 0
```

---

# Dry Run

Input:

```text
[1,1,0,1,1,1]
```

Initial:

```text
currentCount = 0
maxCount = 0
```

### Element = 1

```text
currentCount = 1
maxCount = 1
```

### Element = 1

```text
currentCount = 2
maxCount = 2
```

### Element = 0

The sequence is broken:

```text
currentCount = 0
maxCount = 2
```

### Element = 1

```text
currentCount = 1
maxCount = 2
```

### Element = 1

```text
currentCount = 2
maxCount = 2
```

### Element = 1

```text
currentCount = 3
maxCount = 3
```

Final answer:

```text
3
```

---

# Why We Need maxCount

A common mistake is to return `currentCount`.

Consider:

```text
[1,1,0,1,1,1]
```

The first sequence has length `2`.

The second sequence has length `3`.

If we only keep the current count, the answer can be lost whenever a `0` appears.

Therefore:

```text
currentCount → current sequence
maxCount     → best sequence found so far
```

Both are required.

---

# Time Complexity

```text
O(n)
```

The array is traversed exactly once.

For an array of `n` elements:

```text
n elements
↓
n iterations
```

Therefore the time complexity is `O(n)`.

---

# Space Complexity

```text
O(1)
```

Only two integer variables are used:

```text
currentCount
maxCount
```

No additional data structure is required.

---

# Edge Cases

### 1. Null array

```text
nums = null
```

Throw:

```text
ArgumentNullException
```

---

### 2. Empty array

```text
nums = []
```

Throw:

```text
ArgumentException
```

---

### 3. Single element = 1

```text
[1]
```

Output:

```text
1
```

---

### 4. Single element = 0

```text
[0]
```

Output:

```text
0
```

---

### 5. All ones

```text
[1,1,1,1]
```

Output:

```text
4
```

---

### 6. All zeros

```text
[0,0,0,0]
```

Output:

```text
0
```

---

### 7. Multiple sequences

```text
[1,1,0,1,1,1,0,1]
```

The sequences have lengths:

```text
2
3
1
```

Maximum:

```text
3
```

---

# Key DSA Pattern

**Counting / Running Streak**

This pattern is useful when a problem asks for:

* Longest consecutive elements
* Maximum consecutive `1`s
* Longest sequence satisfying a condition
* Current streak vs maximum streak
* Counting contiguous runs
