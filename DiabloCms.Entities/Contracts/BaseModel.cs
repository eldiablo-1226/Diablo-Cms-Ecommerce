using System;

namespace DiabloCms.Entities.Contracts
{
    public abstract class BaseModel : IAuditInfo
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}