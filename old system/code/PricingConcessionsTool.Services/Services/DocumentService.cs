using PricingConcessionsTool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

using PricingConcessionsTool.DTO;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Web.Hosting;
using PricingConcessionsTool.Services.Properties;

namespace PricingConcessionsTool.Services.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly Paragraph _paragraphOne = null;
        private readonly Paragraph _paragraphTwo = null;
        private readonly Paragraph _paragraphThree = null;
        private readonly Paragraph _paragraphFour = null;
        private readonly Paragraph _paragraphFive = null;


        public DocumentService()
        {
            _paragraphOne = new Paragraph("I am pleased to inform you that as a valued client you have been granted a concession on pricing for the services listed below. A concession is a discount on your pricing. This means that, subject to the conditions set out below, instead of paying the standard pricing for these service(s), you will be charged at the discounted pricing for the concession period.");
            _paragraphTwo = new Paragraph("Please note that the pricing will revert to our standard pricing  immediately after the expiry of the concession period, or should the conditions set out below not be met,", verdanaBold);
            _paragraphThree = new Paragraph("All our pricing is reviewed on the 1st of January of each year or as otherwise advised by us. Your pricing will therefore be updated on 1 January or as otherwise advised. However, the concession you have been granted will remain the same until the concession period expires, and such concession will be applied to any updated pricing.");
            _paragraphFour = new Paragraph("The concession will be effective once you have indicated your acceptance of this Offer by signing in the space provided below.");
            _paragraphFive = new Paragraph("You are most welcome to contact me, your Relationship Manager, for any questions about pricing, or any of the business banking services we offer.");
        }


        private const string pageTitle = "Concession Letter";
        private const int HorizontalMargin = 40;
        private const int VerticalMargin = 40;

        Font verdanaH1 = FontFactory.GetFont("Verdana", 12, Font.BOLD | Font.NORMAL , new BaseColor(0, 0, 0));
        Font verdanaBold = FontFactory.GetFont("Verdana", 10, Font.BOLD | Font.NORMAL, new BaseColor(0, 0, 0));
        Font verdanaBlueUnderlined = FontFactory.GetFont("Verdana", 10, Font.BOLD | Font.UNDERLINE, new BaseColor(0, 0, 204));
        Font verdanaBlue = FontFactory.GetFont("Verdana", 11, Font.BOLD | Font.NORMAL, new BaseColor(0, 0, 204));

        Font blank = FontFactory.GetFont("Verdana", 10, Font.BOLD | Font.NORMAL, BaseColor.WHITE);

        private const string ConfirmationPragraphTitle = "Offer of a Concession on pricing (“Fees/Interest Rates/Offer”) by the Standard Bank of South Africa Limited (“we/us/our”) to {0}";

        public byte[] GenerateDocument(List<Concession> concessions)
        {
            byte[] document;

            var concession = concessions.FirstOrDefault();

            using (var outputMemoryStream = new MemoryStream())
            {
                using (var pdfDocument = new Document(PageSize.A4, HorizontalMargin, HorizontalMargin, VerticalMargin, VerticalMargin))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, outputMemoryStream);
                    pdfWriter.CloseStream = false;
                    // pdfWriter.PageEvent = new PrintHeaderFooter { Title = pageTitle };

                    pdfDocument.Open();

                    //float indentation = pdfDocument.PageSize.Width; 

                    string root = HostingEnvironment.MapPath("~");

                    var imagePath = root + "/Images/stblogo.png";

                    Image logo = Image.GetInstance(imagePath);
                    logo.Alignment = Element.ALIGN_RIGHT;
                    logo.ScaleToFit(100f, 100f);

                    pdfDocument.Add(logo);

                    Paragraph centerName = new Paragraph("Personal & Business  Banking",verdanaBlue);
                    //  centerName.IndentationRight = indentation;
                    centerName.Alignment = 2;
                    pdfDocument.Add(centerName);

                    pdfDocument.Add(Chunk.NEWLINE);

                    Paragraph date = new Paragraph(DateTime.Now.ToShortDateString());
                    date.Alignment = 2;
                    pdfDocument.Add(date);

                    pdfDocument.Add(Chunk.NEWLINE);


                    Paragraph clientNumber = new Paragraph();// string.Format("Client Number:{0}", "Set cust No"));

                    Phrase clientNumberLabel = new Phrase("Client Number:", verdanaBold);

                    Phrase clientNumberText = new Phrase(concession.Customer.CustomerNumber);

                    clientNumber.Add(clientNumberLabel);
                    clientNumber.Add(clientNumberText);


                    //  centerName.IndentationRight = indentation;
                    pdfDocument.Add(clientNumber);

                    pdfDocument.Add(Chunk.NEWLINE);


                    Paragraph clientName = new Paragraph(string.Format("Dear {0}", concession.Customer.CustomerName));

                    //  centerName.IndentationRight = indentation;
                    pdfDocument.Add(clientName);

                    pdfDocument.Add(Chunk.NEWLINE);

                    //pdfDocument.Add(new Paragraph(ConfirmationPragraphTitle, verdanaBold));
                    pdfDocument.Add(new Paragraph(string.Format(ConfirmationPragraphTitle, concession.Customer.CustomerName), verdanaBold));

                    pdfDocument.Add(Chunk.NEWLINE);
                    pdfDocument.Add(_paragraphOne);


                    pdfDocument.Add(Chunk.NEWLINE);
                    pdfDocument.Add(_paragraphTwo);

                    pdfDocument.Add(Chunk.NEWLINE);
                    pdfDocument.Add(_paragraphThree);

                    pdfDocument.Add(Chunk.NEWLINE);
                    pdfDocument.Add(_paragraphFour);

                    pdfDocument.Add(Chunk.NEWLINE);

                    Paragraph TandC = new Paragraph();// string.Format("Client Number:{0}", "Set cust No"));

                    TandC.Add(new Phrase("Our Terms & Conditions and the standard fees are available at "));
                    TandC.Add(new Phrase("www.standardbank.co.za", verdanaBlueUnderlined));
                    pdfDocument.Add(TandC);
                    pdfDocument.Add(Chunk.NEWLINE);

                    pdfDocument.Add(new Paragraph(_paragraphFive));
                    pdfDocument.Add(Chunk.NEWLINE);
                    pdfDocument.Add(new Paragraph("Yours sincerely,"));
                    pdfDocument.Add(Chunk.NEWLINE);


                    var requestorParagraph = new Paragraph();// string.Format("Client Number:{0}", "Set cust No"));

                    var businessSuiteHeadParagraph = new Paragraph();

                    requestorParagraph.Add(new Phrase(concession.Requestor.FullName, verdanaBlue));
                    requestorParagraph.Add(Chunk.NEWLINE);
                    requestorParagraph.Add(new Phrase(concession.Requestor.Role));
                    requestorParagraph.Add(Chunk.NEWLINE);

                    var table = new PdfPTable(2);
                    table.WidthPercentage = 100;

                    var requestorCell = new PdfPCell(requestorParagraph);
                    requestorCell.BorderColor = BaseColor.WHITE;
                    requestorCell.BorderWidth = 0;

                    businessSuiteHeadParagraph.Add(new Phrase(concession.BusinessCentreManager.FullName, verdanaBlue));
                    businessSuiteHeadParagraph.Add(Chunk.NEWLINE);
                    businessSuiteHeadParagraph.Add(new Phrase(concession.BusinessCentreManager.Role));
                    businessSuiteHeadParagraph.Add(Chunk.NEWLINE);

                    var businessHeadCell = new PdfPCell(businessSuiteHeadParagraph);
                    businessHeadCell.BorderColor = BaseColor.WHITE;
                    businessHeadCell.BorderWidth = 0;

                    table.AddCell(requestorCell);
                    table.AddCell(businessHeadCell);                   
                    pdfDocument.Add(table);

                    pdfDocument.NewPage();

                    PdfPTable RiskGroup = new PdfPTable(2);
                    table.WidthPercentage = 50;
                    var RGCell = new PdfPCell(new Phrase("Risk Group"));
                    RGCell.BackgroundColor = BaseColor.GRAY;
                    RiskGroup.AddCell(RGCell);
                    RiskGroup.AddCell(string.Format("{0}",concession.Customer.RiskGroupNumber));
                    RiskGroup.HorizontalAlignment = 0;
                    pdfDocument.Add(RiskGroup);

                    pdfDocument.Add(Chunk.NEWLINE);
                    foreach (var c in concessions)
                    {                      
                        AddConcessionDatatable(pdfDocument, c);                                                               
                        AddConditionOfGrantTable(pdfDocument, c);                         
                    }

                    pdfDocument.NewPage();

                    pdfDocument.Add(new Paragraph("Terms and Conditions", verdanaBold));
                    var termsList = new Paragraph();
                    termsList.Add(Chunk.NEWLINE);

                    termsList.Add(Chunk.NEWLINE);
                    termsList.Add(new Phrase("1.	This Offer replaces all previous pricing communications to you."));
                    termsList.Add(Chunk.NEWLINE);
                    termsList.Add(new Phrase("2.	All pricing is quoted inclusive of VAT (unless otherwise indicated) and is valid for 14 days from the date of offer, after which the bank reserves the right to review the offer if not taken up."));
                    termsList.Add(Chunk.NEWLINE);
                    termsList.Add(new Phrase("3.	The concession is a discount that is given to you on your fees. Your fees may still change from time to time in accordance with our Terms and Conditions. The concession will apply to any updated fees. "));
                    termsList.Add(Chunk.NEWLINE);
                    termsList.Add(new Phrase("4.	The concession is only valid for the concession period or until any conditions that have been imposed are not fulfilled. "));
                    termsList.Add(Chunk.NEWLINE);
                    termsList.Add(new Phrase("5.	If there is a conflict between the terms of this Offer and the Terms and Conditions, the terms of this Offer, only in relation to fees, and only to the extent of such conflict, shall prevail. The Terms and Conditions shall prevail in respect of all other matters. "));
                    termsList.Add(Chunk.NEWLINE);
                    termsList.Add(new Phrase("6.	This Offer is addendum to and not a novation of the Terms and Conditions. All the terms of the Terms and Conditions will remain of full force and effect, unless specifically amended in this Offer."));
                    termsList.Add(Chunk.NEWLINE);
                    pdfDocument.Add(termsList);
                    termsList.Add(Chunk.NEWLINE);
                    termsList.Add(Chunk.NEWLINE);
                    pdfDocument.Add(new Paragraph("I/We hereby confirm my acceptance of this Offer.", verdanaBold));
                    pdfDocument.Add(Chunk.NEWLINE);
                    pdfDocument.Add(Chunk.NEWLINE);

                    PdfPTable signaturesTable = new PdfPTable(3);
                    signaturesTable.WidthPercentage = 100;

                    signaturesTable.AddCell(new PdfPCell(new Phrase("place - holder", blank)));
                    signaturesTable.AddCell(new PdfPCell(new Phrase("place - holder", blank)));
                    signaturesTable.AddCell(new PdfPCell(new Phrase("place - holder", blank)));

                    signaturesTable.AddCell(new PdfPCell(new Phrase("place - holder", blank)));
                    signaturesTable.AddCell(new PdfPCell(new Phrase("place - holder", blank)));
                    signaturesTable.AddCell(new PdfPCell(new Phrase("place - holder",blank)));

                    var clientSignature = new PdfPCell(new Phrase("Client's Authorised Signatory", verdanaBold));
                    clientSignature.BorderColorBottom = BaseColor.WHITE;
                    signaturesTable.AddCell(clientSignature);

                    var clientSignature2 = new PdfPCell(new Phrase("Client's Signature", verdanaBold));
                    clientSignature2.BorderColorBottom = BaseColor.WHITE;
                    signaturesTable.AddCell(clientSignature2);

                    var clientSignatureDate = new PdfPCell(new Phrase("Date", verdanaBold));
                    clientSignatureDate.BorderColorBottom = BaseColor.WHITE;
                    signaturesTable.AddCell(clientSignatureDate);

                    pdfDocument.Add(signaturesTable);

                    pdfDocument.AddAuthor("Test");
                    pdfDocument.AddTitle("Test");


                }


                document = new byte[outputMemoryStream.Position];
                outputMemoryStream.Position = 0;
                outputMemoryStream.Read(document, 0, document.Length);
            }

            return document;
        }

        private void AddConcessionDatatable(Document pdfDocument, Concession c)
        {
            PdfPTable table = null;
            var headerOne = new PdfPCell();
            headerOne.BackgroundColor = BaseColor.GRAY;
            var headerTwo = new PdfPCell();
            headerTwo.BackgroundColor = BaseColor.GRAY;
            var headerThree = new PdfPCell();
            headerThree.BackgroundColor = BaseColor.GRAY;
            var headerFour = new PdfPCell();
            headerFour.BackgroundColor = BaseColor.GRAY;
            var headerFive = new PdfPCell();
            headerFive.BackgroundColor = BaseColor.GRAY;
            var headerSix = new PdfPCell();
            headerSix.BackgroundColor = BaseColor.GRAY;

            pdfDocument.Add(new Phrase(c.ConcessionTypeDescription, verdanaH1));

            switch (c.ConcessionType)
            {

                case DTO.Enums.ConcessionTypes.Lending:
                    var lending = c as ConcessionLending;
                    table = new PdfPTable(6);

                    table.AddCell(new Phrase("Product Type", verdanaBold));
                    table.AddCell(new Phrase("Product Account Number", verdanaBold));
                    table.AddCell(new Phrase("Channel / Fee Type", verdanaBold));
                    table.AddCell(new Phrase("Fee / Interest to Client", verdanaBold));
                    table.AddCell(new Phrase("Concession Start Date", verdanaBold));
                    table.AddCell(new Phrase("Concession End Date", verdanaBold));

                    table.AddCell(new Phrase(string.Format("{0}", lending.ProductType.Description)));
                    table.AddCell(new Phrase(string.Format("{0}", string.Join(",", c.AccountList))));

                    if (lending.ProductType.ProductTypeId != 1)
                    {
                        table.AddCell(new Phrase(string.Format("{0}", lending.MarginAbovePrime)));
                        table.AddCell(new Phrase(string.Format("{0}", lending.MarginAbovePrime)));

                    }
                    else
                    {
                        PdfPTable overDraftTable = new PdfPTable(2);
                        overDraftTable.WidthPercentage = 100;
                        overDraftTable.AddCell(new Phrase("MarginAbovePrime", verdanaBold));
                        overDraftTable.AddCell(new Phrase(string.Format("{0}", lending.MarginAbovePrime)));

                        overDraftTable.AddCell(new Phrase("Initiation Fee", verdanaBold));
                        overDraftTable.AddCell(new Phrase(string.Format("{0}", lending.InitiationFee)));

                        overDraftTable.AddCell(new Phrase("Review Fee", verdanaBold));
                        overDraftTable.AddCell(new Phrase(string.Format("{0}", lending.ReviewFee)));

                        overDraftTable.AddCell(new Phrase("Unutilized Facility Fee", verdanaBold));
                        overDraftTable.AddCell(new Phrase(string.Format("{0}", lending.UnutilizedFacilityFee)));

                        table.AddCell(overDraftTable);
                    }
                    table.AddCell(new Phrase(string.Format("{0}", lending.ConcessionDate.Value.ToShortDateString())));
                    table.AddCell(new Phrase(string.Format("{0}", lending.ExpiryDate.Value.ToShortDateString())));
                    ;
                    break;
                case DTO.Enums.ConcessionTypes.Investment:
                    var investment = c as ConcessionInvestment;
                    table = new PdfPTable(6);
                    table.AddCell(new Phrase("Product Type", verdanaBold));
                    table.AddCell(new Phrase("Product Account Number", verdanaBold));
                    table.AddCell(new Phrase("Channel / Fee Type", verdanaBold));
                    table.AddCell(new Phrase("Fee / Interest to Client", verdanaBold));
                    table.AddCell(new Phrase("Concession Start Date", verdanaBold));
                    table.AddCell(new Phrase("Concession End Date", verdanaBold));

                    table.AddCell(new Phrase(string.Format("{0}", investment.ProductType.Description)));
                    table.AddCell(new Phrase(string.Format("{0}", string.Join(",", c.AccountList))));
                    table.AddCell(new Phrase(string.Format("{0}", investment.InterestToCustomer)));
                    table.AddCell(new Phrase(string.Format("{0}", investment.InterestToCustomer)));
                    table.AddCell(new Phrase(string.Format("{0}", investment.ConcessionDate.Value.ToShortDateString())));
                    table.AddCell(new Phrase(string.Format("{0}", investment.ExpiryDate.Value.ToShortDateString())));
                    break;
                case DTO.Enums.ConcessionTypes.Mas:
                    var mas = c as ConcessionMas;
                    table = new PdfPTable(5);
                    table.AddCell(new Phrase("Merchant Number", verdanaBold));
                    table.AddCell(new Phrase("Channel/Fee Type", verdanaBold));
                    table.AddCell(new Phrase("Fee/Rate (Incl VAT)", verdanaBold));
                    table.AddCell(new Phrase("Concession Start Date", verdanaBold));
                    table.AddCell(new Phrase("Concession End Date", verdanaBold));

                    table.AddCell(new Phrase(string.Format("{0}", mas.MerchantNumber)));
                    table.AddCell(new Phrase(string.Format("{0}", mas.TransactionType.Description)));
                    table.AddCell(new Phrase(string.Format("{0}", mas.CommissionRate)));
                    table.AddCell(new Phrase(string.Format("{0}", mas.DateApproved.Value.ToShortDateString())));
                    table.AddCell(new Phrase(string.Format("{0}", mas.ExpiryDate.Value.ToShortDateString())));
                    break;
                case DTO.Enums.ConcessionTypes.Trade:
                    var trade = c as ConcessionTrade;
                    table = new PdfPTable(5);
                    table.AddCell(new Phrase("Product Account Number", verdanaBold));
                    table.AddCell(new Phrase("Channel Type", verdanaBold));
                    table.AddCell(new Phrase("Fee/Rate (Incl VAT)", verdanaBold));
                    table.AddCell(new Phrase("Concession Start Date", verdanaBold));
                    table.AddCell(new Phrase("Concession End Date", verdanaBold));

                    table.AddCell(new Phrase(string.Format("{0}", string.Join(",", c.AccountList))));
                    table.AddCell(new Phrase(string.Format("{0}", trade.ChannelType.Description)));
                    table.AddCell(new Phrase(string.Format("{0} + {1}", trade.BaseRate, trade.AdValorem)));
                    table.AddCell(new Phrase(string.Format("{0}", trade.DateApproved.Value.ToShortDateString())));
                    table.AddCell(new Phrase(string.Format("{0}", trade.ExpiryDate.Value.ToShortDateString())));
                    break;
                case DTO.Enums.ConcessionTypes.Transactional:
                    var transactional = c as ConcessionTransactional;
                    table = new PdfPTable(5);
                    table.AddCell(new Phrase("Product Account Number", verdanaBold));
                    table.AddCell(new Phrase("Channel Type", verdanaBold));
                    table.AddCell(new Phrase("Fee/Rate (Incl VAT)", verdanaBold));
                    table.AddCell(new Phrase("Concession Start Date", verdanaBold));
                    table.AddCell(new Phrase("Concession End Date", verdanaBold));

                    table.AddCell(new Phrase(string.Format("{0}", string.Join(",", c.AccountList))));
                    table.AddCell(new Phrase(string.Format("{0}", transactional.ChannelType.Description)));
                    table.AddCell(new Phrase(string.Format("{0} + {1}", transactional.BaseRate, transactional.AdValorem)));
                    table.AddCell(new Phrase(string.Format("{0}", transactional.DateApproved.Value.ToShortDateString())));
                    table.AddCell(new Phrase(string.Format("{0}", transactional.ExpiryDate.Value.ToShortDateString())));
                    break;
                case DTO.Enums.ConcessionTypes.Bol:
                    var bol = c as ConcessionBol;
                    table = new PdfPTable(5);
                    table.AddCell(new Phrase("BOL User ID", verdanaBold));
                    table.AddCell(new Phrase("Channel/Fee Type", verdanaBold));
                    table.AddCell(new Phrase("Fee/Rate (Incl VAT)", verdanaBold));
                    table.AddCell(new Phrase("Concession Start Date", verdanaBold));
                    table.AddCell(new Phrase("Concession End Date", verdanaBold));

                    table.AddCell(new Phrase(string.Format("{0}", bol.BusinesOnlineUser.UserName)));
                    table.AddCell(new Phrase(string.Format("{0}", bol.BusinesOnlineTransactionType.Description)));
                    table.AddCell(new Phrase(string.Format("{0} + {1}", bol.BaseFee, "No Ad Valorem")));
                    table.AddCell(new Phrase(string.Format("{0}", bol.DateApproved.Value.ToShortDateString())));
                    table.AddCell(new Phrase(string.Format("{0}", bol.ExpiryDate.Value.ToShortDateString())));
                    break;
                case DTO.Enums.ConcessionTypes.Cash:
                    var cash = c as ConcessionCash;
                    table = new PdfPTable(5);
                    table.AddCell(new Phrase("Product Account Number", verdanaBold));
                    table.AddCell(new Phrase("Channel Type", verdanaBold));
                    table.AddCell(new Phrase("Fee/Rate (Incl VAT)", verdanaBold));
                    table.AddCell(new Phrase("Concession Start Date", verdanaBold));
                    table.AddCell(new Phrase("Concession End Date", verdanaBold));

                    table.AddCell(new Phrase(string.Format("{0}", string.Join(",", c.AccountList))));
                    table.AddCell(new Phrase(string.Format("{0}", cash.ChannelType.Description)));
                    table.AddCell(new Phrase(string.Format("{0} + {1}", cash.BaseRate, cash.AdValorem)));
                    table.AddCell(new Phrase(string.Format("{0}", cash.DateApproved.Value.ToShortDateString())));
                    table.AddCell(new Phrase(string.Format("{0}", cash.ExpiryDate.Value.ToShortDateString())));

                    break;
            }
            table.WidthPercentage = 100;
            pdfDocument.Add(table);
        }

        private void AddConditionOfGrantTable(Document pdfDocument, Concession concession)
        {
            if (concession.ConditionList.Count > 0)
            {
                pdfDocument.Add(Chunk.NEWLINE);
                PdfPTable table = new PdfPTable(3);
                PdfPCell concessionTypeCell = new PdfPCell(new Phrase("Conditions of Grant", verdanaBold));
                concessionTypeCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                concessionTypeCell.Colspan = 3;
                concessionTypeCell.HorizontalAlignment = 3;
                table.AddCell(concessionTypeCell);
                table.WidthPercentage = 100;

                var conditionMeasureCell = new PdfPCell(new Phrase("Condition Measure", verdanaBold));
                conditionMeasureCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(conditionMeasureCell);

                var conditionProductCell = new PdfPCell(new Phrase("Condition Product", verdanaBold));
                conditionProductCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(conditionProductCell);
                var valueCell = new PdfPCell(new Phrase("Value", verdanaBold));
                valueCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(valueCell);

                foreach (var condition in concession.ConditionList)
                {
                    table.AddCell(condition.ConditionType.Description);
                    table.AddCell(condition.ConditionProduct.Description);
                    table.AddCell(string.Format("{0}", condition.Value));
                }
                pdfDocument.Add(Chunk.NEWLINE);
                pdfDocument.Add(table);
            }
        }

        public string SaveDocument(byte[] pdfData)
        {
            var fileName = string.Format("{0}.pdf", Guid.NewGuid().ToString().Replace("-", string.Empty));

            string root = HostingEnvironment.MapPath("~");

            if(!Directory.Exists(root + "/" + Settings.Default.LettersFolder))
            {
                Directory.CreateDirectory(root + "/" + Settings.Default.LettersFolder);
            }

            File.WriteAllBytes(root + "/" + Settings.Default.LettersFolder + "/" + fileName, pdfData);
          
            return string.Format("{0}/{1}", Settings.Default.LettersFolder, fileName);
        }
    }



    public class PrintHeaderFooter : PdfPageEventHelper
    {
        private PdfContentByte pdfContent;
        private PdfTemplate pageNumberTemplate;
        private BaseFont baseFont;
        private DateTime printTime;

        public string Title { get; set; }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            printTime = DateTime.Now;
            baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            pdfContent = writer.DirectContent;
            pageNumberTemplate = pdfContent.CreateTemplate(50, 50);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);

            Rectangle pageSize = document.PageSize;

            if (Title != string.Empty)
            {
                pdfContent.BeginText();
                pdfContent.SetFontAndSize(baseFont, 10);
                pdfContent.SetRGBColorFill(0, 0, 0);
                pdfContent.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetTop(40));
                pdfContent.ShowText(Title);
                pdfContent.EndText();
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            int pageN = writer.PageNumber;
            string text = pageN + " - ";
            float len = baseFont.GetWidthPoint(text, 8);

            Rectangle pageSize = document.PageSize;
            pdfContent = writer.DirectContent;
            pdfContent.SetRGBColorFill(100, 100, 100);

            pdfContent.BeginText();
            pdfContent.SetFontAndSize(baseFont, 8);
            pdfContent.SetTextMatrix(pageSize.Width / 2, pageSize.GetBottom(30));
            pdfContent.ShowText(text);
            pdfContent.EndText();

            pdfContent.AddTemplate(pageNumberTemplate, (pageSize.Width / 2) + len, pageSize.GetBottom(30));

            pdfContent.BeginText();
            pdfContent.SetFontAndSize(baseFont, 8);
            pdfContent.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, printTime.ToString(), pageSize.GetRight(40), pageSize.GetBottom(30), 0);
            pdfContent.EndText();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            pageNumberTemplate.BeginText();
            pageNumberTemplate.SetFontAndSize(baseFont, 8);
            pageNumberTemplate.SetTextMatrix(0, 0);
            pageNumberTemplate.ShowText(string.Empty + (writer.PageNumber - 1));
            pageNumberTemplate.EndText();
        }
    }
}
