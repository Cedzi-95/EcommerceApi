using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
    [ForeignKey("CategoryId")]
    public string? CategoryId { get; set; }
    public Category? Category { get; set; }
}