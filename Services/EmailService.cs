using System.Text;
using MimeKit;
using MailKit.Net.Smtp;
public interface IEmailService
{
    Task SendOrderConfirmationAsync(string toEmail, OrderResponseDto order);
}

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly IConfiguration _config;

    public EmailService(ILogger<EmailService> logger,
    IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }
    public async Task SendOrderConfirmationAsync(string toEmail, OrderResponseDto order)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_config["Email:From"]!));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = $"Order Confirmation #{order.OrderId.ToString()[..8].ToUpper()}";

        var body = new StringBuilder();
        body.AppendLine("<h2>Thanks for your order!</h2>");
        body.AppendLine($"<p><strong>Order ID:</strong> #{order.OrderId.ToString()[..8].ToUpper()}</p>");
        body.AppendLine($"<p><strong>Date:</strong> {order.OrderedAt:yyyy-MM-dd HH:mm}</p>");
        body.AppendLine("<h3>Items ordered:</h3><ul>");

        foreach (var item in order.OrderItems ?? [])
            body.AppendLine($"<li>{item.ProductName} x{item.Quantity} — ${item.RowTotal:F2}</li>");

        body.AppendLine($"</ul><hr/><p><strong>Total: ${order.TotalAmount:F2}</strong></p>");
        body.AppendLine("<p>Your order is being processed!</p>");

        message.Body = new TextPart("html") { Text = body.ToString() };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_config["Email:Host"]!, int.Parse(_config["Email:Port"]!), false);
        await smtp.AuthenticateAsync(_config["Email:From"]!, _config["Email:Password"]!);
        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);

        _logger.LogInformation("Confirmation email sent to {Email}", toEmail);
    }
}