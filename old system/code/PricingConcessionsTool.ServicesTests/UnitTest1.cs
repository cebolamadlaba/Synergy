using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PricingConcessionsTool.Services.Interfaces;
using PricingConcessionsTool.Services.Services;
using System.Collections.Generic;
using PricingConcessionsTool.DTO;
using System.IO;

namespace PricingConcessionsTool.ServicesTests
{
    [TestClass]
    public class UnitTest1
    {


        IDocumentService _documentService = null;

        IConcessionService _concessionService = null;


        [TestInitialize]
        public void Initialize()
        {
            _documentService = new DocumentService();
            _concessionService = new ConcessionService();
        }

        [TestMethod]
        public void GenerateDocumentTest()
        {
            var concession = _concessionService.GetConcession(3057);

            var doc = _documentService.GenerateDocument(new List<Concession>() { concession });

            File.WriteAllBytes("Test.pdf", doc);


        }
    }
}
