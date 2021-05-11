using System;
using System.Collections.Generic;
using DiabloCms.Entities.Contracts;
using Microsoft.AspNetCore.Identity;

namespace DiabloCms.Entities.Models
{
    public class CmsUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public CmsUser()
        {
            base.Id = Guid.NewGuid().ToString();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Address> Addresses { get; } = new HashSet<Address>();

        public ICollection<Order> Orders { get; } = new HashSet<Order>();

        public ICollection<Wishlist> Wishlists { get; } = new HashSet<Wishlist>();

        public ICollection<CartItem> CartItems { get; } = new HashSet<CartItem>();

        #region Audit

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        #endregion Audit
    }
}