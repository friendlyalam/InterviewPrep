Project Structure
01_SingletonPattern
│
├── Models
│      ApplicationConfiguration.cs
│
├── Consumers
│      UserService.cs
│      OrderService.cs
│      PaymentService.cs
│
├── SingletonImplementations
│      ├── 01_EagerSingleton
│      │      ConfigurationManager.cs
│      │
│      ├── 02_LazySingleton
│      │      ConfigurationManager.cs
│      │
│      ├── 03_ThreadSafeSingleton
│      │      ConfigurationManager.cs
│      │
│      ├── 04_DoubleCheckedLockingSingleton
│      │      ConfigurationManager.cs
│      │
│      └── 05_LazyTSingleton
│             ConfigurationManager.cs
│
└── Program.cs
Why this structure?

Notice something.

The business problem never changes.

Configuration Management System

Only the implementation changes.

Configuration Management System

        ↓

Eager Singleton

Lazy Singleton

Thread Safe Singleton

Double Checked Locking

Lazy<T>

Exactly like product companies compare implementations.