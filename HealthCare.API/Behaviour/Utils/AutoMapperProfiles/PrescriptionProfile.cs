namespace HealthCare.API.Behaviour.Utils.AutoMapperProfiles
{
    using AutoMapper;
    using Contracts.Models.Prescription.Data;
    using Contracts.Models.Prescription.Responses;
    using DataLayer.Entities.MedicalData;

    public class PrescriptionProfile : Profile
    {
        public PrescriptionProfile()
        {
            CreateMap<PrescriptionData, Prescription>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<Prescription, ExtendedPrescriptionData>()
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.MedicalManInfo.Name))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreateDate.ToShortDateString()))
                .ForMember(dest => dest.DoctorSpecialty, opt => opt.MapFrom(src => src.Doctor.MedicalManInfo.Specialties[0].Specialty.Name))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));
        }
    }
}