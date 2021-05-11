using System.ComponentModel.DataAnnotations;
using DiabloCms.Shared.ConstContent;

namespace DiabloCms.Models.RequestModel.Identity
{
    using static ModelConstants.Identity;

    public class LoginRequestModel
    {
        [Required]
        [EmailAddress]
        [MinLength(MinEmailLength)]
        [MaxLength(EmailLength)]
        public string Email { get; set; }

        /*[MinLength(MinPhoneNumberLength)]
        [MaxLength(PhoneNumberLength)]
        [RegularExpression(PhoneNumberRegularExpression)]
        public string PhoneNumber { get; set; }*/

        [Required]
        [MinLength(MinPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}