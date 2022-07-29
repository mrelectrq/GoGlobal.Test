using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace GoGlobal.Test.Backend.Services;

public interface IMailSender
{
    Task<bool> SendMailAsync(string to, string subject, string message);
}

public class EmailService : IMailSender
{
    private MailMessage Message { get; set; }
    private readonly MailAddress _from;
    private readonly SmtpClient _client;
//smtpaltio@gmail.com
    public EmailService(string SmtpHost,string SmtpPort)
    {
        
        _from = new MailAddress("alextioggss@outlook.com");
        _client = new SmtpClient
        {
            Host = SmtpHost,
            Port = int.Parse(SmtpPort),
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(
                "alextioggss@outlook.com",
                "Test123#Test123#")
        };
    }

    public async Task<bool> SendMailAsync(string to, string emailSubject, string emailBody)
    {
        try
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            Message = new MailMessage { From = _from };
            Message.To.Add(new MailAddress(to));
            Message.IsBodyHtml = true;
            Message.Subject = emailSubject;
            Message.Body = emailBody;
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            await _client.SendMailAsync(Message);

            return true;
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
            return false;
        }
    }
}