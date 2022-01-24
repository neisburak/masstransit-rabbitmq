using System;
using System.Threading.Tasks;
using MassTransit;

namespace MessageContracts.Events
{
    public class SubmittedOrderNotificationEventConsumer : IConsumer<IOrderSubmittedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderSubmittedEvent> context)
        {
            var message = context.Message;
            await Console.Out.WriteLineAsync($"A bill has been created for the order Id with: {message.OrderId}");
        }
    }
}