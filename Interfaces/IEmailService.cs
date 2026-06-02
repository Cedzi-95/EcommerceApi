public interface IEmailService
{
    Task SendOrderConfirmationAsync(string toEmail, OrderResponseDto order);
}
