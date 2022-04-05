namespace HealthCare.API.Behaviour.Utils.AutoMapperProfiles
{
    using AutoMapper;

    using Contracts.Models.UserAccount.Requests;
    using Contracts.Models.UserAccount.Responses;
    using DataLayer.Entities.UserAccount;

    public class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap<RegisterUserRequest, User>();

            CreateMap<User, RetrieveDoctorsResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.UserContact));
        }
    }
}
