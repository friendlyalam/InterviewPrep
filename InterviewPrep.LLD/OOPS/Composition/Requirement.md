Business Requirement

Build an Order Management System.

Each order must contain one shipping address.

The address:

cannot be shared with another order in this example
is created by the order
belongs to the order
Class Diagram
           Order
             ◆
             │
      ShippingAddress

The filled diamond (◆) represents Composition.