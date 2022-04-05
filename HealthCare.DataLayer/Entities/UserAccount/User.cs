namespace HealthCare.DataLayer.Entities.UserAccount
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
   
    using Base;
    using Contacts;
    using MedicalCenter;
    using MedicalData;
    using MedicalMan;
    using Notification;
    using Utilities.Enums;

    public class User : SystemData
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Secret { get; set; }

        public RoleType RoleType { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public User Parent { get; set; }

        public UserContact UserContact { get; set; }

        public MedicalManInfo MedicalManInfo { get; set; }

        public MedicalProfile MedicalProfile { get; set; }

        public MedicalCenterInfo MedicalCenterInfo { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();

        public List<NotificationUser> UserNotifications { get; set; } = new List<NotificationUser>();

        public List<NotificationTypeUser> NotificationTypeUsers { get; set; } = new List<NotificationTypeUser>();

        [InverseProperty("Doctor")]
        public List<OutpatientCard> DoctorsOutpatientCards { get; set; } = new List<OutpatientCard>();

        [InverseProperty("Patient")]
        public List<OutpatientCard> PatientOutpatientCards { get; set; } = new List<OutpatientCard>();

        [InverseProperty("MedCenter")]
        public List<OutpatientCard> MedicalCenterOutpatientCards { get; set; } = new List<OutpatientCard>();

        [InverseProperty("Doctor")]
        public List<Appointment> DoctorAppointments { get; set; } = new List<Appointment>();

        [InverseProperty("Patient")]
        public List<Appointment> PatientAppointments { get; set; } = new List<Appointment>();

        [InverseProperty("Doctor")]
        public List<MedicalCenterDoctor> DoctorWorkPlaces { get; set; } = new List<MedicalCenterDoctor>();

        [InverseProperty("MedicalCenter")]
        public List<MedicalCenterDoctor> MedicalCenterEmployees { get; set; } = new List<MedicalCenterDoctor>();

        [InverseProperty("Doctor")]
        public List<Prescription> DoctorsPrescriptions { get; set; } = new List<Prescription>();

        [InverseProperty("Patient")]
        public List<Prescription> PatientPrescriptions { get; set; } = new List<Prescription>();
    }
}
