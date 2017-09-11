using System;
using System.Collections.Generic;
using System.Linq;
using FluentEmail.Razor;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator;
using StandardBank.ConcessionManagement.Model.UserInterface;

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
        /// Initializes a new instance of the <see cref="LetterGeneratorManager"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        /// <param name="fileUtiltity">The file utiltity.</param>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="pdfUtility">The PDF utility.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="lendingManager">The lending manager.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        public LetterGeneratorManager(IConfigurationData configurationData, IFileUtiltity fileUtiltity,
            IConcessionManager concessionManager, IPdfUtility pdfUtility, IUserManager userManager,
            ILendingManager lendingManager, ILegalEntityRepository legalEntityRepository)
        {
            _templatePath = configurationData.LetterTemplatePath;
            _fileUtiltity = fileUtiltity;
            _concessionManager = concessionManager;
            _pdfUtility = pdfUtility;
            _userManager = userManager;
            _lendingManager = lendingManager;
            _legalEntityRepository = legalEntityRepository;
        }

        /// <summary>
        /// Generates the letters.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        public byte[] GenerateLetters(string concessionReferenceId)
        {
            var concessionLetterPath = System.IO.Path.Combine(_templatePath, "ConcessionLetter.cshtml");
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
            }

            //TODO: Group by customer and generate a concession letter for each customer associated with the concession 

            //TODO: Combine all the concession letters into one PDF for printing

            //TODO: This is a hack (calling First()) to allow me to check in
            return GeneratePdf(concessionLetterPath, concessionLetters.First());
        }

        /// <summary>
        /// Gets the lending concession letter data.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="requestor">The requestor.</param>
        /// <param name="bcm">The BCM.</param>
        /// <returns></returns>
        private IEnumerable<ConcessionLetter> GetLendingConcessionLetterData(Concession concession, User requestor, User bcm)
        {
            var lendingConcession = _lendingManager.GetLendingConcession(concession.ReferenceNumber, requestor);

            var lendingConcessionDetail =  lendingConcession.LendingConcessionDetails.First();
            var legalEntity = _legalEntityRepository.ReadById(lendingConcessionDetail.LegalEntityId.Value);

            var concessionLetter = new ConcessionLetter
            {
                CurrentDate = DateTime.Now.ToString("yyyy-MM-dd"),
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

            return new[]  {concessionLetter};
        }

        /// <summary>
        /// Generates the PDF.
        /// </summary>
        /// <param name="concessionLetterPath">The concession letter path.</param>
        /// <param name="concessionLetter">The concession letter.</param>
        /// <returns></returns>
        private byte[] GeneratePdf(string concessionLetterPath, ConcessionLetter concessionLetter)
        {
            var razorRenderer = new RazorRenderer();
            var html = razorRenderer.Parse(_fileUtiltity.ReadFileText(concessionLetterPath), concessionLetter);

            return _pdfUtility.GeneratePdfFromHtml(html);
        }
    }
}
