namespace HealthCare.DataLayer.Interfaces
{
    using Utilities.Enums;

    public interface IInformation
    {
        string Name { get; set; }

        int UserId { get; set; }

        Gender Gender { get; set; }
    }
}