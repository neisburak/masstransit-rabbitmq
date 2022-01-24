using System;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace MessageContracts
{
    public class BusConfigurator
    {
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(configuration =>
            {
                configuration.Host(RabbitMQConstants.Uri, hostConfiguration =>
                {
                    hostConfiguration.Username(RabbitMQConstants.Username);
                    hostConfiguration.Password(RabbitMQConstants.Password);
                });

                registrationAction?.Invoke(configuration);
            });
        }
    }
}