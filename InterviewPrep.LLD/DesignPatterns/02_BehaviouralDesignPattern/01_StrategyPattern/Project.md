Business Logic

Premium members always receive

15%

discount.

Example

₹1000

↓

₹850
Strategy Relationship
                 IPricingStrategy
                       ▲
       ┌───────────────┼────────────────┐
       │               │                │
       ▼               ▼                ▼

RegularPricing  FestivalPricing  PremiumPricing

Notice

All three classes implement

IPricingStrategy

Therefore

PricingService

doesn't care which one it receives.

This is exactly what the Liskov Substitution Principle (LSP) means.

Why No if-else?

Many beginners write

if(customerType=="Regular")
{
}
else if(customerType=="Festival")
{
}
else if(customerType=="Premium")
{
}

This is bad because every new pricing type forces you to modify the same class.

Instead

Regular Strategy

Festival Strategy

Premium Strategy

Each class owns one algorithm.

Open/Closed Principle

Today

Regular

Festival

Premium

Tomorrow

Corporate Pricing

Do we modify existing strategies?

No.

We simply create

CorporatePricingStrategy

and register it with DI.

Existing code remains untouched.

Why Keep Strategies Small?

Each strategy should answer only one question:

"How do I calculate the price for this pricing model?"

Nothing else.

No logging.

No database.

No API calls.

No caching.

Those responsibilities belong elsewhere.

Enterprise Tip

Many developers put validation inside each strategy.

For example

if(context.Product==null)

in every strategy.

Avoid duplicating that logic.

Instead:

Program

↓

PricingService

↓

Validate Once

↓

Strategy

This keeps strategies focused on their algorithm.

SOLID Principles
Principle	How Applied
SRP	One pricing algorithm per class.
OCP	Add new strategies without changing existing ones.
LSP	Any strategy can replace another through IPricingStrategy.
ISP	Small interface with a single responsibility.
DIP	Consumers depend on IPricingStrategy, not concrete implementations.
Product Company Discussion

This is exactly how many enterprise systems are structured:

Amazon
Pricing Strategy

↓

Festival

Prime

Corporate

Bulk Purchase
Uber
Fare Strategy

↓

Normal

Surge

Airport

Shared Ride
Microsoft
Authentication Strategy

↓

JWT

Azure AD

Google

Windows

Each algorithm is isolated and independently testable.

Interview Questions
Q1 Why create multiple classes instead of one large class?

To isolate algorithms, improve maintainability, and follow the Open/Closed Principle.

Q2 Why should strategies not access the database directly?

Because their responsibility is only the pricing algorithm. Data access belongs to repositories or services.

Q3 Can one strategy call another strategy?

Generally, no. Strategies should remain independent. If algorithms need orchestration, that responsibility belongs to a service or coordinator, not to another strategy.