namespace HealthCare.BusinessLayer.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using DataLayer.Entities.MedicalCenter;
    using DataLayer.Entities.MedicalMan;
    using DataLayer.Entities.Patient;
    using DataLayer.Entities.UserAccount;
    using DataLayer.Interfaces;
    using Microsoft.EntityFrameworkCore;

    using Utilities.Enums;

    public static class CommonInfoExtensions
    {
        public static void AddSpecialties(this MedicalManInfo dbModel,
            IEnumerable<int> specialtiesInRequest,
            List<MedicalManSpecialty> currentSpecialties,
            int medicalManInfoId)
        {
            foreach (var specialty in specialtiesInRequest)
            {
                if (currentSpecialties.Any(x => x.SpecialtyId == specialty))
                {
                    currentSpecialties.RemoveAll(x => x.SpecialtyId == specialty);
                    continue;
                }

                dbModel.Specialties.Add(
                    new MedicalManSpecialty
                    {
                        MedManInfoId = medicalManInfoId,
                        SpecialtyId = specialty
                    });
            }

            foreach (var currentSpec in currentSpecialties)
                currentSpec.IsDeleted = true;
        }

        public static void AddMedicalCenters(this User dbModel,
            IEnumerable<int> medicalCenterInRequest,
            List<MedicalCenterDoctor> currentMedicalCenters,
            int doctorId)
        {
            foreach (var medicalCenter in medicalCenterInRequest)
            {
                if (currentMedicalCenters.Any(x => x.Id == medicalCenter))
                {
                    currentMedicalCenters.RemoveAll(x => x.Id == medicalCenter);
                    continue;
                }
                
                dbModel.DoctorWorkPlaces.Add(
                    new MedicalCenterDoctor
                    {
                        MedicalCenterId = medicalCenter,
                        DoctorId = doctorId
                    });
            }

            foreach (var currentCenter in currentMedicalCenters)
                currentCenter.IsDeleted = true;
        }

        public static IInformation AddInfoModel(this DbContext dbContext, int userId, string name, RoleType userRoleType, Gender gender = Gender.None)
        {
            switch (userRoleType)
            {
                case RoleType.Patient:
                    {
                        var model = dbContext.CreateInfoObject<PatientInfo>(name, userId, gender);
                        return model;
                    }
                case RoleType.Doctor:
                    {
                        var model = dbContext.CreateInfoObject<MedicalManInfo>(name, userId, gender);
                        return model;
                    }
                case RoleType.MedicalCenter:
                    {
                        var model = dbContext.CreateInfoObject<MedicalCenterInfo>(name, userId, gender);
                        return model;
                    }
                case RoleType.Admin:
                    return null;
            }

            return null;
        }
    }
}
