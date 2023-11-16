using MoscowWeatherApi.Helpers;
using MoscowWeatherApi.Entities;
using Newtonsoft.Json;

namespace MoscowWeatherApi.Services
{
    public interface IMoscowWeatherSrv
    {
        MoscowWeather GetWeather(DateTime date);
        MoscowWeather CreateWeather(DateTime date);
    }

    public class MoscowWeatherSrv : IMoscowWeatherSrv
    {
        private const string WeatherUrl = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{0}/{1}?key={2}";
        private const string Location = "Moscow";
        private const string ApiKey = "UQAP8SH9KDTHM4JJRZUTBCRJR";

        private readonly DataContext _context;

        public MoscowWeatherSrv(DataContext context)
        {
            _context = context;
        }

        public MoscowWeather GetWeather(DateTime date)
        {
            return GetMoscowWeather(date).Result;
        }
        
        public MoscowWeather CreateWeather(DateTime date)
        {
            MoscowWeather weather = GetMoscowWeather(date).Result;
            _context.Weathers.Add(weather);
            _context.SaveChanges();

            return weather;
        }

        private async static Task<MoscowWeather> GetMoscowWeather(DateTime date)
        {
            var client = new HttpClient();
            var request = new Uri(string.Format(WeatherUrl, Location, date.ToString("yyyy-M-d"), ApiKey));

            HttpResponseMessage response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseBody))
            {
                return new();
            }

            dynamic weather = JsonConvert.DeserializeObject(responseBody);
            if (weather == null)
            {
                return new();
            }

            int temp = 0;
            foreach (var day in weather.days)
            {
                temp = day.temp;
                break;
            }

            return new() {
                Date = date,
                TemperatureC = (temp - 32) * 5 / 9
            };
        }
    }
}
