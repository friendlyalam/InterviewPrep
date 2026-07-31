

namespace InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Consumer
{

    //consumer classes never change throughout the Singleton project.
    public class UserService
    {
        public void DisplayConfiguration()
        {
            var configuration =
                SingletonImplementations
                ._01_EagerSingleton
                .ConfigurationManager
                .Instance
                .GetConfiguration();

            Console.WriteLine("User Service");

            Console.WriteLine(configuration.ConnectionString);

            Console.WriteLine();
        }
    }
}
