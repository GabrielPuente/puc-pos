using CBF.Application;
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
                    var errorQueue = "Error";

                    c.SimpleRetryStrategy(secondLevelRetriesEnabled: true, maxDeliveryAttempts: 2, errorQueueAddress: errorQueue);
                }));

            return services;
        }

        public static IBus SubscribeToEvents(this IBus bus)
        {
            Task.WaitAll(new Task[]
            {
            });

            return bus;
        }
    }
}
