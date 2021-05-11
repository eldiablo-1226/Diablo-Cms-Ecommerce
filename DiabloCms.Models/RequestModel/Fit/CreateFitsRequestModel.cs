using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DiabloCms.Shared.ConstContent;

namespace DiabloCms.Models.RequestModel.Fit
{
    public class CreateFitsRequestModel
    {
        [Required]
        [MinLength(ModelConstants.Common.MinNameLength)]
        public string Name { get; set; }

        public string Photo { get; set; }

        [Required] public IEnumerable<Guid> ProductId { get; set; }
    }
}