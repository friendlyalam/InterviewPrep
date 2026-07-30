
namespace InterviewPrep.LLD.OOPS.Interfaces.FlightBookingSystem
{
    public interface IAirline
    {
        void BookTicket(string passengerName);

        void CancelTicket(string bookingId);

        void CheckIn(string bookingId);
    }
}
