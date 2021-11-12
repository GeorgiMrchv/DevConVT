using System.ComponentModel.DataAnnotations;

namespace DevConVT.Umbraco.ViewModels
{
    public class SendMailViewModel
    {
       
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email Address is required!")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}