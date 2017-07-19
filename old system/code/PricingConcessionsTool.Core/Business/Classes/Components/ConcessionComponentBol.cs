using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Core.Data;
using System.Data.Entity;

namespace PricingConcessionsTool.Core.Business.Classes.Components
{
    public class ConcessionComponentBol : ConcessionComponent, IConcessionComponent
    {

        public bool CanHandle(ConcessionTypes concessionType)
        {
            return concessionType == ConcessionTypes.Bol;
        }

        public Result RemoveConcession(Concession concession)
        {
            try
            {
                var concessionBol = concession as ConcessionBol;

                var result = new Result();

                using (var db = new PricingEntities())
                {
                    tblLegalEntity tblLegalEntity = ConcessionCustomer(concession, db);

                    tblConcession newtblConcession = BaseConcession(concession, db);

                    tblConcessionBol newtblConcessionBol = CreateBol(concessionBol);

                    newtblConcession.tblConcessionBols.Add(newtblConcessionBol);

                    tblLegalEntity.tblConcessions.Add(newtblConcession);

                    if (tblLegalEntity.pkLegalEntityId == 0)
                    {
                        db.Entry(tblLegalEntity).State = EntityState.Added;

                        db.tblLegalEntities.Add(tblLegalEntity);
                    }

                    if (concessionBol.ConcessionId == 0)
                    {
                        newtblConcession.fkBCMUserId = concessionBol.BusinessCentreManagerId;
                    }


                    db.SaveChanges();

                    result.ConcessionId = newtblConcession.pkConcessionId;

                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static tblConcessionBol CreateBol(ConcessionBol concessionBol)
        {
            return new tblConcessionBol()
            {
                BolUseId = concessionBol.BusinesOnlineUser.BusinesOnlineUserId,
                fkBusinesOnlineTransactionTypeId = concessionBol.BusinesOnlineTransactionType.BusinesOnlineTransactionTypeId,
                fkTransactionGroupId = concessionBol.TransactionGroup.TransactionGroupId,
                TransactionValue = concessionBol.Value,
                TransactionVolume = concessionBol.Volume,
                Fee = concessionBol.BaseFee

            };
        }
    

        public Result SaveConcession(Concession concession)
        {
            try
            {
                var concessionBol = concession as ConcessionBol;

                var result = new Result();

                using (var db = new PricingEntities())
                {
                    tblLegalEntity tblLegalEntity = ConcessionCustomer(concession, db);

                    tblConcession newtblConcession = BaseConcession(concession, db);

                    var newtblConcessionBol = new tblConcessionBol()
                    {
                        BolUseId = concessionBol.BusinesOnlineUser.BusinesOnlineUserId,
                        fkBusinesOnlineTransactionTypeId = concessionBol.BusinesOnlineTransactionType.BusinesOnlineTransactionTypeId,
                        fkTransactionGroupId = concessionBol.TransactionGroup.TransactionGroupId,
                        TransactionValue = concessionBol.Value,
                        TransactionVolume= concessionBol.Volume,
                        Fee = concessionBol.BaseFee

                    };

                    newtblConcession.tblConcessionBols.Add(newtblConcessionBol);

                    tblLegalEntity.tblConcessions.Add(newtblConcession);

                    if (tblLegalEntity.pkLegalEntityId == 0)
                    {
                        db.Entry(tblLegalEntity).State = EntityState.Added;

                        db.tblLegalEntities.Add(tblLegalEntity);
                    }

                    if (concessionBol.ConcessionId == 0)
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
                throw ex;
            }
        }

    }
}
