using System.ComponentModel.DataAnnotations;
using DiabloCms.Shared.ConstContent;

namespace DiabloCms.Models.RequestModel.Categories
{
    using static ErrorMessages;
    using static ModelConstants.Common;

    public class CategoryRequastModel
    {
        [Required]
        [StringLength(
            NameLength,
            ErrorMessage = StringLengthErrorMessage,
            MinimumLength = MinNameLength)]
        public string Name { get; set; }

        [MaxLength(NameLength)] public string ParentCategoryName { get; set; }

        public bool ShowInFilter { get; set; }
    }
}