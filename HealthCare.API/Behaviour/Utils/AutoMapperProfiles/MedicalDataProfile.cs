namespace HealthCare.API.Behaviour.Utils.AutoMapperProfiles
{
    using System.Linq;
    using AutoMapper;
    using Contracts.Models.Common;
    using Contracts.Models.CommonMedicalData;
    using Contracts.Models.OutpatientCard;
    using Contracts.Models.OutpatientCard.Data;
    using DataLayer.Entities.MedicalData;
    using DataLayer.Entities.OutpatientCardData;

    public class MedicalDataProfile : Profile
    {
        public MedicalDataProfile()
        {
            CreateMap<AttachmentData, MedicalTest>()
                .ForMember(dest => dest.MedicalTestAttachments, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<IllnessData, Illness>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<IllnessType, CommonMedicalData>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<IllnessCategory, CommonMedicalData>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember) => sourceMember != null));
            //#######
            CreateMap<OutpatientCard, OutPatientCardInfo>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.MedicalManInfo.Name))
                .ForMember(dest => dest.UIN, opt => opt.MapFrom(src => src.Doctor.MedicalManInfo.IdentityNumber))
                .ForMember(dest => dest.MedicalCenterId, opt => opt.MapFrom(src => src.MedCenterId))
                .ForMember(dest => dest.DoctorSpecialty, opt => opt.MapFrom(src => src.SpecialtyCode))
                .ForMember(dest => dest.MedicalDocuments, opt => opt.MapFrom(src => src.ConsultationDocuments.Where(doc => !doc.IsVSD)))
                .ForMember(dest => dest.MedicalDocumentsMMD, opt => opt.MapFrom(src => src.ConsultationDocumentsMMD))
                .ForMember(dest => dest.MedicalDocumentsVSD, opt => opt.MapFrom(src => src.ConsultationDocuments.Where(doc => doc.IsVSD)))
                .ForMember(dest => dest.MainDiagnose, opt => opt.MapFrom(src => src.OutpatientCardIllnesses.FirstOrDefault(card => card.IsMain)))
                .ForMember(dest => dest.OtherDiseases, opt => opt.MapFrom(src => src.OutpatientCardIllnesses.Where(card => !card.IsMain)))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<OutPatientCardInfo, OutpatientCard>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));
           //#########
            CreateMap<OutpatientCard, OutpatientCardBaseInfo>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<OutpatientCardBaseInfo, OutpatientCard>()
                .ForMember(dest => dest.MedCenterId, opt => opt.MapFrom(src => src.MedicalCenterId))
                .ForMember(dest => dest.SpecialtyCode, opt => opt.MapFrom(src => src.SpecialtyCode))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));
            //######
            CreateMap<ConsultationDocument, MedicalDocuments>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null && !source.IsVSD));

            CreateMap<MedicalDocuments, ConsultationDocument>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<ConsultationDocumentMMD, MedicalDocumentsMMD>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<MedicalDocumentsMMD, ConsultationDocumentMMD>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<ConsultationDocumentKP, MedicalDocumentsKP>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<MedicalDocumentsKP,ConsultationDocumentKP>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<ConsultationDocument, MedicalDocumentsVSD>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null && source.IsVSD));

            CreateMap<MedicalDocumentsVSD, ConsultationDocument>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null && source.IsVSD));

            CreateMap<Illness, OutPatientCardIllnessData>()       
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.IllnessType.IllnessCategoryId))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<OutPatientCardIllnessData, Illness>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));
            
            CreateMap<MainIllnessData, Illness>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<Illness, MainIllnessData>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.IllnessType.IllnessCategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.IllnessType.Name))
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));
            
            CreateMap<SickLeave, SickLeaveData>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            CreateMap<SickLeaveData, SickLeave>()
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => sourceMember != null));

            //CreateMap<MedicalTest, AttachmentData>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.))
            //    .ForAllMembers(opt => opt.Condition(
            //        (source, destination, sourceMember, destMember) => sourceMember != null));        
        }
    }
}
