namespace HealthCare.DataLayer.Entities.Notification
{
    using Base;
    using UserAccount;

    public class NotificationTypeUser : SystemData
    {
        public int NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
