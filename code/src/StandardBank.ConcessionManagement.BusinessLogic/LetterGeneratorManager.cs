using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

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
        public LetterGeneratorManager(IConfigurationData configurationData, IFileUtiltity fileUtiltity,
            IConcessionManager concessionManager, IPdfUtility pdfUtility, IUserManager userManager,
            ILendingManager lendingManager, ILegalEntityRepository legalEntityRepository, ICashManager cashManager,
            IRazorRenderer razorRenderer)
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
        }

        /// <summary>
        /// Generates the letters.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        public byte[] GenerateLetters(string concessionReferenceId)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId);
            var requestor = _userManager.GetUser(concession.RequestorId);
            var bcm = _userManager.GetUser(concession.BcmUserId);

            var concessionLetters = new List<ConcessionLetter>();

            //TODO: Get concession details based on concession type
            switch (concession.ConcessionType)
            {
                case "Lending":
                    concessionLetters.AddRange(GetLendingConcessionLetterData(concession, requestor, bcm));
                    break;
                case "Cash":
                    concessionLetters.AddRange(GetCashConcessionLetterData(concession, requestor, bcm));
                    break;
                case "Transactional":
                    concessionLetters.AddRange(GetTransactionalConcessionLetterData(concession, requestor, bcm));
                    break;
                default:
                    throw new NotImplementedException(concession.ConcessionType);
            }

            return GenerateConcessionLetterPdf(concessionLetters);
        }

        /// <summary>
        /// Gets the transactional concession letter data.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="requestor">The requestor.</param>
        /// <param name="bcm">The BCM.</param>
        /// <returns></returns>
        private IEnumerable<ConcessionLetter> GetTransactionalConcessionLetterData(Concession concession, User requestor, User bcm)
        {
            //TODO:
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the cash concession letter data.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="requestor">The requestor.</param>
        /// <param name="bcm">The BCM.</param>
        /// <returns></returns>
        private IEnumerable<ConcessionLetter> GetCashConcessionLetterData(Concession concession, User requestor, User bcm)
        {
            var concessionLetters = new List<ConcessionLetter>();
            var pageBreakBefore = false;
            var cashConcession = _cashManager.GetCashConcession(concession.ReferenceNumber, requestor);
            var cashConcessionDetails = cashConcession.CashConcessionDetails.OrderBy(_ => _.AccountNumber);

            foreach (var cashConcessionDetail in cashConcessionDetails)
            {
                var concessionLetter =
                    concessionLetters.FirstOrDefault(_ => _.CashConcessionLetters != null &&
                                                          _.CashConcessionLetters.Any(
                                                              x => x.AccountNumber == cashConcessionDetail
                                                                       .AccountNumber));

                if (concessionLetter == null)
                {
                    concessionLetter = PopulateBaseConcessionLetter(concession, requestor, bcm,
                        cashConcessionDetail.LegalEntityId.Value);

                    concessionLetter.CashConcessionLetters = new List<CashConcessionLetter>();
                    concessionLetter.ConditionConcessionLetters = GetConcessionConditionLetters(concession);
                    concessionLetter.PageBreakBefore = pageBreakBefore;

                    pageBreakBefore = true;

                    concessionLetters.Add(concessionLetter);
                }

                var cashConcessionLetters = new List<CashConcessionLetter>();
                cashConcessionLetters.AddRange(concessionLetter.CashConcessionLetters);
                cashConcessionLetters.Add(PopulateCashConcessionLetter(concession, cashConcessionDetail));
                concessionLetter.CashConcessionLetters = cashConcessionLetters;
            }

            return concessionLetters;
        }

        /// <summary>
        /// Populates the cash concession letter.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <returns></returns>
        private CashConcessionLetter PopulateCashConcessionLetter(Concession concession, CashConcessionDetail cashConcessionDetail)
        {
            return new CashConcessionLetter
            {
                AccountNumber = cashConcessionDetail.AccountNumber,
                ChannelType = cashConcessionDetail.Channel,
                BaseRateAdValorem =
                    $"{cashConcessionDetail.BaseRate.GetValueOrDefault(0):C} + {cashConcessionDetail.AdValorem.GetValueOrDefault(0)}%",
                ConcessionEndDate = concession.DateApproved.Value.ToString("dd/MM/yyyy"),
                ConcessionStartDate = concession.ExpiryDate.Value.ToString("dd/MM/yyyy")
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
            var concessionLetters = new List<ConcessionLetter>();
            var pageBreakBefore = false;
            var lendingConcession = _lendingManager.GetLendingConcession(concession.ReferenceNumber, requestor);
            var lendingConcessionDetails = lendingConcession.LendingConcessionDetails.OrderBy(_ => _.AccountNumber);

            foreach (var lendingConcessionDetail in lendingConcessionDetails)
            {
                var concessionLetter =
                    concessionLetters.FirstOrDefault(_ => _.LendingConcessionLetters != null &&
                                                          _.LendingConcessionLetters.Any(
                                                              x => x.AccountNumber == lendingConcessionDetail
                                                                       .AccountNumber));

                if (concessionLetter == null)
                {
                    concessionLetter =
                        concessionLetters.FirstOrDefault(
                            _ => _ != null &&
                                 _.LendingOverDraftConcessionLetters.Any(
                                     x => x.AccountNumber == lendingConcessionDetail.AccountNumber));
                }

                if (concessionLetter == null)
                {
                    concessionLetter = PopulateBaseConcessionLetter(concession, requestor, bcm,
                        lendingConcessionDetail.LegalEntityId.Value);

                    concessionLetter.LendingConcessionLetters = new List<LendingConcessionLetter>();
                    concessionLetter.LendingOverDraftConcessionLetters = new List<LendingOverDraftConcessionLetter>();
                    concessionLetter.ConditionConcessionLetters = GetConcessionConditionLetters(concession);
                    concessionLetter.PageBreakBefore = pageBreakBefore;

                    pageBreakBefore = true;

                    concessionLetters.Add(concessionLetter);
                }

                if (lendingConcessionDetail.ProductType == "Overdraft")
                {
                    var lendingOverDraftConcessionLetters = new List<LendingOverDraftConcessionLetter>();
                    lendingOverDraftConcessionLetters.AddRange(concessionLetter.LendingOverDraftConcessionLetters);
                    lendingOverDraftConcessionLetters.Add(
                        PopulateLendingOverDraftConcessionLetter(concession, lendingConcessionDetail));
                    concessionLetter.LendingOverDraftConcessionLetters = lendingOverDraftConcessionLetters;
                }
                else
                {
                    var lendingConcessionLetters = new List<LendingConcessionLetter>();
                    lendingConcessionLetters.AddRange(concessionLetter.LendingConcessionLetters);
                    lendingConcessionLetters.Add(PopulateLendingConcessionLetter(concession, lendingConcessionDetail));
                    concessionLetter.LendingConcessionLetters = lendingConcessionLetters;
                }
            }

            return concessionLetters;
        }

        /// <summary>
        /// Populates the lending over draft concession letter.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="lendingConcessionDetail">The lending concession detail.</param>
        /// <returns></returns>
        private LendingOverDraftConcessionLetter PopulateLendingOverDraftConcessionLetter(Concession concession,
            LendingConcessionDetail lendingConcessionDetail)
        {
            return new LendingOverDraftConcessionLetter
            {
                AccountNumber = lendingConcessionDetail.AccountNumber,
                ApprovedMarginToPrime = lendingConcessionDetail.ApprovedMap.ToString("C"),
                ProductType = lendingConcessionDetail.ProductType,
                ReviewFeeType = lendingConcessionDetail.ReviewFeeType,
                MarginToPrime = lendingConcessionDetail.MarginAgainstPrime.ToString("C"),
                InitiationFee = lendingConcessionDetail.InitiationFee.ToString("C"),
                ReviewFee = lendingConcessionDetail.ReviewFee.ToString("C"),
                ConcessionEndDate = concession.DateApproved.Value.ToString("dd/MM/yyyy"),
                ConcessionStartDate = concession.ExpiryDate.Value.ToString("dd/MM/yyyy"),
                UFFFee = lendingConcessionDetail.UffFee.ToString("C")
            };
        }

        /// <summary>
        /// Gets the concession condition letters.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        private IEnumerable<ConditionConcessionLetter> GetConcessionConditionLetters(Concession concession)
        {
            var conditions = new List<ConditionConcessionLetter>();

            var concessionConditions = _concessionManager.GetConcessionConditions(concession.Id);

            foreach (var concessionCondition in concessionConditions)
            {
                conditions.Add(new ConditionConcessionLetter
                {
                    Value = concessionCondition.ConditionValue.ToString("C"),
                    ConditionProduct = concessionCondition.ProductType,
                    ConditionMeasure = concessionCondition.ConditionType,
                    Deadline = $"{concessionCondition.Period} - {concessionCondition.PeriodType}"
                });
            }

            return conditions;
        }

        /// <summary>
        /// Populates the lending concession letter.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="lendingConcessionDetail">The lending concession detail.</param>
        /// <returns></returns>
        private LendingConcessionLetter PopulateLendingConcessionLetter(Concession concession, LendingConcessionDetail lendingConcessionDetail)
        {
            return new LendingConcessionLetter
            {
                AccountNumber = lendingConcessionDetail.AccountNumber,
                ProductType = lendingConcessionDetail.ProductType,
                ChannelOrFeeType = lendingConcessionDetail.InitiationFee.ToString("C"),
                FeeOrMarginAbovePrime = lendingConcessionDetail.MarginAgainstPrime.ToString("C"),
                ConcessionEndDate = concession.DateApproved.Value.ToString("dd/MM/yyyy"),
                ConcessionStartDate = concession.ExpiryDate.Value.ToString("dd/MM/yyyy")
            };
        }

        /// <summary>
        /// Populates the base concession letter.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="requestor">The requestor.</param>
        /// <param name="bcm">The BCM.</param>
        /// <param name="legalEntityId">The legal entity identifier.</param>
        /// <returns></returns>
        private ConcessionLetter PopulateBaseConcessionLetter(Concession concession, User requestor, User bcm, int legalEntityId)
        {
            var legalEntity = _legalEntityRepository.ReadById(legalEntityId);

            var concessionLetter = new ConcessionLetter
            {
                CurrentDate = DateTime.Now.ToString("dd/MM/yyyy"),
                TemplatePath = _templatePath,
                RiskGroupNumber = Convert.ToString(concession.RiskGroupNumber),
                BCMEmailAddress = bcm?.EmailAddress,
                BCMContactNumber = bcm?.ContactNumber,
                BCMName = bcm?.FullName,
                RequestorEmailAddress = requestor?.EmailAddress,
                RequestorName = requestor?.FullName,
                RequestorContactNumber = requestor?.ContactNumber,
                ClientName = legalEntity.CustomerName,
                ClientNumber = legalEntity.CustomerNumber,
                ClientPostalAddress = legalEntity.PostalAddress,
                ClientCity = legalEntity.City,
                ClientContactPerson = legalEntity.ContactPerson,
                ClientPostalCode = legalEntity.PostalCode
            };

            return concessionLetter;
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
