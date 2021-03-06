using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System.Collections.Generic;
using System.Linq;

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

            //GlmsTierData
            CreateMap<Model.Repository.GlmsTierData, Model.UserInterface.GlmsTierData>().ReverseMap();
            CreateMap<Model.Repository.GlmsTierDataView, Model.UserInterface.GlmsTierDataView>().ReverseMap();

            //ChannelType
            CreateMap<Model.Repository.ChannelType, Model.UserInterface.ChannelType>().ReverseMap();

            //Concession
            CreateMap<Model.Repository.Concession, Model.UserInterface.Concession>()
                .ForMember(target => target.ReferenceNumber, _ => _.MapFrom(source => source.ConcessionRef))
                .ForMember(target => target.DateOpened, _ => _.MapFrom(source => source.ConcessionDate))
                .ForMember(target => target.DateSentForApproval, _ => _.MapFrom(source => source.DatesentForApproval))
                .ForMember(target => target.Requestor, _ => _.Ignore())
                .ForMember(target => target.SmtDealNumber, _ => _.MapFrom(source => source.SMTDealNumber))

                .ForMember(target => target.MrsCrs, _ => _.MapFrom(source => source.MRS_CRS));
            CreateMap<Model.UserInterface.Concession, Model.Repository.Concession>()
                .ForMember(target => target.ConcessionRef, _ => _.MapFrom(source => source.ReferenceNumber))
                .ForMember(target => target.ConcessionDate, _ => _.MapFrom(source => source.DateOpened))
                .ForMember(target => target.DatesentForApproval, _ => _.MapFrom(source => source.DateSentForApproval))
                .ForMember(target => target.SMTDealNumber, _ => _.MapFrom(source => source.SmtDealNumber))
                .ForMember(target => target.MRS_CRS, _ => _.MapFrom(source => source.MrsCrs));

            //ConcessionCash
            CreateMap<Model.Repository.ConcessionCash, Model.UserInterface.Cash.CashConcessionDetail>()
                .ForMember(target => target.CashConcessionDetailId, _ => _.MapFrom(source => source.Id));
            CreateMap<Model.UserInterface.Cash.CashConcessionDetail, Model.Repository.ConcessionCash>()
                .ForMember(target => target.Id, _ => _.MapFrom(source => source.CashConcessionDetailId));

            //ConcessionComment
            CreateMap<Model.Repository.ConcessionComment, Model.UserInterface.ConcessionComment>().ReverseMap();

            //ConcessionCondition
            CreateMap<Model.Repository.ConcessionCondition, Model.UserInterface.ConcessionCondition>()
                    .ForMember(target => target.ConditionVolume, _ => _.MapFrom(source => source.Volume))
                    .ForMember(target => target.ConditionValue, _ => _.MapFrom(source => source.Value))
                    .ForMember(target => target.ConcessionConditionId, _ => _.MapFrom(source => source.Id))
                    .ForMember(target => target.ApprovedDate, _ => _.MapFrom(source => source.DateApproved));
            CreateMap<Model.UserInterface.ConcessionCondition, Model.Repository.ConcessionCondition>()
                .ForMember(target => target.Volume, _ => _.MapFrom(source => source.ConditionVolume))
                .ForMember(target => target.Value, _ => _.MapFrom(source => source.ConditionValue))
                .ForMember(target => target.Id, _ => _.MapFrom(source => source.ConcessionConditionId))
                .ForMember(target => target.DateApproved, _ => _.MapFrom(source => source.ApprovedDate));

            //ConcessionConditionView
            CreateMap<Model.Repository.ConcessionConditionView, Model.UserInterface.ConcessionCondition>()
                .ForMember(target => target.ConditionVolume, _ => _.MapFrom(source => source.Volume))
                .ForMember(target => target.ConditionValue, _ => _.MapFrom(source => source.Value))
                .ForMember(target => target.ApprovedDate, _ => _.MapFrom(source => source.DateApproved))
                .ForMember(target => target.ConcessionReferenceNumber, _ => _.MapFrom(source => source.ReferenceNumber))
                .ForMember(target => target.ProductType, _ => _.MapFrom(source => source.ConditionProduct));
            CreateMap<Model.UserInterface.ConcessionCondition, Model.Repository.ConcessionConditionView>()
                .ForMember(target => target.Volume, _ => _.MapFrom(source => source.ConditionVolume))
                .ForMember(target => target.Value, _ => _.MapFrom(source => source.ConditionValue))
                .ForMember(target => target.DateApproved, _ => _.MapFrom(source => source.ApprovedDate))
                .ForMember(target => target.ReferenceNumber, _ => _.MapFrom(source => source.ConcessionReferenceNumber))
                .ForMember(target => target.ConditionProduct, _ => _.MapFrom(source => source.ProductType));

            //ConcessionInboxView
            CreateMap<Model.Repository.ConcessionInboxView, Model.UserInterface.Inbox.InboxConcession>()
                .ForMember(target => target.DateOpened, _ => _.MapFrom(source => source.ConcessionDate))
                .ForMember(target => target.ReferenceNumber, _ => _.MapFrom(source => source.ConcessionRef))
                .ForMember(target => target.DateSentForApproval, _ => _.MapFrom(source => source.DatesentForApproval));
            CreateMap<Model.UserInterface.Inbox.InboxConcession, Model.Repository.ConcessionInboxView>()
                .ForMember(target => target.ConcessionDate, _ => _.MapFrom(source => source.DateOpened))
                .ForMember(target => target.ConcessionRef, _ => _.MapFrom(source => source.ReferenceNumber))
                .ForMember(target => target.DatesentForApproval, _ => _.MapFrom(source => source.DateSentForApproval));

            //ConcessionLending
            CreateMap<Model.Repository.ConcessionLending, Model.UserInterface.Lending.LendingConcessionDetail>()
                .ForMember(target => target.MarginAgainstPrime, _ => _.MapFrom(source => source.MarginToPrime))
                .ForMember(target => target.LendingConcessionDetailId, _ => _.MapFrom(source => source.Id))
                .ForMember(target => target.ApprovedMap, _ => _.MapFrom(source => source.ApprovedMarginToPrime))
                .ForMember(target => target.LoadedMap, _ => _.MapFrom(source => source.LoadedMarginToPrime))
                .ForMember(target => target.MrsEri, _ => _.MapFrom(source => source.MRS_ERI));
            CreateMap<Model.UserInterface.Lending.LendingConcessionDetail, Model.Repository.ConcessionLending>()
                .ForMember(target => target.MarginToPrime, _ => _.MapFrom(source => source.MarginAgainstPrime))
                .ForMember(target => target.Id, _ => _.MapFrom(source => source.LendingConcessionDetailId))
                .ForMember(target => target.ApprovedMarginToPrime, _ => _.MapFrom(source => source.ApprovedMap))
                .ForMember(target => target.LoadedMarginToPrime, _ => _.MapFrom(source => source.LoadedMap))
                .ForMember(target => target.MRS_ERI, _ => _.MapFrom(source => source.MrsEri));

            //ConcessionLendingTieredRate
            CreateMap<Model.Repository.ConcessionLendingTieredRate, Model.UserInterface.Lending.LendingConcessionDetailTieredRate>()
                .ForMember(target => target.ApprovedMap, _ => _.MapFrom(source => source.ApprovedMarginToPrime))
                .ReverseMap();

            //ConcessionRelationship
            CreateMap<Model.Repository.ConcessionRelationship, Model.UserInterface.ConcessionRelationship>()
                .ReverseMap();

            //ConcessionRelationshipDetail
            CreateMap<Model.Repository.ConcessionRelationshipDetail, Model.UserInterface.ConcessionRelationshipDetail>()
                    .ReverseMap();

            //ConcessionTransactional
            CreateMap<Model.Repository.ConcessionTransactional,
                    Model.UserInterface.Transactional.TransactionalConcessionDetail>()
                    .ForMember(target => target.TransactionalConcessionDetailId, _ => _.MapFrom(source => source.Id));
            CreateMap<Model.UserInterface.Transactional.TransactionalConcessionDetail,
                    Model.Repository.ConcessionTransactional>()
                .ForMember(target => target.Id, _ => _.MapFrom(source => source.TransactionalConcessionDetailId));

            //ConcessionType
            CreateMap<Model.Repository.ConcessionType, Model.UserInterface.ConcessionType>().ReverseMap();

            //ConditionProduct
            CreateMap<Model.Repository.ConditionProduct, Model.UserInterface.ConditionProduct>().ReverseMap();

            //ConditionType
            CreateMap<Model.Repository.ConditionType, Model.UserInterface.ConditionType>().ReverseMap();

            //FinancialCash
            CreateMap<Model.Repository.FinancialCash, Model.UserInterface.Cash.CashFinancial>().ReverseMap();

            //FinancialBol
            CreateMap<Model.Repository.FinancialBol, Model.UserInterface.Bol.BolFinancial>().ReverseMap();

            //FinancialTrade
            CreateMap<Model.Repository.FinancialTrade, Model.UserInterface.Trade.TradeFinancial>().ReverseMap();

            //FinancialInvestment
            CreateMap<Model.Repository.FinancialInvestment, Model.UserInterface.Investment.InvestmentFinancial>().ReverseMap();

            //FinancialLending
            CreateMap<Model.Repository.FinancialLending, Model.UserInterface.Lending.LendingFinancial>().ReverseMap();

            //FinancialTransactional
            CreateMap<Model.Repository.FinancialTransactional, Model.UserInterface.Transactional.TransactionalFinancial
            >().ReverseMap();

            //Period
            CreateMap<Model.Repository.Period, Model.UserInterface.Period>().ReverseMap();

            //GlmsGroup
            CreateMap<Model.Repository.GlmsGroup, Model.UserInterface.GlmsGroup>().ReverseMap();

            //InterestPricingCategory
            CreateMap<Model.Repository.InterestPricingCategory, Model.UserInterface.InterestPricingCategory>().ReverseMap();

            //InterestType
            CreateMap<Model.Repository.InterestType, Model.UserInterface.InterestType>().ReverseMap();

            //RateType
            CreateMap<Model.Repository.RateType, Model.UserInterface.RateType>().ReverseMap();

            //SlabType
            CreateMap<Model.Repository.SlabType, Model.UserInterface.SlabType>().ReverseMap();

            //PeriodType
            CreateMap<Model.Repository.PeriodType, Model.UserInterface.PeriodType>().ReverseMap();

            //BaseRateCode
            CreateMap<Model.Repository.BaseRateCode, Model.UserInterface.BaseRateCode>().ReverseMap();

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

            //Region
            CreateMap<Model.Repository.Region, Model.UserInterface.Region>().ReverseMap();

            //ReviewFeeType
            CreateMap<Model.Repository.ReviewFeeType, Model.UserInterface.ReviewFeeType>().ReverseMap();

            //RiskGroup
            CreateMap<Model.Repository.RiskGroup, Model.UserInterface.RiskGroup>()
                    .ForMember(target => target.Name, _ => _.MapFrom(source => source.RiskGroupName))
                    .ForMember(target => target.Number, _ => _.MapFrom(source => source.RiskGroupNumber));
            CreateMap<Model.UserInterface.RiskGroup, Model.Repository.RiskGroup>()
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

            //TransactionTableNumber
            CreateMap<Model.Repository.TransactionTableNumber, Model.UserInterface.Transactional.TransactionTableNumber>().ReverseMap();

            //TransactionType
            CreateMap<Model.Repository.TransactionType, Model.UserInterface.TransactionType>().ReverseMap();

            //User 
            CreateMap<Model.Repository.User, Model.UserInterface.User>().ReverseMap();

            //BOL
            CreateMap<Model.Repository.BOLChargeCode, Model.UserInterface.Bol.BOLChargeCode>().ReverseMap();
            CreateMap<Model.Repository.BOLChargeCodeType, Model.UserInterface.Bol.BOLChargeCodeType>().ReverseMap();
            CreateMap<Model.Repository.LegalEntityBOLUser, Model.UserInterface.Bol.LegalEntityBOLUser>().ReverseMap();
            CreateMap<Model.Repository.ConcessionBol, Model.UserInterface.Bol.BolConcessionDetail>().ReverseMap();

            //Trade
            CreateMap<Model.Repository.TradeProduct, Model.UserInterface.Trade.TradeProduct>().ReverseMap();
            CreateMap<Model.Repository.TradeProductType, Model.UserInterface.Trade.TradeProductType>().ReverseMap();
            CreateMap<Model.Repository.ConcessionTrade, Model.UserInterface.Trade.TradeConcessionDetail>().ReverseMap();
            CreateMap<Model.Repository.LegalEntityGBBNumber, Model.UserInterface.Trade.LegalEntityGBBNumber>().ReverseMap();

            //Investment
            CreateMap<Model.Repository.InvestmentProduct, Model.UserInterface.Investment.InvestmentProduct>().ReverseMap();
            CreateMap<Model.Repository.InvestmentProductType, Model.UserInterface.Investment.InvestmentProductType>().ReverseMap();
            CreateMap<Model.Repository.ConcessionInvestment, Model.UserInterface.Investment.InvestmentConcessionDetail>().ReverseMap();
            CreateMap<Model.Repository.LegalEntityGBBNumber, Model.UserInterface.Investment.LegalEntityGBBNumber>().ReverseMap();

            //LegalEntity
            CreateMap<Model.Repository.LegalEntity, Model.UserInterface.LegalEntity>().ReverseMap();
        }

    }
}
