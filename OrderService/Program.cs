using System;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using MessageContracts;
using MessageContracts.Consumers;

namespace OrderService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = BusConfigurator.ConfigureBus(configuration =>
            {
                configuration.ReceiveEndpoint(RabbitMQConstants.OrderServiceQueueName, e =>
                {
                    // Variations
                    // e.Consumer(() => new SubmitOrderCommandConsumer());
                    // e.Consumer(typeof(SubmitOrderCommandConsumer), type => Activator.CreateInstance(type));
                    e.Consumer<SubmitOrderCommandConsumer>();

                    e.UseRetry(r => r.Immediate(5));

                    e.UseRetry(r => r.Ignore(typeof(ArgumentNullException), typeof(DivideByZeroException)));

                    e.UseRateLimit(1000, TimeSpan.FromSeconds(5));

                    e.UseCircuitBreaker(cbConfiguration =>
                    {
                        cbConfiguration.TripThreshold = 10;
                        cbConfiguration.ActiveThreshold = 5;
                        cbConfiguration.TrackingPeriod = TimeSpan.FromMinutes(1);
                        cbConfiguration.ResetInterval = TimeSpan.FromMinutes(5);
                    });
                });
            });

            await bus.StartAsync();

            Console.WriteLine("Listening order commands... Press any key to exit.");
            Console.ReadKey();

            await bus.StopAsync();
        }
    }
}
