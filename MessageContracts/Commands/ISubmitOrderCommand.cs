namespace MessageContracts.Commands
{
    public interface ISubmitOrderCommand
    {
        int OrderId { get; }
        string OrderCode { get; }
    }
}