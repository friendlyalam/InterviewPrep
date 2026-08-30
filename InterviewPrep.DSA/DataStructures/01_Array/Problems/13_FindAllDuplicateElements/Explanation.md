# Finding Duplicate Elements

An optimized approach using two hash sets to extract duplicate array elements in a single pass[cite: 1].

---

## 1. Approach

* **The Two-HashSet Technique:** Uses two hash sets (`seen` and `duplicates`) to process the array in a single traversal[cite: 1].
* **Single Pass:** As we iterate through each element, we attempt to add it to `seen`[cite: 1].
* **Duplicate Detection:** `seen.Add(x)` returns `false` if $x$ is already present[cite: 1]. When this happens, we insert $x$ into `duplicates`[cite: 1].
* **Automatic De-duplication:** Using a hash set for `duplicates` ensures that 3rd, 4th, or subsequent occurrences of a number are ignored automatically[cite: 1].

---

## 2. Dry Run

Input: `numbers = [4, 3, 2, 2, 3, 3]`[cite: 1]

| Step | Number | `seen.Add()` | Action | `seen` State | `duplicates` State |
| :---: | :---: | :---: | :--- | :--- | :--- |
| **1** | `4` | `true` | Insert into `seen` | `{4}` | `{}` |
| **2** | `3` | `true` | Insert into `seen` | `{4, 3}` | `{}` |
| **3** | `2` | `true` | Insert into `seen` | `{4, 3, 2}` | `{}` |
| **4** | `2` | `false` | Duplicate found! Insert into `duplicates` | `{4, 3, 2}` | `{2}` |
| **5** | `3` | `false` | Duplicate found! Insert into `duplicates` | `{4, 3, 2}` | `{2, 3}` |
| **6** | `3` | `false` | Already in `duplicates` (ignored) | `{4, 3, 2}` | `{2, 3}` |

**Output:** `[2, 3]`[cite: 1]

---

## 3. Complexity Analysis

* **Time Complexity:** $O(N)$ — Iterates through the input array of size $N$ once[cite: 1]. Hash set additions and lookups take $O(1)$ amortized average time[cite: 1].
* **Space Complexity:** $O(N)$ — In the worst-case scenario, storing elements across `seen` and `duplicates` requires $O(N)$ auxiliary space[cite: 1].

---

## 4. Edge Cases

* **`null` input:** Throws an `ArgumentNullException`[cite: 1].
* **Empty array (`[]`):** Throws an `ArgumentException`[cite: 1].
* **Single item / No duplicates (`[5]`, `[1, 2, 3]`):** Returns an empty array `[]`[cite: 1].
* **All identical (`[7, 7, 7]`):** Ignores 3rd+ occurrences and returns `[7]`[cite: 1].
* **Negative numbers & zeros (`[-1, -1, 0, 0]`):** Correctly handles hash values for negative integers and returns `[-1, 0]`[cite: 1].