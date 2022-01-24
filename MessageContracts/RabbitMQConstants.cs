using System.ComponentModel;

namespace MessageContracts
{
    public static class RabbitMQConstants
    {
        public const string Uri = "amqp://localhost";
        public const string Username = "guest";
        public const string Password = "guest";

        public const string OrderServiceQueueName = "order.service";
        public const string NotificationServiceQueueName = "notification.service";
        public const string BillingServiceQueueName = "billing.service";
    }
}