namespace HealthCare.DataLayer.Configurations
{
    using Entities.MedicalData;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OutpatientCardConfig : IEntityTypeConfiguration<OutpatientCard>
    {
        public void Configure(EntityTypeBuilder<OutpatientCard> modelBuilder)
        {
            modelBuilder
                .HasOne(e => e.Doctor)
                .WithMany(e => e.DoctorsOutpatientCards)
                .HasForeignKey(e => e.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .HasOne(e => e.Patient)
                .WithMany(e => e.PatientOutpatientCards)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .HasOne(e => e.MedCenter)
                .WithMany(e => e.MedicalCenterOutpatientCards)
                .HasForeignKey(e => e.MedCenterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
