public class CreateOrderDto
{
    public Guid UserId { get; set; }
    public ICollection<OrderItemResponseDto>? OrderItems { get; set;}
}

public class OrderResponseDto
{
    public Guid OrderId {get; set; }
    public Guid UserId { get; set; }
     public Status OrderStatus { get; set; } 
    public PaymentStatus PaymentStatus { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderedAt { get; set; } 
    public ICollection<OrderItemResponseDto>? OrderItems { get; set;}

}

public class OrderItemResponseDto
{
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal RowTotal { get; set; }
}