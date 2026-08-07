using InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Models;

namespace InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.SingletonImplementations._01_EagerSingleton
{
    public sealed class ConfigurationManager
    {
        private static readonly ConfigurationManager _instance =
            new ConfigurationManager();

        private readonly ApplicationConfiguration _configuration;

        private ConfigurationManager()
        {
            Console.WriteLine("Loading Configuration...\n");

            _configuration = new ApplicationConfiguration
            {
                ConnectionString =
                    "Server=SQL01;Database=EnterpriseDB;",

                RedisServer =
                    "redis.company.com",

                JwtSecretKey =
                    "EnterpriseSecretKey",

                ApiBaseUrl =
                    "https://api.company.com"
            };
        }

        public static ConfigurationManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public ApplicationConfiguration GetConfiguration()
        {
            return _configuration;
        }
    }
}
