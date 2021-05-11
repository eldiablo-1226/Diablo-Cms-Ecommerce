using System.Collections.Generic;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class Address : BaseModel
    {
        public string UserId { get; set; }
        public CmsUser User { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; } = new HashSet<Order>();
    }
}