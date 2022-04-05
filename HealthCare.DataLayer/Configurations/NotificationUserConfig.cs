namespace HealthCare.DataLayer.Configurations
{
    using Entities.Notification;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class NotificationUserConfig : IEntityTypeConfiguration<NotificationUser>
    {
        public void Configure(EntityTypeBuilder<NotificationUser> modelBuilder)
        {
            modelBuilder
                .HasOne(e => e.Notification)
                .WithMany(e => e.UserNotifications)
                .HasForeignKey(e => e.NotificationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .HasOne(e => e.User)
                .WithMany(e => e.UserNotifications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
