namespace HealthCare.DataLayer.Entities.Base
{
    using System;

    public class SystemData : IdentityData
    {
        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime UpdateDate { get; set; }
    }
}
