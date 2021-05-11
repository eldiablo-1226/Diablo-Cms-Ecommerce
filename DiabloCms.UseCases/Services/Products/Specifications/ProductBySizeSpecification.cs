using System;
using System.Linq;
using System.Linq.Expressions;
using DiabloCms.Entities.Models;
using DiabloCms.UseCases.Base;

namespace DiabloCms.UseCases.Services.Products.Specifications
{
    public class ProductBySizeSpecification : Specification<Product>
    {
        private readonly string _size;

        public ProductBySizeSpecification(string size)
        {
            _size = size;
        }

        protected override bool Include => _size != null;

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.ProductAttributes.Any(p => p.Size == _size);
        }
    }
}