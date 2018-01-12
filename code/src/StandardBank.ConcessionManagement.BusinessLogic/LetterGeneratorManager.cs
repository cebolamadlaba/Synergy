using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Letter generator manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ILetterGeneratorManager" />
    public class LetterGeneratorManager : ILetterGeneratorManager
    {
        /// <summary>
        /// The template path
        /// </summary>
        private readonly string _templatePath;

        /// <summary>
        /// The file utiltity
        /// </summary>
        private readonly IFileUtiltity _fileUtiltity;

        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// The PDF utility
        /// </summary>
        private readonly IPdfUtility _pdfUtility;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// The lending manager
        /// </summary>
        private readonly ILendingManager _lendingManager;

        /// <summary>
        /// The legal entity repository
        /// </summary>
        private readonly ILegalEntityRepository _legalEntityRepository;

        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly ICashManager _cashManager;

        /// <summary>
        /// The razor renderer
        /// </summary>
        private readonly IRazorRenderer _razorRenderer;

        /// <summary>
        /// The transactional manager
        /// </summary>
        private readonly ITransactionalManager _transactionalManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LetterGeneratorManager"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        /// <param name="fileUtiltity">The file utiltity.</param>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="pdfUtility">The PDF utility.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="lendingManager">The lending manager.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        /// <param name="cashManager">The cash manager.</param>
        /// <param name="razorRenderer">The razor renderer.</param>
        /// <param name="transactionalManager">The transactional manager.</param>
        public LetterGeneratorManager(IConfigurationData configurationData, IFileUtiltity fileUtiltity,
            IConcessionManager concessionManager, IPdfUtility pdfUtility, IUserManager userManager,
            ILendingManager lendingManager, ILegalEntityRepository legalEntityRepository, ICashManager cashManager,
            IRazorRenderer razorRenderer, ITransactionalManager transactionalManager)
        {
            _templatePath = configurationData.LetterTemplatePath;
            _fileUtiltity = fileUtiltity;
            _concessionManager = concessionManager;
            _pdfUtility = pdfUtility;
            _userManager = userManager;
            _lendingManager = lendingManager;
            _legalEntityRepository = legalEntityRepository;
            _cashManager = cashManager;
            _razorRenderer = razorRenderer;
            _transactionalManager = transactionalManager;
        }

        /// <summary>
        /// Generates the letters.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        public byte[] GenerateLetters(string concessionReferenceId)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId);
            var concessionLetters = new List<ConcessionLetter>();

            AddConcessionLetters(concession, concessionLetters);

            return GenerateConcessionLetterPdf(concessionLetters);
        }

        /// <summary>
        /// Adds the concession letters.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="concessionLetters">The concession letters.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void AddConcessionLetters(Concession concession, List<ConcessionLetter> concessionLetters)
        {
            var requestor = _userManager.GetUser(concession.RequestorId);
            var bcm = _userManager.GetUser(concession.BcmUserId);

            switch (concession.ConcessionType)
            {
                case Constants.ConcessionType.Lending:
                    concessionLetters.AddRange(GetLendingConcessionLetterData(concession, requestor, bcm));
                    break;
                case Constants.ConcessionType.Cash:
                    concessionLetters.AddRange(GetCashConcessionLetterData(concession, requestor, bcm));
                    break;
                case Constants.ConcessionType.Transactional:
                    concessionLetters.AddRange(GetTransactionalConcessionLetterData(concession, requestor, bcm));
                    break;
                default:
                    throw new NotImplementedException(concession.ConcessionType);
            }
        }

        /// <summary>
        /// Generates the letters for legal entity.
        /// </summary>
        /// <param name="legalEntityId">The legal entity identifier.</param>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <returns></returns>
        public byte[] GenerateLettersForLegalEntity(int legalEntityId, int requestorId)
        {
            var concessionLetters = new List<ConcessionLetter>();

            //TODO: Figure this out
            throw new NotImplementedException();

            return GenerateConcessionLetterPdf(concessionLetters);
        }

        /// <summary>
        /// Generates the letters for concession details.
        /// </summary>
        /// <param name="concessionDetailIds">The concession detail ids.</param>
        /// <returns></returns>
        public byte[] GenerateLettersForConcessionDetails(IEnumerable<int> concessionDetailIds)
        {
            var concessionLetters = new List<ConcessionLetter>();

            //TODO: Figure this out
            throw new NotImplementedException();

            return GenerateConcessionLetterPdf(concessionLetters);
        }

        /// <summary>
        /// Gets the transactional concession letter data.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="requestor">The requestor.</param>
        /// <param name="bcm">The BCM.</param>
        /// <returns></returns>
        private IEnumerable<ConcessionLetter> GetTransactionalConcessionLetterData(Concession concession,
            User requestor, User bcm)
        {
            var transactionalConcession =
                _transactionalManager.GetTransactionalConcession(concession.ReferenceNumber, requestor);
            var transactionalConcessionDetails =
                transactionalConcession.TransactionalConcessionDetails.OrderBy(_ => _.AccountNumber);

            return GetTransactionalConcessionLetterData(concession.RiskGroupNumber, concession.Id, requestor, bcm,
                transactionalConcessionDetails);
        }

        /// <summary>
        /// Gets the transactional concession letter data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="concessionId">The concession identifier.</param>
        /// <param name="requestor">The requestor.</param>
        /// <param name="bcm">The BCM.</param>
        /// <param name="transactionalConcessionDetails">The transactional concession details.</param>
        /// <returns></returns>
        private IEnumerable<ConcessionLetter> GetTransactionalConcessionLetterData(int? riskGroupNumber,
            int concessionId, User requestor, User bcm,
            IOrderedEnumerable<TransactionalConcessionDetail> transactionalConcessionDetails)
        {
            var concessionLetters = new List<ConcessionLetter>();
            var pageBreakBefore = false;

            foreach (var transactionalConcessionDetail in transactionalConcessionDetails)
            {
                var concessionLetter =
                    concessionLetters.FirstOrDefault(_ => _.TransactionalConcessionLetters != null &&
                                                          _.TransactionalConcessionLetters.Any(
                                                              x => x.LegalEntityId == transactionalConcessionDetail
                                                                       .LegalEntityId));

                if (concessionLetter == null)
                {
                    concessionLetter = PopulateBaseConcessionLetter(riskGroupNumber, requestor, bcm,
                        transactionalConcessionDetail.LegalEntityId.Value);

                    concessionLetter.TransactionalConcessionLetters = new List<TransactionalConcessionLetter>();
                    concessionLetter.ConditionConcessionLetters = GetConcessionConditionLetters(concessionId);
                    concessionLetter.PageBreakBefore = pageBreakBefore;

                    pageBreakBefore = true;

                    concessionLetters.Add(concessionLetter);
                }

                var transactionalConcessionLetters = new List<TransactionalConcessionLetter>();
                transactionalConcessionLetters.AddRange(concessionLetter.TransactionalConcessionLetters);
                transactionalConcessionLetters.Add(
                    PopulateTransactionalConcessionLetter(transactionalConcessionDetail));
                concessionLetter.TransactionalConcessionLetters = transactionalConcessionLetters;
            }

            return concessionLetters;
        }

        /// <summary>
        /// Populates the transactional concession letter.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <returns></returns>
        private TransactionalConcessionLetter PopulateTransactionalConcessionLetter(
            TransactionalConcessionDetail transactionalConcessionDetail)
        {
            return new TransactionalConcessionLetter
            {
                AccountNumber = transactionalConcessionDetail.AccountNumber,
                ChannelOrFeeType = transactionalConcessionDetail.TransactionType,
                FeeOrRate = transactionalConcessionDetail.TransactionType ==
                            Constants.Transactional.TransactionType.ChequeEncashmentFee
                    ? $"R {transactionalConcessionDetail.Fee.GetValueOrDefault(0).ToString("N2", CultureInfo.InvariantCulture)} + {transactionalConcessionDetail.AdValorem.GetValueOrDefault(0).ToString("N2", CultureInfo.InvariantCulture)} %"
                    : $"R {transactionalConcessionDetail.Fee.GetValueOrDefault(0).ToString("N2", CultureInfo.InvariantCulture)}",
                ConcessionStartDate = transactionalConcessionDetail.DateApproved.Value.ToString("dd/MM/yyyy"),
                ConcessionEndDate = transactionalConcessionDetail.ExpiryDate.HasValue
                    ? transactionalConcessionDetail.ExpiryDate.Value.ToString("dd/MM/yyyy")
                    : string.Empty,
                LegalEntityId = transactionalConcessionDetail.LegalEntityId
            };
        }

        /// <summary>
        /// Gets the cash concession letter data.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="requestor">The requestor.</param>
        /// <param name="bcm">The BCM.</param>
        /// <returns></returns>
        private IEnumerable<ConcessionLetter> GetCashConcessionLetterData(Concession concession, User requestor,
            User bcm)
        {
            var cashConcession = _cashManager.GetCashConcession(concession.ReferenceNumber, requestor);
            var cashConcessionDetails = cashConcession.CashConcessionDetails.OrderBy(_ => _.AccountNumber);

            return GetCashConcessionLetterData(concession.RiskGroupNumber, concession.Id, requestor, bcm, cashConcessionDetails);
        }

        /// <summary>
        /// Gets the cash concession letter data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="concessionId">The concession identifier.</param>
        /// <param name="requestor">The requestor.</param>
        /// <param name="bcm">The BCM.</param>
        /// <param name="cashConcessionDetails">The cash concession details.</param>
        /// <returns></returns>
        private IEnumerable<ConcessionLetter> GetCashConcessionLetterData(int? riskGroupNumber, int concessionId,
            User requestor, User bcm, IOrderedEnumerable<CashConcessionDetail> cashConcessionDetails)
        {
            var concessionLetters = new List<ConcessionLetter>();
            var pageBreakBefore = false;

            foreach (var cashConcessionDetail in cashConcessionDetails)
            {
                var concessionLetter =
                    concessionLetters.FirstOrDefault(_ => _.CashConcessionLetters != null &&
                                                          _.CashConcessionLetters.Any(
                                                              x => x.LegalEntityId == cashConcessionDetail
                                                                       .LegalEntityId));

                if (concessionLetter == null)
                {
                    concessionLetter = PopulateBaseConcessionLetter(riskGroupNumber, requestor, bcm,
                        cashConcessionDetail.LegalEntityId.Value);

                    concessionLetter.CashConcessionLetters = new List<CashConcessionLetter>();
                    concessionLetter.ConditionConcessionLetters = GetConcessionConditionLetters(concessionId);
                    concessionLetter.PageBreakBefore = pageBreakBefore;

                    pageBreakBefore = true;

                    concessionLetters.Add(concessionLetter);
                }

                var cashConcessionLetters = new List<CashConcessionLetter>();
                cashConcessionLetters.AddRange(concessionLetter.CashConcessionLetters);
                cashConcessionLetters.Add(PopulateCashConcessionLetter(cashConcessionDetail));
                concessionLetter.CashConcessionLetters = cashConcessionLetters;
            }

            return concessionLetters;
        }

        /// <summary>
        /// Populates the cash concession letter.
        /// </summary>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <returns></returns>
        private CashConcessionLetter PopulateCashConcessionLetter(CashConcessionDetail cashConcessionDetail)
        {
            return new CashConcessionLetter
            {
                AccountNumber = cashConcessionDetail.AccountNumber,
                ChannelType = cashConcessionDetail.Channel,
                BaseRateAdValorem =
                    $"R {cashConcessionDetail.BaseRate.GetValueOrDefault(0).ToString("N2", CultureInfo.InvariantCulture)} + {cashConcessionDetail.AdValorem.GetValueOrDefault(0).ToString("N2", CultureInfo.InvariantCulture)}%",
                ConcessionEndDate = cashConcessionDetail.ExpiryDate.HasValue
                    ? cashConcessionDetail.ExpiryDate.Value.ToString("dd/MM/yyyy")
                    : string.Empty,
                ConcessionStartDate = cashConcessionDetail.DateApproved.Value.ToString("dd/MM/yyyy"),
                LegalEntityId = cashConcessionDetail.LegalEntityId
            };
        }

        /// <summary>
        /// Gets the lending concession letter data.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="requestor">The requestor.</param>
        /// <param name="bcm">The BCM.</param>
        /// <returns></returns>
        private IEnumerable<ConcessionLetter> GetLendingConcessionLetterData(Concession concession, User requestor,
            User bcm)
        {
            var lendingConcession = _lendingManager.GetLendingConcession(concession.ReferenceNumber, requestor);
            var lendingConcessionDetails = lendingConcession.LendingConcessionDetails.OrderBy(_ => _.AccountNumber);

            return GetLendingConcessionLetterData(concession.RiskGroupNumber, concession.Id, requestor, bcm, lendingConcessionDetails);
        }

        /// <summary>
        /// Gets the lending concession letter data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="concessionId">The concession identifier.</param>
        /// <param name="requestor">The requestor.</param>
        /// <param name="bcm">The BCM.</param>
        /// <param name="lendingConcessionDetails">The lending concession details.</param>
        /// <returns></returns>
        private IEnumerable<ConcessionLetter> GetLendingConcessionLetterData(int? riskGroupNumber, int concessionId,
            User requestor, User bcm, IOrderedEnumerable<LendingConcessionDetail> lendingConcessionDetails)
        {
            var concessionLetters = new List<ConcessionLetter>();
            var pageBreakBefore = false;

            foreach (var lendingConcessionDetail in lendingConcessionDetails)
            {
                var concessionLetter =
                    concessionLetters.FirstOrDefault(_ => _.LendingConcessionLetters != null &&
                                                          _.LendingConcessionLetters.Any(
                                                              x => x.LegalEntityId == lendingConcessionDetail
                                                                       .LegalEntityId));

                if (concessionLetter == null)
                {
                    concessionLetter =
                        concessionLetters.FirstOrDefault(
                            _ => _ != null &&
                                 _.LendingOverDraftConcessionLetters.Any(
                                     x => x.LegalEntityId == lendingConcessionDetail.LegalEntityId));
                }

                if (concessionLetter == null)
                {
                    concessionLetter = PopulateBaseConcessionLetter(riskGroupNumber, requestor, bcm,
                        lendingConcessionDetail.LegalEntityId.Value);

                    concessionLetter.LendingConcessionLetters = new List<LendingConcessionLetter>();
                    concessionLetter.LendingOverDraftConcessionLetters = new List<LendingOverDraftConcessionLetter>();
                    concessionLetter.ConditionConcessionLetters = GetConcessionConditionLetters(concessionId);
                    concessionLetter.PageBreakBefore = pageBreakBefore;

                    pageBreakBefore = true;

                    concessionLetters.Add(concessionLetter);
                }

                if (lendingConcessionDetail.ProductType == Constants.Lending.ProductType.Overdraft)
                {
                    var lendingOverDraftConcessionLetters = new List<LendingOverDraftConcessionLetter>();
                    lendingOverDraftConcessionLetters.AddRange(concessionLetter.LendingOverDraftConcessionLetters);
                    lendingOverDraftConcessionLetters.Add(
                        PopulateLendingOverDraftConcessionLetter(lendingConcessionDetail));
                    concessionLetter.LendingOverDraftConcessionLetters = lendingOverDraftConcessionLetters;
                }
                else
                {
                    var lendingConcessionLetters = new List<LendingConcessionLetter>();
                    lendingConcessionLetters.AddRange(concessionLetter.LendingConcessionLetters);
                    lendingConcessionLetters.Add(PopulateLendingConcessionLetter(lendingConcessionDetail));
                    concessionLetter.LendingConcessionLetters = lendingConcessionLetters;
                }
            }

            return concessionLetters;
        }

        /// <summary>
        /// Populates the lending over draft concession letter.
        /// </summary>
        /// <param name="lendingConcessionDetail">The lending concession detail.</param>
        /// <returns></returns>
        private LendingOverDraftConcessionLetter PopulateLendingOverDraftConcessionLetter(
            LendingConcessionDetail lendingConcessionDetail)
        {
            return new LendingOverDraftConcessionLetter
            {
                AccountNumber = lendingConcessionDetail.AccountNumber,
                ApprovedMarginToPrime =
                    $"R {lendingConcessionDetail.ApprovedMap.GetValueOrDefault(0).ToString("N2", CultureInfo.InvariantCulture)}",
                ProductType = lendingConcessionDetail.ProductType,
                ReviewFeeType = lendingConcessionDetail.ReviewFeeType,
                MarginToPrime =
                    $"R {lendingConcessionDetail.MarginAgainstPrime.ToString("N2", CultureInfo.InvariantCulture)}",
                InitiationFee =
                    $"R {lendingConcessionDetail.InitiationFee.ToString("N2", CultureInfo.InvariantCulture)}",
                ReviewFee = $"R {lendingConcessionDetail.ReviewFee.ToString("N2", CultureInfo.InvariantCulture)}",
                ConcessionEndDate = lendingConcessionDetail.ExpiryDate.Value.ToString("dd/MM/yyyy"),
                ConcessionStartDate = lendingConcessionDetail.DateApproved.Value.ToString("dd/MM/yyyy"),
                UFFFee = $"R {lendingConcessionDetail.UffFee.ToString("N2", CultureInfo.InvariantCulture)}",
                LegalEntityId = lendingConcessionDetail.LegalEntityId
            };
        }

        /// <summary>
        /// Gets the concession condition letters.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        private IEnumerable<ConditionConcessionLetter> GetConcessionConditionLetters(int concessionId)
        {
            var conditions = new List<ConditionConcessionLetter>();

            var concessionConditions = _concessionManager.GetConcessionConditions(concessionId);

            foreach (var concessionCondition in concessionConditions)
            {
                conditions.Add(new ConditionConcessionLetter
                {
                    Value =
                        $"R {concessionCondition.ExpectedTurnoverValue.GetValueOrDefault(0).ToString("N2", CultureInfo.InvariantCulture)}",
                    ConditionProduct = concessionCondition.ProductType,
                    ConditionMeasure = concessionCondition.ConditionType,
                    Deadline = concessionCondition.ExpiryDate.HasValue
                        ? concessionCondition.ExpiryDate.Value.ToString("dd/MM/yyyy")
                        : $"{concessionCondition.Period} - {concessionCondition.PeriodType}"
                });
            }

            return conditions;
        }

        /// <summary>
        /// Populates the lending concession letter.
        /// </summary>
        /// <param name="lendingConcessionDetail">The lending concession detail.</param>
        /// <returns></returns>
        private LendingConcessionLetter PopulateLendingConcessionLetter(LendingConcessionDetail lendingConcessionDetail)
        {
            return new LendingConcessionLetter
            {
                AccountNumber = lendingConcessionDetail.AccountNumber,
                ProductType = lendingConcessionDetail.ProductType,
                ChannelOrFeeType =
                    $"R {lendingConcessionDetail.InitiationFee.ToString("N2", CultureInfo.InvariantCulture)}",
                FeeOrMarginAbovePrime =
                    $"R {lendingConcessionDetail.MarginAgainstPrime.ToString("N2", CultureInfo.InvariantCulture)}",
                ConcessionEndDate = lendingConcessionDetail.ExpiryDate.Value.ToString("dd/MM/yyyy"),
                ConcessionStartDate = lendingConcessionDetail.DateApproved.Value.ToString("dd/MM/yyyy"),
                LegalEntityId = lendingConcessionDetail.LegalEntityId
            };
        }

        /// <summary>
        /// Populates the base concession letter.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="requestor">The requestor.</param>
        /// <param name="bcm">The BCM.</param>
        /// <param name="legalEntityId">The legal entity identifier.</param>
        /// <returns></returns>
        private ConcessionLetter PopulateBaseConcessionLetter(int? riskGroupNumber, User requestor, User bcm,
            int legalEntityId)
        {
            var legalEntity = _legalEntityRepository.ReadById(legalEntityId);

            var concessionLetter = new ConcessionLetter
            {
                CurrentDate = DateTime.Now.ToString("dd/MM/yyyy"),
                TemplatePath = _templatePath,
                RiskGroupNumber = Convert.ToString(riskGroupNumber),
                BCMEmailAddress = bcm?.EmailAddress,
                BCMContactNumber = bcm?.ContactNumber,
                BCMName = bcm?.FullName,
                RequestorEmailAddress = requestor?.EmailAddress,
                RequestorName = requestor?.FullName,
                RequestorContactNumber = requestor?.ContactNumber,
                ClientName = GetValueOrDashes(legalEntity.CustomerName),
                ClientNumber = GetValueOrDashes(legalEntity.CustomerNumber),
                ClientPostalAddress = GetValueOrDashes(legalEntity.PostalAddress),
                ClientCity = GetValueOrDashes(legalEntity.City),
                ClientContactPerson = GetValueOrDashes(legalEntity.ContactPerson),
                ClientPostalCode = GetValueOrDashes(legalEntity.PostalCode)
            };

            return concessionLetter;
        }

        /// <summary>
        /// Gets the value or dashes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private string GetValueOrDashes(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return value;

            return "____________________________________________";
        }

        /// <summary>
        /// Generates the concession letter PDF.
        /// </summary>
        /// <param name="concessionLetters">The concession letters.</param>
        /// <returns></returns>
        private byte[] GenerateConcessionLetterPdf(IEnumerable<ConcessionLetter> concessionLetters)
        {
            var templateHeaderPath = System.IO.Path.Combine(_templatePath, "TemplateHeader.html");
            var templateFooterPath = System.IO.Path.Combine(_templatePath, "TemplateFooter.html");
            var concessionLetterPath = System.IO.Path.Combine(_templatePath, "ConcessionLetter.cshtml");
            var concessionLetterHtml = _fileUtiltity.ReadFileText(concessionLetterPath);

            var html = new StringBuilder();

            //add the header
            html.Append(_fileUtiltity.ReadFileText(templateHeaderPath));

            //loop through the concession letters and add each one, run the razor rendered to populate the template
            //with the relevant details
            foreach (var concessionLetter in concessionLetters)
                html.Append(_razorRenderer.Parse(concessionLetterHtml, concessionLetter));

            //add the footer
            html.Append(_fileUtiltity.ReadFileText(templateFooterPath));

            //generate a pdf from the html
            return _pdfUtility.GeneratePdfFromHtml(html.ToString());
        }
    }
}