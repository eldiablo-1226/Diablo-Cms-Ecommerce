using System.Collections.Generic;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class Category : BaseDeletableModel
    {
        public string Name { get; set; }
        public string ParentCategoryName { get; set; }
        public bool ShowInFilter { get; set; }
        public ICollection<Product> Products { get; } = new HashSet<Product>();
    }
}