using System;
using System.Linq.Expressions;
using DiabloCms.Entities.Models;
using DiabloCms.UseCases.Base;

namespace DiabloCms.UseCases.Services.Products.Specifications
{
    public class ProductByNameSpecification : Specification<Product>
    {
        private readonly string _name;

        public ProductByNameSpecification(string name)
        {
            _name = name;
        }

        protected override bool Include => _name != null;

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.Name.ToLower().Contains(_name.ToLower()) ||
                              product.Description.ToLower().Contains(_name.ToLower());
        }
    }
}