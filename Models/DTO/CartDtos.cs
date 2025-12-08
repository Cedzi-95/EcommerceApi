public class AddToCartDto
{
    public Guid UserId { get; set; }

}

public class CartResponseDto
{
    public Guid CartId { get; set; }
    public Guid UserId { get; set; }
    public ICollection<CartItemResponseDto>? CartItems { get; set; }
}

public class CartItemResponseDto
{
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
}