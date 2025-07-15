using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
   
    public int Id { get; set; }
    public DateTime OrderAt { get; set; }
    public double Payment { get; set; }
    [ForeignKey("userId")]
    public string? UserId { get; set; }
    [ForeignKey("productId")]
    public string? ProductId { get; set; }


}