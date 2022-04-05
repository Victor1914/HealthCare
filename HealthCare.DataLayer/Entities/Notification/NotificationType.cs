namespace HealthCare.DataLayer.Entities.Notification
{
    using System.Collections.Generic;

    using Base;

    public class NotificationType : SystemData
    {
        public string Name { get; set; }

        public List<NotificationTypeUser> NotificationTypeUsers { get; set; } = new List<NotificationTypeUser>();
    }
}
