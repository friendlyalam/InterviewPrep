
namespace InterviewPrep.LLD.Design.CreationalPattern._01_SingletonPattern.Models
{
    public class ApplicationConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string RedisServer { get; set; } = string.Empty;

        public string JwtSecretKey { get; set; }= string.Empty;

        public string ApiBaseUrl { get; set; }=string.Empty;
    }
}
