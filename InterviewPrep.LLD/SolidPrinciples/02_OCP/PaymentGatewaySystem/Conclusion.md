Output
================================
CHECKOUT STARTED
================================

Order Id : 1001

Customer : Mohd Alam

Amount : ₹65000

---------- STRIPE ----------

Processing ₹65000 using Stripe.

Stripe Payment Successful

Checkout Completed.
New Requirement

Business says

Add Razorpay.

How many lines change inside

CheckoutService

Answer

ZERO

Only Program changes.

IPaymentGateway paymentGateway =
    new RazorpayPaymentGateway();

Everything else remains exactly the same.


Again Business Says

Add PayPal.

Again

IPaymentGateway paymentGateway =
    new PayPalPaymentGateway();

Nothing changes inside

CheckoutService
Next Year

Business says

Support Amazon Pay.

Developer creates

public class AmazonPayGateway
        : IPaymentGateway
{
    public void ProcessPayment(
        PaymentRequest paymentRequest)
    {
        Console.WriteLine(
            "Amazon Pay Successful");
    }
}

Only this class is added.

No modification in

CheckoutService
StripePaymentGateway
RazorpayPaymentGateway
PayPalPaymentGateway

This is Open for Extension.

Object Interaction
Program
    │
    ▼
CheckoutService
    │
    ▼
IPaymentGateway
    │
 ┌──┼───────────────┐
 │  │               │
 ▼  ▼               ▼
Stripe Razorpay   PayPal
Why does this satisfy OCP?

Because

Adding

GooglePayGateway

requires

✅ Creating a new class

❌ Modifying CheckoutService

What if we violate OCP?

Bad design

public void Process(string paymentType)
{
    if(paymentType=="Stripe")
    {

    }
    else if(paymentType=="Razorpay")
    {

    }
    else if(paymentType=="PayPal")
    {

    }
}

Next provider?

Modify this method.

Again.

Again.

Again.

Eventually the method becomes hundreds of lines long and is difficult to maintain.

Interview Questions
Why is CheckoutService closed for modification?

Because whenever a new payment gateway is added, CheckoutService remains unchanged.

Which class is open for extension?

Any new class implementing

IPaymentGateway

Example

ApplePayGateway

GooglePayGateway

AmazonPayGateway
Which OOP concepts are used?
Interface
Abstraction
Polymorphism
Constructor Injection
Which SOLID principle naturally supports OCP?

Dependency Inversion Principle (DIP) is often used alongside OCP because depending on abstractions makes extension much easier.

Product Company Review
Good Points

✅ Interface-based design

✅ Constructor Injection

✅ Polymorphism

✅ Easy testing

✅ Easily extensible

✅ Low coupling

One Improvement for a Real Production System

If I were reviewing this code in a product company, I would suggest one enhancement:

Instead of manually creating StripePaymentGateway or RazorpayPaymentGateway in Program.cs, 
use the ASP.NET Core Dependency Injection container and configuration (for example, selecting
the gateway based on configuration or a factory). We'll intentionally postpone that until we cover
Dependency Inversion Principle (DIP) and Factory Pattern, so you first understand the design without framework magic.


