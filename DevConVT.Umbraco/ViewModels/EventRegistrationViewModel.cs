using System.ComponentModel.DataAnnotations;

namespace DevConVT.Umbraco.ViewModels
{
    public class EventRegistrationViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        
        [Required(ErrorMessage = "Please, enter a valid email address!")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public int Count { get; set; }

    }
}