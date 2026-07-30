
namespace InterviewPrep.LLD.OOPS.Interfaces.FlightBookingSystem
{
    public class AirIndia : IAirline
    {
        public void BookTicket(string passengerName)
        {
            Console.WriteLine($"Air India booked ticket for {passengerName}");
        }

        public void CancelTicket(string bookingId)
        {
            Console.WriteLine($"Air India cancelled {bookingId}");
        }

        public void CheckIn(string bookingId)
        {
            Console.WriteLine($"Air India checked in {bookingId}");
        }
    }
}
