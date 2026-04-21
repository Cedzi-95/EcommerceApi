public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(Guid userId, CreateOrderDto request);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order> GetByIdAsyn(Guid orderId);
    Task<Order> UpdateAsync(Guid orderId);
    Task<IEnumerable<Order>> GetOrdersByUserAsync(Guid userId);
    Task<Order> OrderStatusAsync(Guid orderId);
    Task<Order> PaymentStatusAsync(Guid OrderId);
}
