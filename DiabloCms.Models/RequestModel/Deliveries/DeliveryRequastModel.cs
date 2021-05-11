using System.ComponentModel.DataAnnotations;
using DiabloCms.Shared.ConstContent;

namespace DiabloCms.Models.RequestModel.Deliveries
{
    using static ModelConstants.Common;
    using static ModelConstants.Product;

    public class DeliveryRequastModel
    {
        [Required] [MaxLength(NameLength)] public string Name { get; set; }

        [Required] [MaxLength(UrlLength)] public string Logo { get; set; }

        public decimal Price { get; set; }
    }
}