using System.ComponentModel.DataAnnotations;

namespace Identity.API.Model.AccountViewModels
{
    public record ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }
    }
}
