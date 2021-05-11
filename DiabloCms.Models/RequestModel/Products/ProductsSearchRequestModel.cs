using System;

namespace DiabloCms.Models.RequestModel.Products
{
    public class ProductsSearchRequestModel
    {
        public string Query { get; set; } = "";

        public Guid? Category { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? Price { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public int Page { get; set; } = 1;
    }
}