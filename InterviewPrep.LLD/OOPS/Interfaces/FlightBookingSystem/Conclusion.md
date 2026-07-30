Tomorrow, if the business adds:

Qatar Airways
Singapore Airlines
Lufthansa
British Airways

You only create new classes implementing IAirline.

BookingService remains unchanged.

Why this is a better Interface example

This demonstrates a true capability:

Air India can book tickets
Emirates can book tickets
Indigo can book tickets

An interface models that shared capability.

An inheritance hierarchy like:

Airline
   ▲
AirIndia