using System.Collections.Generic;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class Delivery : BaseModel
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public decimal Price { get; set; }

        public ICollection<Order> Orders { get; } = new HashSet<Order>();
    }
}