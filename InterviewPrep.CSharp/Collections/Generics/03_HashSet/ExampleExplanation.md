44. Program Explanation

The most important part is:

bool added = numbers.Add(50);

If 50 wasn't already present:

true

Then:

numbers.Add(30);

returns:

false

because 30 already exists.

This is why HashSet<T>.Add() is so useful for duplicate detection.

Set operations

Given:

A = 1 2 3 4
B = 3 4 5 6
Union
1 2 3 4 5 6
Intersection
3 4
Difference A - B
1 2
Symmetric difference
1 2 5 6

These four operations are worth knowing extremely well for DSA.

45. The DSA Pattern You Should Memorize

For:

"Find whether an array contains a duplicate."

Use:

HashSet<int> seen = new();

foreach (int number in numbers)
{
    if (!seen.Add(number))
    {
        return true;
    }
}

return false;

Mental model:

                number
                   │
                   ▼
             seen.Add()
                /    \
               /      \
          true          false
           │              │
        New value       Duplicate

This pattern is much more important for your interviews than memorizing every HashSet<T> method.

46. Final Cheat Sheet
HashSet<T>
│
├── Generic Collection ✅
├── Namespace: System.Collections.Generic
├── Hash-based
├── Unique elements
│
├── Add()
├── Contains()
├── Remove()
├── RemoveWhere()
├── Clear()
├── Count
│
├── UnionWith()
├── IntersectWith()
├── ExceptWith()
├── SymmetricExceptWith()
│
├── IsSubsetOf()
├── IsSupersetOf()
├── IsProperSubsetOf()
├── IsProperSupersetOf()
├── Overlaps()
├── SetEquals()
│
├── CopyTo()
├── ToArray()
├── EnsureCapacity()
└── TrimExcess()
⭐ The three collections we've now covered
List<T>
    ↓
Ordered, index-based, duplicates allowed

Dictionary<TKey,TValue>
    ↓
Key → Value mapping

HashSet<T>
    ↓
Unique values + fast membership

And the DSA mental model:

List
→ "I need a sequence."

Dictionary
→ "I need to map one thing to another."

HashSet
→ "I need to know whether I've already seen this."

That distinction will be extremely useful when we start solving DSA problems with C# collections.