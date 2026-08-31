

using InterviewPrep.LLD.OOPS.Aggregation;

namespace InterviewPrep.LLD.OOPS.Aggregation
{

    //    Notice something very important.
    //The airline does not create pilots.
    //It receives them.
    public class Airline
    {
        public string AirlineName { get; }

        private readonly List<Pilot> _pilots;

        public Airline(string airlineName,
                       List<Pilot> pilots)
        {
            AirlineName = airlineName;
            _pilots = pilots;
        }

        public void DisplayPilots()
        {
            Console.WriteLine($"Airline : {AirlineName}");

            Console.WriteLine();

            foreach (Pilot pilot in _pilots)
            {
                pilot.Display();
            }
        }
    }
}

//Notice:

//List<Pilot> pilots

//is passed from outside.

//This is the biggest clue that this is Aggregation.
