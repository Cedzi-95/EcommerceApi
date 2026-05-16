public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(Guid userId, CreateOrderDto request);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<OrderResponseDto> GetByIdAsyn(Guid orderId);
    Task<Order> UpdateAsync(Guid orderId);
    Task<IEnumerable<OrderResponseDto>> GetOrdersByUserAsync(Guid userId);
    Task OrderStatusAsync(Guid orderId, Status newOrderStatus);
    Task PaymentStatusAsync(Guid OrderId, PaymentStatus paymentStatus);
}
