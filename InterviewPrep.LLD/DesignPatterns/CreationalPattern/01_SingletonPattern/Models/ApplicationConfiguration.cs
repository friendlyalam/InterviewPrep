
namespace InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Models
{
    public class ApplicationConfiguration
    {
        public string ConnectionString { get; set; }

        public string RedisServer { get; set; }

        public string JwtSecretKey { get; set; }

        public string ApiBaseUrl { get; set; }
    }
}
