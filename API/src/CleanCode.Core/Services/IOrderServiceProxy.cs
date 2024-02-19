namespace CleanCode.Core.Communication
{
    // Inter to define contracts of Order Microservice. 
    public interface IOrderServiceProxy
    {
        Task<List<long>> GetOrders(string orderedBy);
    }
}
