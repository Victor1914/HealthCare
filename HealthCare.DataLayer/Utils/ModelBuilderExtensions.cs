namespace HealthCare.DataLayer.Utils
{
    using Microsoft.EntityFrameworkCore;

    using Entities.MedicalData;
    using Entities.MedicalMan;
    using Entities.UserAccount.Contacts;
    using Entities.Notification;
    using HealthCare.Interfaces;

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder, IDataRetriever dataRetriever)
        {
            modelBuilder.Entity<Area>().HasData(dataRetriever.RetrieveData<Area>());

            modelBuilder.Entity<City>().HasData(dataRetriever.RetrieveData<City>());

            modelBuilder.Entity<Country>().HasData(dataRetriever.RetrieveData<Country>());

            modelBuilder.Entity<IllnessCategory>().HasData(dataRetriever.RetrieveData<IllnessCategory>());

            modelBuilder.Entity<IllnessType>().HasData(dataRetriever.RetrieveData<IllnessType>());

            modelBuilder.Entity<MedicamentCategory>().HasData(dataRetriever.RetrieveData<MedicamentCategory>());

            modelBuilder.Entity<Specialty>().HasData(dataRetriever.RetrieveData<Specialty>());

            modelBuilder.Entity<MedicalTestType>().HasData(dataRetriever.RetrieveData<MedicalTestType>());

            modelBuilder.Entity<NotificationType>().HasData(dataRetriever.RetrieveData<NotificationType>());
        }
    }
}
