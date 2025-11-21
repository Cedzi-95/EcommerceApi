using System.ComponentModel.DataAnnotations.Schema;

public class Order
{   
    public Guid Id { get; set; }
    public DateTime OrderAt { get; set; }
    public double Payment { get; set; }
    [ForeignKey("userId")]
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }


}