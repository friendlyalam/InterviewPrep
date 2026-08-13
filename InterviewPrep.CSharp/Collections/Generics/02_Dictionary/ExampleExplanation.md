52. Program Explanation
 1. 
Creating the dictionary
Dictionary<int, string> employees = new()

means:

Key   = int
Value = string

So:

101 → Ali

is valid.

Initial data
{
    { 101, "Ali" },
    { 102, "Ahmed" },
    { 103, "John" }
};

creates three entries.

Therefore:

Count = 3
Add()
employees.Add(104, "David");

adds:

104 → David
TryAdd()
employees.TryAdd(105, "Sara");

returns:

true

because 105 doesn't exist.

Then:

employees.TryAdd(101, "New Ali");

returns:

false

because 101 already exists.

The existing value remains unchanged.

Indexer
employees[101]

retrieves the value.

Then:

employees[101] = "Mohammad Ali";

updates it.

ContainsKey()
employees.ContainsKey(102)

checks whether employee ID 102 exists.

ContainsValue()
employees.ContainsValue("Ahmed")

searches values.

Remember:

ContainsKey   → average O(1)
ContainsValue → O(n)
TryGetValue()
employees.TryGetValue(103, out employeeName)

attempts to retrieve employee 103.

This is preferable to:

ContainsKey()
+
dictionary[key]

when you need both existence checking and the value.

GetValueOrDefault()
employees.GetValueOrDefault(102)

returns the value if present, otherwise the default value.

Use TryGetValue() when you need to know explicitly whether the key exists.

Keys
employees.Keys

iterates through keys.

Values
employees.Values

iterates through values.

KeyValuePair
foreach (KeyValuePair<int, string> employee in employees)

lets us access both:

employee.Key
employee.Value
Remove()
employees.Remove(104);

removes the employee with ID 104.

It returns:

true

if the entry existed and was removed.

Remove(key, out value)
employees.Remove(
    105,
    out string? removedEmployee)

does two things:

1. Removes key 105
2. Gives us its value
EnsureCapacity()
employees.EnsureCapacity(100);

ensures the dictionary has enough internal capacity for approximately that scale of entries.

It does not add 100 employees.

Clear()
employees.Clear();

removes all entries.

Final:

Count = 0
53. The Most Important Things to Remember

For your notes, remember this mental model:

                 Dictionary<TKey,TValue>
                          │
                          ▼
                       Key
                          │
                          ▼
                    Hash function
                          │
                          ▼
                      Hash code
                          │
                          ▼
                       Bucket  (A bucket is essentially a location used by the hash table to organize entries.)
                          │
                          ▼
                       Entry
                          │
                          ▼
                        Value

And the interview summary:

Dictionary
│
├── Generic collection ✅
├── Key → Value
├── Keys must be unique
├── Values can duplicate
├── Hash-table based
│
├── Add()          → duplicate key = exception
├── TryAdd()       → duplicate key = false
├── [key]          → get/update/add
├── TryGetValue()  → safe lookup
├── ContainsKey()  → average O(1)
├── ContainsValue()→ O(n)
├── Remove()
├── Clear()
├── Count
├── Keys
└── Values

Average lookup → O(1)
Worst case      → O(n)
⭐ DSA priority

For your DSA preparation, Dictionary<TKey,TValue> is more important than memorizing every C# method. The real goal is to understand:

hashing → hash code → bucket → collision → equality → average O(1) lookup → frequency map.

That knowledge directly transfers to coding-interview problems.