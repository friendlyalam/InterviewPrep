17. What You Actually Need to Remember

For product-company interviews, don't memorize 30 ArrayList APIs.

Remember this:

ArrayList
   ↓
Non-generic
   ↓
System.Collections
   ↓
Stores object
   ↓
Can contain mixed types
   ↓
Boxing/unboxing
   ↓
Casting
   ↓
Not type-safe
   ↓
Legacy
   ↓
Replace with List<T>


⭐ One-line interview answer

ArrayList is a legacy non-generic dynamic collection that stores elements as object;
because it lacks type safety and can cause boxing/unboxing and casting overhead, modern C# code generally uses List<T> instead.