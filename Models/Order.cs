using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

public class Order
{   
    public Guid Id { get; set; }
    public DateTime OrderedAt { get; set; }
    public decimal Payment { get; set; }
    public Guid UserId { get; set; }
    public UserEntity? User { get; set;}
    public Status OrderStatus { get; set; } 
    public PaymentStatus PaymentStatus { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
    
    
}


public class OrderItem
{
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public enum Status
{
    PENDING,
    CONFIRMED,
    PROCESSING,
    SHIPPED,
    DELIVERED,
    CANCELLED,
    REFUNDED
}

public enum PaymentStatus
{
    PENDING,
    PAID,
    FAILED,
    REFUNDED
}
