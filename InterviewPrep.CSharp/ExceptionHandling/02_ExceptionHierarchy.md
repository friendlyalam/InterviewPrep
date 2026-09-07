Exception Hierarchy

This is important for interviews.

System.Object
    |
System.Exception
    |
    +-- System.SystemException
    |      |
    |      +-- NullReferenceException
    |      +-- ArgumentException
    |      |      |
    |      |      +-- ArgumentNullException
    |      |      +-- ArgumentOutOfRangeException
    |      |
    |      +-- InvalidOperationException
    |      +-- IndexOutOfRangeException
    |      +-- DivideByZeroException
    |      +-- FormatException
    |      +-- IOException
    |
    +-- ApplicationException
    |
    +-- Custom Exceptions