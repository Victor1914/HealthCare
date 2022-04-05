namespace HealthCare.DataLayer.Configurations
{
    using Entities.Notification;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class NotificationTypeUserConfig : IEntityTypeConfiguration<NotificationTypeUser>
    {
        public void Configure(EntityTypeBuilder<NotificationTypeUser> modelBuilder)
        {
            modelBuilder
                .HasOne(e => e.NotificationType)
                .WithMany(e => e.NotificationTypeUsers)
                .HasForeignKey(e => e.NotificationTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .HasOne(e => e.User)
                .WithMany(e => e.NotificationTypeUsers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
