using System;

namespace DiabloCms.Entities.Models
{
    public class PhotoUrl
    {
        public Guid Id { get; set; }

        public Guid FilesId { get; set; }
        public Files Files { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}