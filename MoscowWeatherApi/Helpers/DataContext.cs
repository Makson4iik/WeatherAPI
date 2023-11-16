using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using MoscowWeatherApi.Entities;

namespace MoscowWeatherApi.Helpers
{
    public static class MyModuleInitializer
    {
        [ModuleInitializer]
        public static void Initialize()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }

    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WeatherDatabase"));
        }

        public DbSet<MoscowWeather> Weathers { get; set; }
    }
}
