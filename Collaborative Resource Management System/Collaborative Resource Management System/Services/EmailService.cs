using Collaborative_Resource_Management_System.Models;
using Collaborative_Resource_Management_System.Models.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendLowStockAlert(IEnumerable<InventoryItem> lowStockItems)
    {
        var body = "The following items are low in stock:\n";
        foreach (var item in lowStockItems)
        {
            if (item.ItemType == ItemType.Consumable && item.Consumable != null)
            {
                body += $"{item.Name}: {item.Consumable.QuantityAvailable} available\n";
            }
        }


        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["EmailSettings:FromAddress"]),
            Subject = "Low Stock Alert",
            Body = body,
            IsBodyHtml = false 
        };

        mailMessage.To.Add(_configuration["EmailSettings:AlertEmailAddress"]); 

        var smtpHost = _configuration["EmailSettings:SmtpHost"];
        var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
        var smtpUser = _configuration["EmailSettings:SmtpUser"];
        var smtpPass = _configuration["EmailSettings:SmtpPass"];
        var useSSL = bool.Parse(_configuration["EmailSettings:UseSSL"]);

        using var smtpClient = new SmtpClient(smtpHost, smtpPort)
        {
            Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass),
            EnableSsl = useSSL
        };

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
        }
    }


    public async Task SendTestEmailAsync()
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["EmailSettings:FromAddress"]),
            Subject = "Test Email",
            Body = "This is a test email from the inventory system.",
            IsBodyHtml = true
        };

        mailMessage.To.Add(_configuration["EmailSettings:TestEmailAddress"]);

        var smtpHost = _configuration["EmailSettings:SmtpHost"];
        var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
        var smtpUser = _configuration["EmailSettings:SmtpUser"];
        var smtpPass = _configuration["EmailSettings:SmtpPass"];
        var useSSL = bool.Parse(_configuration["EmailSettings:UseSSL"]);

        using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
        {
            smtpClient.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass);
            smtpClient.EnableSsl = useSSL;

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
            }
        }
    }
}
