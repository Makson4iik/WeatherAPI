using System.Collections.Generic;
using MoscowWeatherApi.Models;

namespace MoscowWeatherApi.Helpers
{
    public class UserValidate
    {
        //This method is used to check the user credentials
        public static bool Login(string username, string password)
        {
            UserRepository userBL = new ();
            var userLists = userBL.GetUsers();
            return userLists.Any(user =>
                user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                && user.Password == password);
        }
    }
}
