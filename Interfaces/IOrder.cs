public interface IOrderRepository : IRepository<Order>
{
    
}

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Guid userId, CreateOrderDto request);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order> GetByIdAsyn(Guid orderId);
    Task<Order> UpdateAsync(Guid orderId);
}