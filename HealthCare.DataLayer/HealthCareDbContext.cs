namespace HealthCare.DataLayer
{
    using Microsoft.EntityFrameworkCore;

    using Configurations;
    using Entities.MedicalCenter;
    using Entities.MedicalData;
    using Entities.MedicalMan;
    using Entities.Notification;
    using Entities.OutpatientCardData;
    using Entities.Patient;
    using Entities.UserAccount;
    using Entities.UserAccount.Contacts;
    using HealthCare.Interfaces;
    using Utils;

    public class HealthCareDbContext : DbContext
    {
        private readonly IDataRetriever _dataRetriever;

        public HealthCareDbContext(IDataRetriever dataRetriever)
        {
            _dataRetriever = dataRetriever;
        }

        public HealthCareDbContext(DbContextOptions options, IDataRetriever dataRetriever) : base(options)
        {
            _dataRetriever = dataRetriever;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<UserContact> UserContacts { get; set; }

        public DbSet<Email> Emails { get; set; }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<NotificationUser> NotificationUsers { get; set; }

        public DbSet<NotificationTypeUser> NotificationTypeUsers { get; set; }

        public DbSet<NotificationType> NotificationTypes { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<AppointmentHours> AppointmentHours { get; set; }

        public DbSet<MedicalManInfo> MedicalManInfos { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<MedicalManSpecialty> MedicalManSpecialties { get; set; }

        public DbSet<MedicalCenterInfo> MedicalCenterInfos { get; set; }

        public DbSet<PatientInfo> PatientInfos { get; set; }

        public DbSet<MedicalProfile> MedicalProfiles { get; set; }

        public DbSet<OutpatientCard> OutpatientCards { get; set; }

        public DbSet<ConsultationDocument> ConsultationDocuments { get; set; }

        public DbSet<ConsultationDocumentMMD> ConsultationDocumentsMMD { get; set; }

        public DbSet<ConsultationDocumentKP> ConsultationDocumentsKP { get; set; }
       
        public DbSet<SickLeave> SickLeaves { get; set; }

        public DbSet<MedicalTest> MedicalTests { get; set; }

        public DbSet<MedicalTestAttachment> MedicalTestAttachments { get; set; }

        public DbSet<MedicalTestType> MedicalTestTypes { get; set; }

        public DbSet<IllnessType> IllnessTypes { get; set; }

        public DbSet<IllnessCategory> IllnessCategories { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<MedicalCenterDoctor> MedicalCenterDoctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new AppointmentConfig());

            modelBuilder.ApplyConfiguration(new MedicalCenterDoctorConfig());

            modelBuilder.ApplyConfiguration(new MedicalManSpecialtyConfig());

            modelBuilder.ApplyConfiguration(new NotificationTypeUserConfig());

            modelBuilder.ApplyConfiguration(new NotificationUserConfig());

            modelBuilder.ApplyConfiguration(new OutpatientCardConfig());

            modelBuilder.ApplyConfiguration(new PrescriptionConfig());
            
            modelBuilder.ApplyConfiguration(new AppointmentHoursConfig());

            modelBuilder.Seed(_dataRetriever);
        }
    }
}
