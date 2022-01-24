using System;
using System.Threading.Tasks;
using MassTransit;
using MessageContracts.Commands;
using MessageContracts.Events;

namespace MessageContracts.Consumers
{
    public class SubmitOrderCommandConsumer : IConsumer<ISubmitOrderCommand>
    {
        public async Task Consume(ConsumeContext<ISubmitOrderCommand> context)
        {
            var message = context.Message;
            await Console.Out.WriteLineAsync($"Order with Id: {message.OrderId} and Code: {message.OrderCode} has beed submitted.");

            await context.Publish<IOrderSubmittedEvent>(new
            {
                message.OrderId,
                message.OrderCode,
                Success = true
            });
        }
    }
}