Business Requirement

An e-commerce company initially supports only Stripe.

After some time, the business signs agreements with:

Razorpay
PayPal
Amazon Pay
Google Pay

The development team should be able to add a new payment provider without modifying the existing CheckoutService.

This is exactly what Open/Closed Principle is about.

Project Structure
PaymentGatewaySystem
│
├── Models
│     └── PaymentRequest.cs
│
├── Interfaces
│     └── IPaymentGateway.cs
│
├── Services
│     ├── StripePaymentGateway.cs
│     ├── RazorpayPaymentGateway.cs
│     ├── PayPalPaymentGateway.cs
│     └── CheckoutService.cs
│
└── Program.cs