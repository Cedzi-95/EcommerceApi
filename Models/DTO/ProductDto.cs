using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

public class AddProductDto
{
    public string ProductName { get; set; } = string.Empty;
    [Required]
    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stock can't have negative value")]
    public int StockQuantity { get; set; }
    [Required]
    public string? CategoryId { get; set; }
}

public class UpdateProductDto
{
     public string ProductName { get; set; } = string.Empty;
    [Required]
    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stock can't have negative value")]
    public int StockQuantity { get; set; }
    [Required]
    public bool IsAvailable { get; set; } = true;
    public string? CategoryId { get; set; }
}

public class ProductImageDto
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string AltText { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsPrimary { get; set; } = false;
}

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsAvailable { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }
    public ICollection<ProductImageDto> Images { get; set; } = new List<ProductImageDto>();


}