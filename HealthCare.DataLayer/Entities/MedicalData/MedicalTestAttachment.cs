namespace HealthCare.DataLayer.Entities.MedicalData
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Base;

    public class MedicalTestAttachment : SystemData
    {
        public string Url { get; set; }

        public int MedicalTestId { get; set; }

        [ForeignKey("MedicalTestId")]
        public MedicalTest MedicalTest { get; set; }
    }
}
