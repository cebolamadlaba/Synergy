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
            //AccrualType
            CreateMap<Model.Repository.AccrualType, Model.UserInterface.AccrualType>().ReverseMap();

            //Centre
            CreateMap<Model.Repository.Centre, Model.UserInterface.Centre>()
                .ForMember(target => target.Name, _ => _.MapFrom(source => source.CentreName));
            CreateMap<Model.UserInterface.Centre, Model.Repository.Centre>()
                .ForMember(target => target.CentreName, _ => _.MapFrom(source => source.Name));

            //ChannelType
            CreateMap<Model.Repository.ChannelType, Model.UserInterface.ChannelType>().ReverseMap();

            //Concession
            CreateMap<Model.Repository.Concession, Model.UserInterface.Concession>()
                .ForMember(target => target.ReferenceNumber, _ => _.MapFrom(source => source.ConcessionRef))
                .ForMember(target => target.DateOpened, _ => _.MapFrom(source => source.ConcessionDate))
                .ForMember(target => target.DateSentForApproval, _ => _.MapFrom(source => source.DatesentForApproval))
                .ForMember(target => target.Requestor , _ => _.Ignore())
                .ForMember(target => target.SmtDealNumber, _ => _.MapFrom(source => source.SMTDealNumber));
            CreateMap<Model.UserInterface.Concession, Model.Repository.Concession>()
                .ForMember(target => target.Requestor, _ => _.Ignore())
                .ForMember(target => target.ConcessionRef, _ => _.MapFrom(source => source.ReferenceNumber))
                .ForMember(target => target.ConcessionDate, _ => _.MapFrom(source => source.DateOpened))
                .ForMember(target => target.DatesentForApproval, _ => _.MapFrom(source => source.DateSentForApproval))
                .ForMember(target => target.SMTDealNumber, _ => _.MapFrom(source => source.SmtDealNumber));

            //ConcessionCash
            CreateMap<Model.Repository.ConcessionCash, Model.UserInterface.Cash.CashConcessionDetail>()
                .ForMember(target => target.Value, _ => _.MapFrom(source => source.CashValue))
                .ForMember(target => target.Volume, _ => _.MapFrom(source => source.CashVolume))
                .ForMember(target => target.CashConcessionDetailId, _ => _.MapFrom(source => source.Id));
            CreateMap<Model.UserInterface.Cash.CashConcessionDetail, Model.Repository.ConcessionCash>()
                .ForMember(target => target.CashValue, _ => _.MapFrom(source => source.Value))
                .ForMember(target => target.CashVolume, _ => _.MapFrom(source => source.Volume))
                .ForMember(target => target.Id, _ => _.MapFrom(source => source.CashConcessionDetailId));

            //ConcessionComment
            CreateMap<Model.Repository.ConcessionComment, Model.UserInterface.ConcessionComment>().ReverseMap();

            //ConcessionCondition
            CreateMap<Model.Repository.ConcessionCondition, Model.UserInterface.ConcessionCondition>()
                .ForMember(target => target.ConditionVolume, _ => _.MapFrom(source => source.Volume))
                .ForMember(target => target.ConditionValue, _ => _.MapFrom(source => source.Value))
                .ForMember(target => target.ConcessionConditionId, _ => _.MapFrom(source => source.Id));
            CreateMap<Model.UserInterface.ConcessionCondition, Model.Repository.ConcessionCondition>()
                .ForMember(target => target.Volume, _ => _.MapFrom(source => source.ConditionVolume))
                .ForMember(target => target.Value, _ => _.MapFrom(source => source.ConditionValue))
                .ForMember(target => target.Id, _ => _.MapFrom(source => source.ConcessionConditionId));

            //ConcessionLending
            CreateMap<Model.Repository.ConcessionLending, Model.UserInterface.Lending.LendingConcessionDetail>()
                .ForMember(target => target.MarginAgainstPrime, _ => _.MapFrom(source => source.MarginToPrime))
                .ForMember(target => target.LendingConcessionDetailId, _ => _.MapFrom(source => source.Id))
                .ForMember(target => target.ApprovedMap, _ => _.MapFrom(source => source.ApprovedMarginToPrime))
                .ForMember(target => target.LoadedMap, _ => _.MapFrom(source => source.LoadedMarginToPrime));
            CreateMap<Model.UserInterface.Lending.LendingConcessionDetail, Model.Repository.ConcessionLending>()
                .ForMember(target => target.MarginToPrime, _ => _.MapFrom(source => source.MarginAgainstPrime))
                .ForMember(target => target.Id, _ => _.MapFrom(source => source.LendingConcessionDetailId))
                .ForMember(target => target.ApprovedMarginToPrime, _ => _.MapFrom(source => source.ApprovedMap))
                .ForMember(target => target.LoadedMarginToPrime, _ => _.MapFrom(source => source.LoadedMap));

            //ConcessionRelationship
            CreateMap<Model.Repository.ConcessionRelationship, Model.UserInterface.ConcessionRelationship>().ReverseMap();

            //ConcessionRelationshipDetail
            CreateMap<Model.Repository.ConcessionRelationshipDetail, Model.UserInterface.ConcessionRelationshipDetail>().ReverseMap();

            //ConcessionTransactional
            CreateMap<Model.Repository.ConcessionTransactional, Model.UserInterface.Transactional.TransactionalConcessionDetail>()
                .ForMember(target => target.TransactionalConcessionDetailId, _ => _.MapFrom(source => source.Id))
                .ForMember(target => target.Value, _ => _.MapFrom(source => source.TransactionValue))
                .ForMember(target => target.Volume, _ => _.MapFrom(source => source.TransactionVolume));
            CreateMap<Model.UserInterface.Transactional.TransactionalConcessionDetail, Model.Repository.ConcessionTransactional>()
                .ForMember(target => target.Id, _ => _.MapFrom(source => source.TransactionalConcessionDetailId))
                .ForMember(target => target.TransactionValue, _ => _.MapFrom(source => source.Value))
                .ForMember(target => target.TransactionVolume, _ => _.MapFrom(source => source.Volume));

            //ConcessionType
            CreateMap<Model.Repository.ConcessionType, Model.UserInterface.ConcessionType>().ReverseMap();

            //Condition
            CreateMap<Model.Repository.Condition, Model.UserInterface.Condition>().ReverseMap();

            //ConditionProduct
            CreateMap<Model.Repository.ConditionProduct, Model.UserInterface.ConditionProduct>().ReverseMap();

            //ConditionType
            CreateMap<Model.Repository.ConditionType, Model.UserInterface.ConditionType>().ReverseMap();

            //FinancialCash
            CreateMap<Model.Repository.FinancialCash, Model.UserInterface.Cash.CashFinancial>().ReverseMap();

            //FinancialLending
            CreateMap<Model.Repository.FinancialLending, Model.UserInterface.Lending.LendingFinancial>().ReverseMap();

            //FinancialTransactional
            CreateMap<Model.Repository.FinancialTransactional, Model.UserInterface.Transactional.TransactionalFinancial>().ReverseMap();

            //Period
            CreateMap<Model.Repository.Period, Model.UserInterface.Period>().ReverseMap();
          
            //PeriodType
            CreateMap<Model.Repository.PeriodType, Model.UserInterface.PeriodType>().ReverseMap();

            //ProductCash
            CreateMap<Model.Repository.ProductCash, Model.UserInterface.Cash.CashProduct>()
                .ForMember(target => target.CashProductId, _ => _.MapFrom(source => source.Id));
            CreateMap<Model.UserInterface.Cash.CashProduct, Model.Repository.ProductCash>()
                .ForMember(target => target.Id, _ => _.MapFrom(source => source.CashProductId));

            //ProductLending
            CreateMap<Model.Repository.ProductLending, Model.UserInterface.Lending.LendingProduct>()
                .ForMember(target => target.LendingProductId, _ => _.MapFrom(source => source.Id));
            CreateMap<Model.UserInterface.Lending.LendingProduct, Model.Repository.ProductLending>()
                .ForMember(target => target.Id, _ => _.MapFrom(source => source.LendingProductId));

            //ProductTransactional
            CreateMap<Model.Repository.ProductTransactional, Model.UserInterface.Transactional.TransactionalProduct>()
                .ForMember(target => target.TransactionalProductId, _ => _.MapFrom(source => source.Id));
            CreateMap<Model.UserInterface.Transactional.TransactionalProduct, Model.Repository.ProductTransactional>()
                .ForMember(target => target.Id, _ => _.MapFrom(source => source.TransactionalProductId));

            //ProductType
            CreateMap<Model.Repository.Product, Model.UserInterface.ProductType>().ReverseMap();

            //Province
            CreateMap<Model.Repository.Province, Model.UserInterface.Province>().ReverseMap();

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

            //TableNumber
            CreateMap<Model.Repository.TableNumber, Model.UserInterface.TableNumber>().ReverseMap();

            //TransactionType
            CreateMap<Model.Repository.TransactionType, Model.UserInterface.TransactionType>().ReverseMap();

            //User 
            CreateMap<Model.Repository.User, Model.UserInterface.User>().ReverseMap();
        }
    }
}
