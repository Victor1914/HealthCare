namespace HealthCare.Contracts.Interfaces
{
    using System.Collections.Generic;
    using Models.CommonMedicalData;

    public interface ITreatment
    {
        List<TreatmentData> Treatments { get; set; }
    }
}