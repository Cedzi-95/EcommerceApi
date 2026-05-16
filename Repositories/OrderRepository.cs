using Microsoft.EntityFrameworkCore;

public class OrderRepository : EfRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserAsync(Guid userId)
    {
        var order = await _context.Orders
        .Where(o => o.UserId == userId)
        .Include(o => o.OrderItems!)
        .ThenInclude(oi => oi.Product)
        .ToListAsync();
        return order;
    }
     public override async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems!)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task OrderStatusAsync(Guid orderId, Status newOrderStatus)
    {
        var order = await _context.Orders
        .Where(o => o.Id == orderId)
        .FirstOrDefaultAsync();

        if (order != null)
        {
            order.OrderStatus = newOrderStatus;
            await UpdateAsync(order);
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
            await UpdateAsync(order);
        }

    }
}


public class OrderItemRepository : EfRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext context) : base(context)
    {
    }

    public Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }
}