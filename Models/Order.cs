using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

public class Order
{   
    public Guid Id { get; set; }
    public DateTime OrderAt { get; set; }
    public double Payment { get; set; }
    [ForeignKey("userId")]
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    
}


public enum Status
{
    PENDING,
    COMFIRMED,
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
