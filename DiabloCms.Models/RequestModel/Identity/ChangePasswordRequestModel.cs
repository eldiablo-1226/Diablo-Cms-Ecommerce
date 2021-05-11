using System.ComponentModel.DataAnnotations;
using DiabloCms.Shared.ConstContent;

namespace DiabloCms.Models.RequestModel.Identity
{
    using static ModelConstants.Identity;

    public class ChangePasswordRequestModel
    {
        [Required]
        [MinLength(MinPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(MinPasswordLength)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [MinLength(MinPasswordLength)]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
    }
}