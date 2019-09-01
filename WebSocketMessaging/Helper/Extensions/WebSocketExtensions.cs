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
        /// <summary>
        /// WebSocket isteklerini belirtilen path için middleware aktif eder.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="path"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app,
                                                              PathString path,
                                                              WebSocketHandler handler)
        {
            return app.Map(path, (_app) => _app.UseMiddleware<WebSocketManagerMiddleware>(handler));
        }

        /// <summary>
        /// Ayağa kaldırma işlemi.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<WebSocketConnectionManager>();
            services.AddSingleton(typeof(ChatRoomHandler));
            return services;
        }

        /// <summary>
        /// Eğer isimsiz kullanıcı katılırsa ona default rasgele bir isim verir.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetWebSocketName(this string name)
        {
            name += "-" + Guid.NewGuid().ToString().Substring(0, 3);
            return name;
        }
    }
}
