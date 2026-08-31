Output
Mohd Alam placed an order.

Order Id : 101
Item      : Wireless Mouse
Why is this Association?

Look at this method carefully.

public void PlaceOrder(Order order)
{
    order.DisplayOrder();
}

The Customer object is using the Order object.

It does not:

create the order
own the order
permanently store the order

It simply collaborates with it.

That is Association.

Memory Representation

When this code executes:

Customer customer =
    new Customer("Mohd Alam");

Order order =
    new Order(101, "Wireless Mouse");

Memory:

Stack
────────────────────────

customer ---------+
                  |
order ------------|------+
                  |      |
                  ▼      ▼

Heap
────────────────────────

Customer Object

Name = Mohd Alam


Order Object

OrderId = 101

Item = Wireless Mouse

Now:

customer.PlaceOrder(order);

During method execution:

Customer Object

↓

uses

↓

Order Object

After the method finishes:

Nothing changes.

Both objects still exist independently.

Important Observation

The Customer class does not have:

private Order _order;

or

public Order Order { get; set; }

It simply receives the object as a parameter.

That is one of the simplest forms of Association.

Real Product Company Examples
Object 1	Relationship	Object 2
Customer	places	Order
Doctor	treats	Patient
Employee	works on	Project
Rider	delivers	Order
User	watches	Movie
Student	attends	Course

Notice that in all these examples, one object uses another to perform a business operation.

Interview Questions
Q1. Why is this Association?

Because Customer collaborates with Order through a method parameter, and both objects have independent lifecycles.

Q2. Does Customer own the Order?

No.

It simply uses the Order object.

Q3. Can the Order exist without the Customer?

Yes.

Order order = new Order(101, "Wireless Mouse");

The Order object exists even if no customer calls PlaceOrder().

Q4. Who creates the Order?

The caller (Main method in this example), not the Customer.