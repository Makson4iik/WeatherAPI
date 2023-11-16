using MoscowWeatherApi.Models;

namespace MoscowWeatherApi.Helpers
{
    public class UserRepository
    {
        private readonly List<User> _users = new()
        {
            new User()
            {
                ID = 1,
                UserName = "MaleUser",
                Password = "123456"
            },
            new User()
            {
                ID = 2,
                UserName = "FemaleUser",
                Password = "abcdef"
            },
            new User()
            {
                ID = 3,
                UserName = "admin",
                Password = "admin"
            }
        };

        public List<User> GetUsers()
        {
            return _users;
        }
    }
}
