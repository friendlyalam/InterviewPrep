using InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Models;
using System.Numerics;

namespace InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.SingletonImplementations._02_LazySingleton
{
    public sealed class ConfigurationManager
    {
        private static ConfigurationManager? _instance;

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
                if (_instance == null)
                {
                    _instance = new ConfigurationManager();
                }

                return _instance;
            }
        }

        public ApplicationConfiguration GetConfiguration()
        {
            return _configuration;
        }
    }
}

//The whole class in one sentence

//ConfigurationManager uses a private constructor to prevent external object creation,
//a static _instance field to store the single object, and a public static Instance property
//to lazily create and return that same object whenever the application needs configuration.

//And the four pieces you should immediately recognize in any Singleton implementation are:

//private constructor
//        +
//static instance
//        +
//public static Instance
//        +
//controlled creation
//        =
//Singleton



//Complete flow

//The entire class can be understood like this:

//                ConfigurationManager
//                         │
//                         │
//                  Instance property
//                         │
//                Is _instance null?
//                    /          \
//                  Yes           No
//                   │             │
//                   ▼             ▼
//             Create object    Return existing
//                   │             │
//                   ▼             │
//          private constructor     │
//                   │              │
//                   ▼              │
//       Load ApplicationConfiguration
//                   │              │
//                   └──────┬───────┘
//                          ▼
//                   return instance