using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    public int ProductId { get; set; }
    [Required]
    public string? ProductName { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public bool IsAvailable { get; set; }
    [ForeignKey("CategoryId")]
    public string? CategoryId { get; set; }
    public Category? Category { get; set; }
}