using CBF.Application;
using CBF.Application.InternalEvent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Retry.Simple;
using System.Threading.Tasks;

namespace CBF.Api.Extensions
{
    public static class RebusApplicationExtensions
    {
        public static IServiceCollection AddRebus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AutoRegisterHandlersFromAssembly(typeof(ApplicationModule).Assembly);
            services.AddRebus((configure, provider) => configure
                .Transport(t =>
                {
                    var opt = t.UseRabbitMq(configuration.GetConnectionString("RabbitMQ"), $"CBF");
                })
                .Options(c =>
                {
                    c.SimpleRetryStrategy(secondLevelRetriesEnabled: true, maxDeliveryAttempts: 2, errorQueueAddress: "CBFError");
                }));

            return services;
        }

        public static void SubscribeToEvents(this IBus bus)
        {
            Task.WaitAll(new Task[]
            {
                bus.Subscribe<CreateEventInternalEvent>()
            });
        }
    }
}
