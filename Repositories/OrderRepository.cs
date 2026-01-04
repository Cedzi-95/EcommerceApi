using Microsoft.EntityFrameworkCore;

public class OrderRepository : EfRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserAsync(Guid userId)
    {
        var order = await _context.Orders
        .Where(o => o.UserId == userId).ToListAsync();
        return order;
    }

    public async Task OrderStatusAsync(Guid orderId, Status newOrderStatus)
    {
        var order = await _context.Orders
        .Where(o => o.Id == orderId)
        .FirstOrDefaultAsync();

        if (order != null)
        {
            order.OrderStatus = newOrderStatus;
        }
        

    }

    public async Task PaymentStatusAsync(Guid OrderId, PaymentStatus NewPaymentStatus)
    {
       var order = await _context.Orders
       .Where(o => o.Id == OrderId)
       .FirstOrDefaultAsync();

       if (order != null)
        {
            order.PaymentStatus = NewPaymentStatus;
        }

    }
}


public class OrderItemRepository : EfRepository<OrderItem>
{
    public OrderItemRepository(AppDbContext context) : base(context)
    {
    }
}