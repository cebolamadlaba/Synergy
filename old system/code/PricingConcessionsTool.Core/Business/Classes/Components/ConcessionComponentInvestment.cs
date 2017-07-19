using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.Core.Data;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using System;
using System.Data.Entity;

namespace PricingConcessionsTool.Core.Business.Classes.Components
{

    public class ConcessionComponentInvestment : ConcessionComponent, IConcessionComponent
    {
        public bool CanHandle(ConcessionTypes concessionType)
        {
            return concessionType == ConcessionTypes.Investment;
        }

        public Result RemoveConcession(Concession concession)
        {
            throw new NotImplementedException();
        }

        public Result SaveConcession(Concession concession)
        {
            try
            {
                var concessionInvestment = concession as ConcessionInvestment;

                var result = new Result();

                using (var db = new PricingEntities())
                {
                    tblLegalEntity tblLegalEntity = ConcessionCustomer(concession, db);

                    tblConcession newtblConcession = BaseConcession(concession, db);

                    var newtblConcessionInvestment = new tblConcessionInvestment()
                    {
                     Balance = concessionInvestment.Balance.Value,
                     fkProductTypeId = concessionInvestment.ProductType.ProductTypeId,
                     Term= concessionInvestment.Term.Value,
                     InterestToCustomer= concessionInvestment.InterestToCustomer.Value,
                     
                    };
                  
                    newtblConcession.tblConcessionInvestments.Add(newtblConcessionInvestment);

                    tblLegalEntity.tblConcessions.Add(newtblConcession);

                    if (tblLegalEntity.pkLegalEntityId == 0)
                    {
                        db.Entry(tblLegalEntity).State = EntityState.Added;

                        db.tblLegalEntities.Add(tblLegalEntity);
                    }

                    if (concessionInvestment.ConcessionId == 0)
                    {
                        newtblConcession.fkBCMUserId = GetBCM(newtblConcession.fkRequestorId, db);
                    }


                    db.SaveChanges();

                    concession.RequestorId = newtblConcession.fkRequestorId;
                    concession.BusinessCentreManagerId = newtblConcession.fkBCMUserId;

                    result.Concession = concession;

                    result.ConcessionId = newtblConcession.pkConcessionId;                   

                }

                return result;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
