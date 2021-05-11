﻿using System;

namespace DiabloCms.Entities.Contracts
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}