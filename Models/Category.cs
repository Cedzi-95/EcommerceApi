using System.ComponentModel.DataAnnotations;

public class Category
{
    public Guid Id { get; set; }
    [Required]
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Slug { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
    public Guid? ParentId { get; set; } 
    public Category? Parent { get; set; }
    public ICollection<Category>? Children { get; set; }
    public ICollection<Product>? Products { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}