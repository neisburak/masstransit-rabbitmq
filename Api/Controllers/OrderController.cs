using System;
using System.Threading.Tasks;
using Api.Models;
using MassTransit;
using MessageContracts;
using MessageContracts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ISendEndpoint _bus;

        public OrderController()
        {
            var bus = BusConfigurator.ConfigureBus();
            var sendToUri = new Uri($"{RabbitMQConstants.Uri}/{RabbitMQConstants.OrderServiceQueueName}");
            _bus = bus.GetSendEndpoint(sendToUri).Result;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SubmitOrder submitOrder)
        {
            await _bus.Send<ISubmitOrderCommand>(submitOrder);

            return Ok("Order submitted successfully.");
        }
    }
}