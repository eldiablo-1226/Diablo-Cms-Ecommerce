using System;
using System.Linq;
using System.Linq.Expressions;
using DiabloCms.Entities.Models;
using DiabloCms.UseCases.Base;

namespace DiabloCms.UseCases.Services.Products.Specifications
{
    public class ProductByColorSpecification : Specification<Product>
    {
        private readonly string _color;

        public ProductByColorSpecification(string color)
        {
            _color = color;
        }

        protected override bool Include => _color != null;

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.ProductAttributes.Any(p => p.Color == _color);
        }
    }
}