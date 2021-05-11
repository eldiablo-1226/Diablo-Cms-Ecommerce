using System;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class CartItem : BaseDeletableModel
    {
        public string UserId { get; set; }
        public CmsUser User { get; set; }

        public Guid ProductAttributeId { get; set; }
        public ProductAttribute ProductAttribute { get; set; }

        public int Quantity { get; set; }
    }
}