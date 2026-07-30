Imagine you're building software similar to:

Airline reservation systems
Travel booking platforms
Corporate travel management software

The system must support multiple airlines.

                    IAirline

                        ▲

        ┌───────────────┼────────────────┐

        │               │                │

   AirIndia      Emirates        Indigo

Notice something important:

Air India is NOT an Emirates.

Indigo is NOT an Air India.

So inheritance would not make sense here.

Instead, each airline can perform booking operations.

That's a capability, which is exactly what interfaces model.