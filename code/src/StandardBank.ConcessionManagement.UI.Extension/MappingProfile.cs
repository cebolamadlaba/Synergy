using AutoMapper;

namespace StandardBank.ConcessionManagement.UI.Extension
{
    /// <summary>
    /// Mapping profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            //Centre
            CreateMap<Model.Repository.Centre, Model.UserInterface.Centre>()
                .ForMember(target => target.Name, _ => _.MapFrom(source => source.CentreName));
            CreateMap<Model.UserInterface.Centre, Model.Repository.Centre>()
                .ForMember(target => target.CentreName, _ => _.MapFrom(source => source.Name));

            //Concession
            CreateMap<Model.Repository.Concession, Model.UserInterface.Concession>()
                .ForMember(target => target.ReferenceNumber, _ => _.MapFrom(source => source.ConcessionRef))
                .ForMember(target => target.DateOpened, _ => _.MapFrom(source => source.ConcessionDate))
                .ForMember(target => target.DateSentForApproval, _ => _.MapFrom(source => source.DatesentForApproval));
            CreateMap<Model.UserInterface.Concession, Model.Repository.Concession>()
                .ForMember(target => target.ConcessionRef, _ => _.MapFrom(source => source.ReferenceNumber))
                .ForMember(target => target.ConcessionDate, _ => _.MapFrom(source => source.DateOpened))
                .ForMember(target => target.DatesentForApproval, _ => _.MapFrom(source => source.DateSentForApproval));

            //ConcessionCondition
            CreateMap<Model.Repository.ConcessionCondition, Model.UserInterface.ConcessionCondition>()
                .ForMember(target => target.ConditionVolume, _ => _.MapFrom(source => source.Volume))
                .ForMember(target => target.ConditionValue, _ => _.MapFrom(source => source.Value));
            CreateMap<Model.UserInterface.ConcessionCondition, Model.Repository.ConcessionCondition>()
                .ForMember(target => target.Volume, _ => _.MapFrom(source => source.ConditionVolume))
                .ForMember(target => target.Value, _ => _.MapFrom(source => source.ConditionValue));

            //ConcessionType
            CreateMap<Model.Repository.ConcessionType, Model.UserInterface.ConcessionType>().ReverseMap();
           
            //ConditionType
            CreateMap<Model.Repository.ConditionType, Model.UserInterface.ConditionType>().ReverseMap();
          
            //Period
            CreateMap<Model.Repository.Period, Model.UserInterface.Period>().ReverseMap();
          
            //PeriodType
            CreateMap<Model.Repository.PeriodType, Model.UserInterface.PeriodType>().ReverseMap();
          
            //ProductType
            CreateMap<Model.Repository.Product, Model.UserInterface.ProductType>().ReverseMap();
        
            //Region
            CreateMap<Model.Repository.Region, Model.UserInterface.Region>().ReverseMap();
           
            //ReviewFeeType
            CreateMap<Model.Repository.ReviewFeeType, Model.UserInterface.ReviewFeeType>().ReverseMap();
     
            //RiskGroup
            CreateMap<Model.Repository.RiskGroup, Model.UserInterface.Pricing.RiskGroup>()
                .ForMember(target => target.Name, _ => _.MapFrom(source => source.RiskGroupName))
                .ForMember(target => target.Number, _ => _.MapFrom(source => source.RiskGroupNumber));
            CreateMap<Model.UserInterface.Pricing.RiskGroup, Model.Repository.RiskGroup>()
                .ForMember(target => target.RiskGroupName, _ => _.MapFrom(source => source.Name))
                .ForMember(target => target.RiskGroupNumber, _ => _.MapFrom(source => source.Number));

            //Role
            CreateMap<Model.Repository.Role, Model.UserInterface.Role>()
                .ForMember(target => target.Name, _ => _.MapFrom(source => source.RoleName))
                .ForMember(target => target.Description, _ => _.MapFrom(source => source.RoleDescription));
            CreateMap<Model.UserInterface.Role, Model.Repository.Role>()
                .ForMember(target => target.RoleName, _ => _.MapFrom(source => source.Name))
                .ForMember(target => target.RoleDescription, _ => _.MapFrom(source => source.Description));

            //User 
            CreateMap<Model.Repository.User, Model.UserInterface.User>().ReverseMap();
        }
    }
}
