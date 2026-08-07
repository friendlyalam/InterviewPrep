

namespace InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Consumer
{
    public class OrderService
    {
        public void DisplayConfiguration()
        {
            var configuration =
                SingletonImplementations
                ._01_EagerSingleton
                .ConfigurationManager
                .Instance
                .GetConfiguration();

            Console.WriteLine("Order Service");

            Console.WriteLine(configuration.RedisServer);

            Console.WriteLine();
        }
    }
}
