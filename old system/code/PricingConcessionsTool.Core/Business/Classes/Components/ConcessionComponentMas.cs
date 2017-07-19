using PricingConcessionsTool.Core.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Core.Data;
using System.Data.Entity;

namespace PricingConcessionsTool.Core.Business.Classes.Components
{
    public class ConcessionComponentMas : ConcessionComponent, IConcessionComponent
    {
        public bool CanHandle(ConcessionTypes concessionType)
        {
            return concessionType == ConcessionTypes.Mas;
        }

        public Result RemoveConcession(Concession concession)
        {
            throw new NotImplementedException();
        }

        public Result SaveConcession(Concession concession)
        {
            try
            {
                var concessionMas = concession as ConcessionMas;

                var result = new Result();

                using (var db = new PricingEntities())
                {
                    tblLegalEntity tblLegalEntity = ConcessionCustomer(concession, db);

                    tblConcession newtblConcession = BaseConcession(concession, db);

                    var newtblConcessionMas = new tblConcessionMa()
                    {
                        MerchantNumber = concessionMas.MerchantNumber,
                        fkTransactionTypeId =concessionMas.TransactionType.TransactionTypeId,
                        Turnover = concessionMas.Turnover.Value,
                        CommissionRate = concessionMas.CommissionRate.Value,
                    };

                    newtblConcession.tblConcessionMas.Add(newtblConcessionMas);

                    tblLegalEntity.tblConcessions.Add(newtblConcession);

                    if (tblLegalEntity.pkLegalEntityId == 0)
                    {
                        db.Entry(tblLegalEntity).State = EntityState.Added;

                        db.tblLegalEntities.Add(tblLegalEntity);
                    }

                    if (concessionMas.ConcessionId == 0)
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
