public class OrderRepository : EfRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }
}


public class OrderItemRepository : EfRepository<OrderItem>
{
    public OrderItemRepository(AppDbContext context) : base(context)
    {
    }
}