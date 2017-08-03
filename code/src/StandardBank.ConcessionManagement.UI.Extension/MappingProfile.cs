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

            //ConcessionType
            CreateMap<Model.Repository.ConcessionType, Model.UserInterface.ConcessionType>();
            CreateMap<Model.UserInterface.ConcessionType, Model.Repository.ConcessionType>();

            //ConditionType
            CreateMap<Model.Repository.ConditionType, Model.UserInterface.ConditionType>();
            CreateMap<Model.UserInterface.ConditionType, Model.Repository.ConditionType>();

            //Period
            CreateMap<Model.Repository.Period, Model.UserInterface.Period>();
            CreateMap<Model.UserInterface.Period, Model.Repository.Period>();

            //PeriodType
            CreateMap<Model.Repository.PeriodType, Model.UserInterface.PeriodType>();
            CreateMap<Model.UserInterface.PeriodType, Model.Repository.PeriodType>();

            //ProductType
            CreateMap<Model.Repository.Product, Model.UserInterface.ProductType>();
            CreateMap<Model.UserInterface.ProductType, Model.Repository.Product>();

            //Region
            CreateMap<Model.Repository.Region, Model.UserInterface.Region>();
            CreateMap<Model.UserInterface.Region, Model.Repository.Region>();

            //ReviewFeeType
            CreateMap<Model.Repository.ReviewFeeType, Model.UserInterface.ReviewFeeType>();
            CreateMap<Model.UserInterface.ReviewFeeType, Model.Repository.ReviewFeeType>();

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
            CreateMap<Model.Repository.User, Model.UserInterface.User>();
            CreateMap<Model.UserInterface.User, Model.Repository.User>();
        }
    }
}
