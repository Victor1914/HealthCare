namespace HealthCare.DataLayer.Configurations
{
    using Entities.MedicalMan;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AppointmentHoursConfig : IEntityTypeConfiguration<AppointmentHours>
    {
        public void Configure(EntityTypeBuilder<AppointmentHours> modelBuilder)
        {
            modelBuilder
                .HasOne(e => e.MedicalCenterInfo)
                .WithMany(e => e.AppointmentHours)
                .HasForeignKey(e => e.MedicalCenterInfoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
