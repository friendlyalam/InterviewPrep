

namespace InterviewPrep.LLD.OOPS.Aggregation
{
    public class Pilot
    {
        public int PilotId { get; }
        public string Name { get; }
        public int Experience { get; }

        public Pilot(int pilotId, string name, int experience)
        {
            PilotId = pilotId;
            Name = name;
            Experience = experience;
        }

        public void FlyAircraft()
        {
            Console.WriteLine($"{Name} is flying the aircraft.");
        }

        public void Display()
        {
            Console.WriteLine(
                $"Id : {PilotId}, Name : {Name}, Experience : {Experience} years");
        }
    }
}
