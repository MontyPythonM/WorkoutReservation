﻿namespace WorkoutReservation.Domain.Common
{
    public abstract class AuditableEntityBase
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
