using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiabloCms.Models.RequestModel.Products
{
    public class UpdateProductPhotosRequestModel
    {
        [Required] public IEnumerable<string> ImageSources { get; set; }
    }
}