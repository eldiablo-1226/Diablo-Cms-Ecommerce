using System;

namespace DiabloCms.Entities.Models
{
    public class FitItem
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid FitId { get; set; }
        public Fit Fit { get; set; }
    }
}