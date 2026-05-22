public class CreatePaymentIntentDto
{
    public decimal Amount { get; set; }
}

public class PaymentIntentResponseDto
{
    public string ClientSecret { get; set; } = string.Empty;
    public string PublishableKey { get; set; } = string.Empty;
}

public class ConfirmOrderPaymentDto
{
    public Guid OrderId { get; set; }
    public string PaymentIntent { get; set; } = string.Empty;
}