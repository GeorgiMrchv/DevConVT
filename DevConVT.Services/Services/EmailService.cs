using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace DevConVT.Services.Services
{
    public class EmailService : IEmailService
    {
        public async void SendEmail() 
        {
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("gmirchev@melontech.com"));
            email.To.Add(MailboxAddress.Parse("geomirchev@gmail.com"));
            email.Subject = "Test Email Subject";
            email.Body = new TextPart(TextFormat.Plain) { Text = "Maraba Arkadash!" };

            // send email
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("gmirchev@melontech.com", "jbdhslgrdtydjpcv");
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
        }
    }
}