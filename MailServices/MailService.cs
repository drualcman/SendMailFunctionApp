namespace MailServices;

public class MailService
{
    readonly IConfiguration Configuration;

    public MailService(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public async ValueTask SendMail(string subject, string body)
    {
        try
        {
            using MailMessage message = new MailMessage(
                Configuration["MailService:From"],
                Configuration["MailService:AdministratorEMail"]
                );
            message.Subject = subject;
            message.Body = body;

            using SmtpClient smtpClient = new SmtpClient(
                Configuration["MailService:Host"],
                int.Parse(Configuration["MailService:Port"]))
            {
                Credentials = new NetworkCredential(
                    Configuration["MailService:UserName"],
                    Configuration["MailService:Password"]),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);
        }
        catch(Exception ex)
        {
            throw;
        }
    }
}
