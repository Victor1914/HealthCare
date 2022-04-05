namespace HealthCare.DataLayer.Entities.MedicalData
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Base;

    public class IllnessType : SystemData
    {
        public string Name { get; set; }

        public int IllnessCategoryId { get; set; }

        [ForeignKey("IllnessCategoryId")]
        public IllnessCategory IllnessCategory { get; set; }

        public string OtherName { get; set; }

        public string MkbCode { get; set; } = "A00-B99";
    }
}
