using System;
using FluentEmail.Razor;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;

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
        /// Initializes a new instance of the <see cref="LetterGeneratorManager"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        /// <param name="fileUtiltity">The file utiltity.</param>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="pdfUtility">The PDF utility.</param>
        public LetterGeneratorManager(IConfigurationData configurationData, IFileUtiltity fileUtiltity,
            IConcessionManager concessionManager, IPdfUtility pdfUtility)
        {
            _templatePath = configurationData.LetterTemplatePath;
            _fileUtiltity = fileUtiltity;
            _concessionManager = concessionManager;
            _pdfUtility = pdfUtility;
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
            
            //TODO: Get concession details based on concession type

            //TODO: Group by cusomter and generate a concession letter for each customer associated with the concession 

            //TODO: Combine all the concession letters into one PDF for printing

            return ReturnTestFile(concessionLetterPath);
            //return _fileUtiltity.ReadFileBytes(concessionLetterPath);
        }

        /// <summary>
        /// Returns the test file.
        /// </summary>
        /// <param name="concessionLetterPath">The concession letter path.</param>
        /// <returns></returns>
        private byte[] ReturnTestFile(string concessionLetterPath)
        {
            var razorRenderer = new RazorRenderer();
            var html = razorRenderer.Parse(_fileUtiltity.ReadFileText(concessionLetterPath), new
            {
                ClientName = "Test Client 101",
                ClientNumber = "555222999",
                CurrentDate = DateTime.Now.ToString("yyyy-MM-dd"),
                TemplatePath = _templatePath
            });

            return _pdfUtility.GeneratePdfFromHtml(html);
        }
    }
}
