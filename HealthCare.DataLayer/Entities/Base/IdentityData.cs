namespace HealthCare.DataLayer.Entities.Base
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class IdentityData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
