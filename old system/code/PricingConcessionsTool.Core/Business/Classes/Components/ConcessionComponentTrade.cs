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
    public class ConcessionComponentTrade : ConcessionComponent, IConcessionComponent
    {
        public bool CanHandle(ConcessionTypes concessionType)
        {
            return concessionType == ConcessionTypes.Trade;
        }

        public Result RemoveConcession(Concession concession)
        {
            throw new NotImplementedException();
        }

        public Result SaveConcession(Concession concession)
        {
            try
            {
                var concessionTrade = concession as ConcessionTrade;

                var result = new Result();

                using (var db = new PricingEntities())
                {
                    tblLegalEntity tblLegalEntity = ConcessionCustomer(concession, db);

                    tblConcession newtblConcession = BaseConcession(concession, db);

                    var newtblConcessionTrade = new tblConcessionTrade()
                    {
                        AdValorem = concessionTrade.AdValorem,
                        fkTransactionTypeId = concessionTrade.TransactionType.TransactionTypeId,
                        fkBaseRateId = concessionTrade.BaseRate.BaseRateId,
                        fkChannelTypeId = concessionTrade.ChannelType.ChannelTypeId,
                        TableNumber = concessionTrade.TableNumber,
                        TransactionValue= concessionTrade.Value,
                        TransactionVolume= concessionTrade.Volume.Value
                    };

                    newtblConcession.tblConcessionTrades.Add(newtblConcessionTrade);

                    tblLegalEntity.tblConcessions.Add(newtblConcession);

                    if (tblLegalEntity.pkLegalEntityId == 0)
                    {
                        db.Entry(tblLegalEntity).State = EntityState.Added;

                        db.tblLegalEntities.Add(tblLegalEntity);
                    }

                    if (concessionTrade.ConcessionId == 0)
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
