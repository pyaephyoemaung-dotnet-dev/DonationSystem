using DonationSystem.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DonationSystem.Middlewares
{
    public class CookiesMiddleware
    {
        private readonly RequestDelegate _next;

        public CookiesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IMessageWriter is injected into InvokeAsync
        public async Task InvokeAsync(HttpContext httpContext, AppDbContext _db)
        {
            string requestUrl = httpContext.Request.Path.ToString().ToLower();
            if(requestUrl == "/login" || requestUrl == "/signup" || requestUrl == "/home")
            {
                goto results;
            }
            var cookies = httpContext.Request.Cookies;
            if(cookies["userId"] is null || cookies["sessionId"] is null)
            {
                httpContext.Response.Redirect("/Login");
                goto results;
            }
            string userId = cookies["userId"]!.ToString();
            string sessionId = cookies["sessionId"]!.ToString();
            var login = await _db.SessionBlog.FirstOrDefaultAsync(x => x.sessionId == sessionId && x.userId == userId);
            if (login is null)
            {
                httpContext.Response.Redirect("/Login");
                goto results;
            }
            if (login.sessionExpired < DateTime.Now)
            {
                httpContext.Response.Redirect("/Login");
                goto results;
            }
        results :
            await _next(httpContext);
        }
    }
    public static class CookiesMiddlewareExtensions
    {
        public static IApplicationBuilder UseCookiesMiddleware(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<CookiesMiddleware>();
        }
    }
}
