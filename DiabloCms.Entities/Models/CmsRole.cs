using System;
using DiabloCms.Entities.Contracts;
using Microsoft.AspNetCore.Identity;

namespace DiabloCms.Entities.Models
{
    public class CmsRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public CmsRole(string name) : base(name)
        {
            base.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}