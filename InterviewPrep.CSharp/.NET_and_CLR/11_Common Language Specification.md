Common Language Specification
Definition

CLS (Common Language Specification) is a set of rules that .NET languages and publicly exposed libraries can follow so that they can interoperate with other .NET languages.

Simple definition:

CTS defines what types .NET understands; CLS defines a common subset of rules that languages agree to support.

6. CTS vs CLS — Most Important Difference

Remember:

CTS = Complete type system
CLS = Common interoperability rules

Or:

CTS is broader; CLS is a subset of CTS.

Conceptually:

             CTS
 ┌─────────────────────────────┐
 │                             │
 │       CLS-compatible        │
 │       subset                │
 │                             │
 └─────────────────────────────┘

Not every CTS feature is necessarily CLS-compliant.

7. CLS Example

Consider:

public class Calculator
{
    public uint Add(uint a, uint b)
    {
        return a + b;
    }
}

uint is a valid CTS type.

But if you want your public API to be broadly consumable across CLS-compliant .NET languages, unsigned types can create interoperability issues.

You can mark an assembly as CLS-compliant:

[assembly: CLSCompliant(true)]

The compiler can then warn about publicly exposed constructs that violate CLS rules.

For example:

[assembly: CLSCompliant(true)]

public class Calculator
{
    public uint Add(uint a, uint b)
    {
        return a + b;
    }
}

The compiler can report a CLS-compliance warning for the public API.

8. CLS Is Especially Important for Public APIs

This is the key practical point.

You don't need to make every private implementation detail CLS-compliant.

CLS matters particularly when designing:

Public classes
Public methods
Public properties
Public parameters
Public return types
Public libraries

Example:

public class PaymentService
{
    public int Process(int amount)
    {
        return amount;
    }
}

This is broadly CLS-friendly.

But exposing language-specific or non-CLS-compatible constructs in public APIs can make cross-language consumption harder.

9. CTS vs CLS Example

Suppose CTS supports:

uint
ulong
ushort

These are valid .NET types.

But CLS intentionally defines a smaller common set for language interoperability.

Therefore:

CTS
 ├── int
 ├── uint
 ├── long
 ├── ulong
 ├── ...
 │
 └── many other types

CLS
 ├── int
 ├── long
 ├── string
 ├── bool
 ├── ...
 └── common interoperable subset