using System.ComponentModel.DataAnnotations;
using DiabloCms.Shared.ConstContent;

namespace DiabloCms.Models.RequestModel.Products
{
    using static ErrorMessages;
    using static ModelConstants.Common;
    using static ModelConstants.Product;

    public class UpdateProductRequestModel
    {
        [Required]
        [StringLength(
            NameLength,
            ErrorMessage = StringLengthErrorMessage,
            MinimumLength = MinNameLength)]
        public string Name { get; set; }

        [MaxLength(DescriptionLength)] public string Description { get; set; }

        [MaxLength(MinNameLength)] public string Article { get; set; }

        public bool IsActive { get; set; }
        public bool IsSingleAttribute { get; set; }

        [MaxLength(UrlLength)] public string VideoUrl { get; set; }

        [Required] public decimal Price { get; set; }

        [Required] public string CategoryId { get; set; }
    }
}