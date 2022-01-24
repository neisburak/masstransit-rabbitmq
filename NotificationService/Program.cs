using System;
using System.Threading.Tasks;
using MassTransit;
using MessageContracts;
using MessageContracts.Events;

namespace NotificationService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = BusConfigurator.ConfigureBus(configuration =>
            {
                configuration.ReceiveEndpoint(RabbitMQConstants.NotificationServiceQueueName, e =>
                {
                    e.Consumer<SubmittedOrderNotificationEventConsumer>();
                });
            });

            await bus.StartAsync();

            Console.WriteLine("Listening order commands... Press any key to exit.");
            Console.ReadKey();

            await bus.StopAsync();
        }
    }
}
