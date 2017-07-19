using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingConcessionsTool.Core.Data;
using System.Data.Entity;

namespace PricingConcessionsTool.Core.Business.Classes.Components
{
    public class ConcessionComponentCash : ConcessionComponent, IConcessionComponent
    {
        public bool CanHandle(ConcessionTypes concessionType)
        {
            return concessionType == ConcessionTypes.Cash;
        }

        public Result RemoveConcession(Concession concession)
        {
            throw new NotImplementedException();
        }

        public Result SaveConcession(Concession concession)
        {
            try
            {
                var concessionCash = concession as ConcessionCash;

                var result = new Result();

                using (var db = new PricingEntities())
                {
                    tblLegalEntity tblLegalEntity = ConcessionCustomer(concession, db);

                    tblConcession newtblConcession = BaseConcession(concession, db);

                    var newtblConcessionCash = new tblConcessionCash()
                    {
                        AdValorem = concessionCash.AdValorem,
                        CashValue = concessionCash.Value,
                        CashVolume = concessionCash.Volume,
                        fkBaseRateId = concessionCash.BaseRate.BaseRateId,
                        fkChannelTypeId= concessionCash.ChannelType.ChannelTypeId,
                         TableNumber= concessionCash.TableNumber
                    };

                    newtblConcession.tblConcessionCashes.Add(newtblConcessionCash);

                    tblLegalEntity.tblConcessions.Add(newtblConcession);

                    if (tblLegalEntity.pkLegalEntityId == 0)
                    {
                        db.Entry(tblLegalEntity).State = EntityState.Added;

                        db.tblLegalEntities.Add(tblLegalEntity);
                    }

                    if (concessionCash.ConcessionId == 0)
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

