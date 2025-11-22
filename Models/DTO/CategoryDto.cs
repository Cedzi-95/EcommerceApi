using System.ComponentModel.DataAnnotations;

public class CategoryDto
{
    [Required]
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Slug { get; set; } = string.Empty;

}

public class CategoryResponseDto
{
    public Guid Id { get; set; }
    [Required]
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Slug { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }

}