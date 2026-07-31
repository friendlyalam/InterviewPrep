using InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Models;

namespace InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.SingletonImplementations._03_ThreadSafeSingleton
{
    public sealed class ConfigurationManager
    {
        private static ConfigurationManager? _instance;

        private static readonly object _lock = new object();

        private readonly ApplicationConfiguration _configuration;

        private ConfigurationManager()
        {
            Console.WriteLine("Loading Configuration...\n");

            _configuration = new ApplicationConfiguration
            {
                ConnectionString = "Server=SQL01;Database=EnterpriseDB;",
                RedisServer = "redis.company.com",
                JwtSecretKey = "EnterpriseSecretKey",
                ApiBaseUrl = "https://api.company.com"
            };
        }

        public static ConfigurationManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigurationManager();
                    }

                    return _instance;
                }
            }
        }

        public ApplicationConfiguration GetConfiguration()
        {
            return _configuration;
        }
    }
}
