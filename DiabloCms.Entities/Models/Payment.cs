using System.Collections.Generic;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class Payment : BaseModel
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string NormalizeName { get; set; }
        public float Percentage { get; set; }

        public ICollection<Order> Orders { get; } = new HashSet<Order>();
    }
}