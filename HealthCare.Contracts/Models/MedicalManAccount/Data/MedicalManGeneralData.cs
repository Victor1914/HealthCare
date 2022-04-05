namespace HealthCare.Contracts.Models.MedicalManAccount.Data
{
    using System.ComponentModel.DataAnnotations;
    using HealthCare.Interfaces;
    using Utilities.Enums;

    public class MedicalManGeneralData : IName
    {
        public long IdentityNumber { get;set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public string BirthDate { get; set; }
    }
}
