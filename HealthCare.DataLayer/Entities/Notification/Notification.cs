namespace HealthCare.DataLayer.Entities.Notification
{
    using System;
    using System.Collections.Generic;

    using Base;
    using Utilities.Enums;

    public class Notification : SystemData
    {
        public NotificationReason ReasonType { get; set; }

        public DateTime NotificationDate { get; set; }

        public List<NotificationUser> UserNotifications { get; set; } = new List<NotificationUser>();
    }
}
