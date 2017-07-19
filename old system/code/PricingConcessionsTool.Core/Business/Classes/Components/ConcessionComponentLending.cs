using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.Core.Data;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using System;
using System.Data.Entity;
using System.Linq;


namespace PricingConcessionsTool.Core.Business.Classes.Components
{

    public class ConcessionComponentLending : ConcessionComponent, IConcessionComponent
    {
        public bool CanHandle(ConcessionTypes concessionType)
        {
            return concessionType == ConcessionTypes.Lending;
        }

        public Result RemoveConcession(Concession concession)
        {
            throw new NotImplementedException();
        }

        public Result SaveConcession(Concession concession)
        {
            try
            {
                var concessionLending = concession as ConcessionLending;

                var result = new Result();

                using (var db = new PricingEntities())
                {
                    tblLegalEntity tblLegalEntity = ConcessionCustomer(concession, db);

                    tblConcession newtblConcession = BaseConcession(concession, db);

                    var newtblConcessionLending = new tblConcessionLending()
                    {
                        fkProductTypeId = concessionLending.ProductType.ProductTypeId,
                        Limit = concessionLending.Limit.Value,
                        Term = concessionLending.Term,
                        MarginToPrime = concessionLending.MarginAbovePrime,
                        ReviewFee = concessionLending.ReviewFee,
                        UFFFee = concessionLending.UnutilizedFacilityFee,
                        InitiationFee = concessionLending.InitiationFee
                    };

                    if(concessionLending.ReviewFeeType!=null)
                    {
                        newtblConcessionLending.fkReviewFeeTypeId = concessionLending.ReviewFeeType.ReviewFeeTypeId;
                    }

                    newtblConcession.tblConcessionLendings.Add(newtblConcessionLending);

                    tblLegalEntity.tblConcessions.Add(newtblConcession);

                    if (tblLegalEntity.pkLegalEntityId == 0)
                    {
                        db.Entry(tblLegalEntity).State = EntityState.Added;

                        db.tblLegalEntities.Add(tblLegalEntity);
                    }

                    if(concessionLending.ConcessionId==0)
                    {
                        newtblConcession.fkBCMUserId = GetBCM(newtblConcession.fkRequestorId, db);
                    }
                    

                    db.SaveChanges();

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
