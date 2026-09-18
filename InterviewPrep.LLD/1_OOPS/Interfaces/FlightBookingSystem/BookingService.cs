
using InterviewPrep.LLD.OOPS.Interfaces.FlightBookingSystem;

namespace InterviewPrep.LLD.OOPS.Interfaces.FlightBookingSystem
{
    public class BookingService
    {
        private readonly IAirline _airline;

        public BookingService(IAirline airline)
        {
            _airline = airline;
        }

        public void CreateBooking(string passenger)
        {
            _airline.BookTicket(passenger);
        }
    }
}

//This class doesn't know:

//Air India
//Emirates
//Indigo

//It only knows:

//IAirline

//That's exactly how enterprise applications are designed.