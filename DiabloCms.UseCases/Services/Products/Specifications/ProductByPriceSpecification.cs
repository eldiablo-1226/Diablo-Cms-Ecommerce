using System;
using System.Linq.Expressions;
using DiabloCms.Entities.Models;
using DiabloCms.UseCases.Base;

namespace DiabloCms.UseCases.Services.Products.Specifications
{
    public class ProductByPriceSpecification : Specification<Product>
    {
        private readonly decimal _maxPrice;
        private readonly decimal _minPrice;

        internal ProductByPriceSpecification(decimal? minPrice, decimal? maxPrice)
        {
            _minPrice = minPrice ?? default;
            _maxPrice = maxPrice ?? decimal.MaxValue;
        }

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => _minPrice < product.Price && _maxPrice > product.Price;
        }
    }
}