using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.Products
{
    public class AttributeResponseModel : IMapFrom<ProductAttribute>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Photo { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
    }
}