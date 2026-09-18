Output
Loading Configuration...

User Service
Server=SQL01;Database=EnterpriseDB;

Order Service
redis.company.com

Payment Service
https://api.company.com

--------------------------------
True
How will we move to Lazy Singleton?

This is the best part.

Nothing changes in:

✅ Models
✅ UserService
✅ OrderService
✅ PaymentService

Only this line changes.

Current

using SingletonPattern.SingletonImplementations._01_EagerSingleton;

becomes

using SingletonPattern.SingletonImplementations._02_LazySingleton;

and we'll update the ConfigurationManager implementation accordingly.

One Small Improvement (Enterprise Standard)

Instead of hardcoding the implementation namespace inside every consumer like:

ConfigurationManager.Instance

I recommend introducing a facade/access point later in this project for learning purposes. 
That way, when we switch from Eager to Lazy, we'll only update one place instead of changing all consumer classes.

This mirrors how enterprise applications minimize the impact of implementation changes.

We'll introduce that improvement after you've understood the basic Singleton implementations so 
that the design evolves naturally rather than hiding important concepts too early.