namespace HealthCare.API.Behaviour.Utils.AutoMapperProfiles
{
    using System;
    using AutoMapper;

    using Contracts.Models.Common;
    using Contracts.Models.PatientAccount.Data;
    using DataLayer.Entities.MedicalData;
    using DataLayer.Entities.Patient;
    using DataLayer.Entities.UserAccount;

    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<PatientGeneralData, PatientInfo>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<MedicalProfileGeneralData, MedicalProfile>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<PatientInfo, PatientInfoData>()
                .ForMember(dest => dest.PatientInfoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.PatientInfoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Address, 
                   opt => opt.MapFrom(src => $"{src.Patient.UserContact.Addresses[0].StreetAddress}, {src.Patient.UserContact.Addresses[0].City.Name}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Patient.UserContact.Emails[0].EmailAddress))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Patient.UserContact.Phones[0].Number))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.UtcNow.Year - src.BirthDate.Year))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<Appointment, PatientInfoData>()
                .ForMember(dest => dest.PatientInfoId, opt => opt.MapFrom(src => src.Patient.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Patient.MedicalProfile.PatientInfo.Name))
                .ForMember(dest => dest.Address, 
                    opt => opt.MapFrom(src => $"{src.Patient.UserContact.Addresses[0].StreetAddress}, {src.Patient.UserContact.Addresses[0].City.Name}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Patient.UserContact.Emails[0].EmailAddress))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Patient.UserContact.Phones[0].Number))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Patient.MedicalProfile.PatientInfo.Gender))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));
        }
    }
}
