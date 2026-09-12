We'll build an Airline Management System.

Business Requirement

An airline has pilots.

Examples:

Emirates
Air India
Lufthansa

Pilots:

Can resign
Can join another airline
Exist independently

Therefore,

this is Aggregation.

Class Diagram
               Airline
                   ◇
                   │
        -----------------------
        │          │          │
      Pilot      Pilot      Pilot

The ◇ (Hollow Diamond) represents Aggregation.