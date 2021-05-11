using System.Collections.Generic;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class Fit : BaseModel
    {
        public string Name { get; set; }
        public string Photo { get; set; }

        public ICollection<FitItem> FitItems { get; set; } = new HashSet<FitItem>();
    }
}