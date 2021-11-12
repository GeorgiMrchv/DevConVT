using DevConVT.Data;
using DevConVT.Data.Models;
using DevConVT.Umbraco.ViewModels;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using DevConVT.Data.Repository;

namespace DevConVT.Umbraco.Controllers
{
    public class EventRegistrationController : SurfaceController
    {
        DevConVTDbContext db = new DevConVTDbContext();

        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/";

        //register repository
        //private readonly IRepository _repo;

        //public EventRegistrationController(IRepository repo)
        //{
        //    _repo = repo;
        //}

        [HttpGet]
        public ActionResult RenderForm(EventRegistrationViewModel model)
        {
            //var reggedCount = _repo.Set<EventRegistration>().Count();

            model.EmailAddress = null;
            var registeredCount = db.EventRegistrations.Count();
            model.Count = registeredCount;

            return PartialView(PARTIAL_VIEW_FOLDER + "_EventRegistration.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmitForm(EventRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToCurrentUmbracoPage();
            }
            var registerCount = db.EventRegistrations.Count();

            if (registerCount > 5)
            {
                await SendEmail("Unsuccessful registration", "No emtpy seats left", model.EmailAddress);
            }
            else
            {
                await SendEmail("Registration completed!", "You have successfully registered for the upcoming event!", model.EmailAddress);


                var newParticipant = new EventRegistration
                {
                    FullName = model.FullName,
                    EmailAddress = model.EmailAddress
                };

                // _repo.Add(newParticipant);

                db.EventRegistrations.Add(newParticipant);
                db.SaveChanges();

            }
            return RedirectToCurrentUmbracoPage();
        }

        public async Task SendEmail(string subject, string body, string emailAddress)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("gmirchev@melontech.com"));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain)
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("gmirchev@melontech.com", "jbdhslgrdtydjpcv");
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
        }

        public void ExportExcell()
        {
            var data = db.EventRegistrations.ToList();

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("RegisteredParticipants");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=DevConVTPArticipants.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

        }
    }
}