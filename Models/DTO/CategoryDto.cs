using System.ComponentModel.DataAnnotations;

public class CategoryDto
{
    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Slug { get; set; } = string.Empty;
    public Guid? ParentId { get; set; }


}

public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public ICollection<CategoryResponseDto>? Children { get; set; }

}