using System.Collections.Generic;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class Files : BaseDeletableModel
    {
        public string Url { get; set; }

        public ICollection<PhotoUrl> PhotoUrl { get; set; } = new HashSet<PhotoUrl>();
    }
}