Product Company Definition

Method overloading means creating multiple methods with the same name but different parameter lists within the same class. 
The compiler determines which method to call based on the arguments.
its also called compile-time polymorphism or static polymorphism because the method to be executed is determined at compile time.

Memory Behaviour

Compiler

↓

Chooses method

↓

Program Runs

-------------------------------------------------------------------------------------------------------
| Feature                   | Method Overloading | Method Overriding | Method Hiding | Operator Overloading |
| ------------------------- | ------------------ | ----------------- | ------------- | -------------------- |
| Same Method Name          | ✅                  | ✅                 | ✅             | N/A                  |
| Same Parameters           | ❌                  | ✅                 | Usually ✅     | N/A                  |
| Inheritance Required      | ❌                  | ✅                 | ✅             | ❌                    |
| Runtime Polymorphism      | ❌                  | ✅                 | ❌             | ❌                    |
| Compile-Time Decision     | ✅                  | ❌                 | ✅             | ✅                    |
| Uses `virtual`/`override` | ❌                  | ✅                 | ❌             | ❌                    |
| Uses `new`                | ❌                  | ❌                 | ✅             | ❌                    |


------------------------------------------------------------------------------------------------------------------------------
Use Method Overloading

When the same operation accepts different inputs.

Examples:

Search
Upload
Login
Export

-------------------------
1. Can overloaded methods have different return types only?

❌ No.

This is invalid:

int Calculate(int x)

decimal Calculate(int x)

The parameter list must differ.


-------------------------------------
| Concept              | Best Use Case                                   |
| -------------------- | ----------------------------------------------- |
| Method Overloading   | Same operation, different parameters            |
| Method Overriding    | Different implementations chosen at runtime     |
| Method Hiding        | Rare legacy or compatibility scenarios          |
| Operator Overloading | Natural behaviour for value-like domain objects |

--------------------------------------------------------------------------
Product-company recommendation

If an interviewer asks, "Which should we prefer?":

Prefer method overloading for API convenience.
Prefer method overriding for extensibility and runtime polymorphism.
Use method hiding sparingly and only with a clear reason.
Use operator overloading only when it makes the code more expressive without reducing clarity.

