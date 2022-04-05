namespace HealthCare.Contracts.Models.MedicalManAccount.Data
{
    using Common;

    public class StatisticsData : TokenData
    {
        public int PatientsCount { get; set; } = 50;

        public int PatientsCountForToday { get; set; } = 70;

        public int AppointmentsCountForToday { get; set; } = 100;
    }
}
