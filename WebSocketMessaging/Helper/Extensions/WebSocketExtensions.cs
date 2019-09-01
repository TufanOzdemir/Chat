using Helper.Handlers;
using Helper.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SocketService;
using System;

namespace Helper.Extensions
{
    public static class WebSocketExtensions
    {
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app,
                                                              PathString path,
                                                              WebSocketHandler handler)
        {
            return app.Map(path, (_app) => _app.UseMiddleware<WebSocketManagerMiddleware>(handler));
        }

        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<WebSocketConnectionManager>();
            services.AddSingleton(typeof(ChatRoomHandler));
            return services;
        }

        public static string GetWebSocketName(this string name)
        {
            name += "-" + Guid.NewGuid().ToString().Substring(0, 3);
            return name;
        }
    }
}
