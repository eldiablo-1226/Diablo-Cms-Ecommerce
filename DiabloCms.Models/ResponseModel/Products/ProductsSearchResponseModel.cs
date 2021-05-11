using System.Collections.Generic;

namespace DiabloCms.Models.ResponseModel.Products
{
    public class ProductsSearchResponseModel
    {
        public IEnumerable<ProductsListingResponseModel> Products { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }
    }
}