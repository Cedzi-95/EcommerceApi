public class Category
{
    public int categoryId { get; set; }
    public string? CategoryName { get; set; }
    public ICollection<Product>? Products { get; set; }
}