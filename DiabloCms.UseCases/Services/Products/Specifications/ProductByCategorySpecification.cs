using System;
using System.Linq.Expressions;
using DiabloCms.Entities.Models;
using DiabloCms.UseCases.Base;

namespace DiabloCms.UseCases.Services.Products.Specifications
{
    public class ProductByCategorySpecification : Specification<Product>
    {
        private readonly Guid? _id;

        public ProductByCategorySpecification(Guid? id)
        {
            _id = id;
        }

        protected override bool Include => _id != null;

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.CategoryId == _id;
        }
    }
}