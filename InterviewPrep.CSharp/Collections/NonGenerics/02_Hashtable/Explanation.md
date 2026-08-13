The one thing to remember
Hashtable
    ↓
Non-generic
    ↓
Hash-based
    ↓
object/object
    ↓
Average O(1) lookup
    ↓
Legacy
    ↓
Dictionary<TKey,TValue> is the modern choice
⭐ Interview definition

Hashtable is a legacy non-generic hash-based key-value collection that provides average O(1)
lookup but lacks compile-time type safety and may require boxing, unboxing, and casting; modern C# generally uses Dictionary<TKey,TValue> instead.