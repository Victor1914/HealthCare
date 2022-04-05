namespace HealthCare.Interfaces
{
    using System;

    public interface IContactIdentity
    {
        int Id { get; set; }

        int UserContactId { get; set; }

        DateTime UpdateDate { get; set; }
    }
}
