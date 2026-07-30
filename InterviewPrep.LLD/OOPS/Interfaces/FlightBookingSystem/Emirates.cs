
namespace InterviewPrep.LLD.OOPS.Interfaces.FlightBookingSystem
{
    public class Emirates : IAirline
    {
        public void BookTicket(string passengerName)
        {
            Console.WriteLine($"Emirates booked ticket for {passengerName}");
        }

        public void CancelTicket(string bookingId)
        {
            Console.WriteLine($"Emirates cancelled {bookingId}");
        }

        public void CheckIn(string bookingId)
        {
            Console.WriteLine($"Emirates checked in {bookingId}");
        }
    }
}
