namespace HealthCare.API.Behaviour.Utils.AutoMapperProfiles
{
    using AutoMapper;
    using Contracts.Models.Common;
    using Contracts.Models.UserAccount.Data;
    using DataLayer.Entities.UserAccount.Contacts;

    public class ContactsProfile : Profile
    {
        public ContactsProfile()
        {
            CreateMap<City, CityData>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<EmailData, Email>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<AddressData, Address>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<PhoneData, Phone>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<Email, EmailData>()
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<Phone, PhoneData>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<Address, AddressData>()
                .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => src.StreetAddress))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));
        }
    }
}
