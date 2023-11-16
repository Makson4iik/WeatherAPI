namespace MoscowWeatherApi.Models
{
    public class User
    {
        public int ID { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
