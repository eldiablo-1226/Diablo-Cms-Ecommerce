using System.ComponentModel.DataAnnotations;

namespace DiabloCms.Models.RequestModel.Products
{
    public class UpdateProductAttributeRequestModel
    {
        [Required] public string Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Photo { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
    }
}