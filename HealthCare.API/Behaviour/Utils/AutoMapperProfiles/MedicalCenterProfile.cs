namespace HealthCare.API.Behaviour.Utils.AutoMapperProfiles
{
    using AutoMapper;
    using Contracts.Models.MedicalCenterAccount.Data;
    using DataLayer.Entities.MedicalCenter;

    public class MedicalCenterProfile : Profile
    {
        public MedicalCenterProfile()
        {
            CreateMap<MedicalCenterData, MedicalCenterInfo>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(usr => usr.Id))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<MedicalCenterInfo, MedicalCenterData>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(usr => usr.UserId))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));
        }
    }
}
