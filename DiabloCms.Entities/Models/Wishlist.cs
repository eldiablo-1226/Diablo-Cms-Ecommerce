using System;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class Wishlist : BaseModel
    {
        public string UserId { get; set; }
        public CmsUser User { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}