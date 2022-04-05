namespace HealthCare.Contracts.Models.OutpatientCard.Data
{
    using System;

    public class SickLeaveData
    {
        public long DocumentNumber { get; set; }

        public int DaysCount { get; set; }

        public string MKBCode { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
