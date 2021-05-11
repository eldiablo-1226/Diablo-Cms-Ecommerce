using System.ComponentModel.DataAnnotations;
using DiabloCms.Shared.ConstContent;

namespace DiabloCms.Models.RequestModel.Fit
{
    public class UpdateFitsRequestModel
    {
        [Required]
        [MinLength(ModelConstants.Common.MinNameLength)]
        public string Name { get; set; }

        public string Photo { get; set; }
    }
}