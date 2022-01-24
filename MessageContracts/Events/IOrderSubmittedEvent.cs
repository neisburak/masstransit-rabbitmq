namespace MessageContracts.Events
{
    public interface IOrderSubmittedEvent
    {
        int OrderId { get; }
        string OrderCode { get; }
        bool Success { get; }
    }
}