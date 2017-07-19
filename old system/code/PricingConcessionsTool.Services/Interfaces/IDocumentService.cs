using PricingConcessionsTool.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Services.Interfaces
{
    public interface IDocumentService
    {
        byte[] GenerateDocument(List<Concession> concessions);
        string SaveDocument(byte[] pdfData);
    }
}
