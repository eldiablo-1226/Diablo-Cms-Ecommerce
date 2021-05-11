using System;
using System.ComponentModel.DataAnnotations;
using DiabloCms.Shared.ConstContent;

namespace DiabloCms.Models.RequestModel.CartIteam
{
    using static ModelConstants.Product;

    public class CartIteamRequestModel
    {
        [Required] public Guid ProductAttributeId { get; set; }

        [Range(MinQuantity, ModelConstants.Product.Quantity)]
        public int Quantity { get; set; } = MinQuantity;
    }
}