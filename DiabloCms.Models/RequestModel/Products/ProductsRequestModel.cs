using System;
using System.ComponentModel.DataAnnotations;
using DiabloCms.Shared.ConstContent;

namespace DiabloCms.Models.RequestModel.Products
{
    using static ErrorMessages;
    using static ModelConstants.Common;
    using static ModelConstants.Product;

    public class ProductsRequestModel
    {
        [Required]
        [StringLength(
            NameLength,
            ErrorMessage = StringLengthErrorMessage,
            MinimumLength = MinNameLength)]
        public string Name { get; set; }

        [MaxLength(DescriptionLength)] public string Description { get; set; }

        [MinLength(MinNameLength)] public string Article { get; set; }

        public bool IsActive { get; set; } = false;
        public bool IsSingleAttribute { get; set; } = true;

        [MaxLength(UrlLength)] public string VideoUrl { get; set; }

        [Required] public decimal Price { get; set; }

        [Required] public Guid CategoryId { get; set; }
    }
}