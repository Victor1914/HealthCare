namespace HealthCare.DataLayer.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Entities.MedicalMan;

    public class MedicalManSpecialtyConfig : IEntityTypeConfiguration<MedicalManSpecialty>
    {
        public void Configure(EntityTypeBuilder<MedicalManSpecialty> modelBuilder)
        {
            modelBuilder
                .HasOne(e => e.MedManInfo)
                .WithMany(e => e.Specialties)
                .HasForeignKey(e => e.MedManInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .HasOne(e => e.Specialty)
                .WithMany(e => e.MedicalMan)
                .HasForeignKey(e => e.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
