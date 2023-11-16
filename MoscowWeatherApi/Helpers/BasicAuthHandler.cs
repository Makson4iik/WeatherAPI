using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MoscowWeatherApi.Helpers
{
    public class BasicAuthHandler
    {
        private readonly RequestDelegate _next;
        private readonly string _relm;

        public BasicAuthHandler(RequestDelegate next, string relm) {
            _next = next;
            _relm = relm;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                return;
            }

            var header = context.Request.Headers["Authorization"];
            var encodedCreds = header.ToString()[6..];
            var creds = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCreds));
            string[] uidpwd = creds.Split(':');
            var uid = uidpwd[0];
            var password = uidpwd[1];

            if (!UserValidate.Login(uid, password)) {
                context.Response.StatusCode = 401;
                return;
            }

            await _next(context);
        }
    }
}
