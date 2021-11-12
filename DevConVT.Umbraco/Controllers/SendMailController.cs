using DevConVT.Umbraco.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace DevConVT.Umbraco.Controllers
{
    public class SendMailController : SurfaceController
    {
        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/";
        public ActionResult RenderForm()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Contact.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmitForm(SendMailViewModel model)
        {
            if (ModelState.IsValid)
            {
                await SendMail(model);
                return RedirectToCurrentUmbracoPage();
            }

            return CurrentUmbracoPage();
        }

        public async Task SendMail(SendMailViewModel model)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("gmirchev@melontech.com"));
            email.To.Add(MailboxAddress.Parse("geomirchev@gmail.com"));
            email.Subject = model.Subject;
            email.Body = new TextPart(TextFormat.Plain)
            {
                Text = model.FullName + "," + System.Environment.NewLine +
                       model.EmailAddress + "," + System.Environment.NewLine +
                       model.Message
            };

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