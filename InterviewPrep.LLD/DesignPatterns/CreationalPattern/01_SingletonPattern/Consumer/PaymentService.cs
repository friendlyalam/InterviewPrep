

namespace InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Consumer
{
        public class PaymentService
        {
            public void DisplayConfiguration()
            {
                var configuration =
                    SingletonImplementations
                    ._01_EagerSingleton
                    .ConfigurationManager
                    .Instance
                    .GetConfiguration();

                Console.WriteLine("Payment Service");

                Console.WriteLine(configuration.ApiBaseUrl);

                Console.WriteLine();
            }
        }
}
