public class Cart
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public ICollection<CartItem>? CartItems { get; set;}

}

public class CartItem
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public Guid CartId { get; set; }
    public Cart? Cart { get; set;}
    public int Quantity { get; set; }
}