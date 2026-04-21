using System.Security.Cryptography.X509Certificates;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserAsync(Guid userId);
    Task OrderStatusAsync(Guid orderId, Status newOrderStatus);
    Task PaymentStatusAsync(Guid OrderId, PaymentStatus newPaymentStatus);
}



