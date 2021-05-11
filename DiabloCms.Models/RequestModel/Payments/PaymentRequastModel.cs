using System.ComponentModel.DataAnnotations;
using DiabloCms.Shared.ConstContent;

namespace DiabloCms.Models.RequestModel.Payments
{
    using static ModelConstants.Common;
    using static ModelConstants.Product;

    public class PaymentRequastModel
    {
        [Required] [MaxLength(NameLength)] public string Name { get; set; }

        [Required] [MaxLength(UrlLength)] public string Logo { get; set; }

        [Required] [MaxLength(NameLength)] public string NormalizeName { get; set; }

        public float Percentage { get; set; }
    }
}