using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Core.Business.Classes
{
    public class LogContext : IlogContext
    {
        public void LogException(Exception ex)
        {
            using (var db = new PricingEntities())
            {

                var log = new tblExceptionLog()
                {
                    Logdate = DateTime.Now,
                    ExceptionType = ex.GetType().Name.ToString(),
                    ExceptionMessage = !string.IsNullOrWhiteSpace(ex.Message) && ex.Message.Length > 100
                        ? ex.Message.Substring(0, 99)
                        : ex.Message,
                    ExceptionSource = ex.StackTrace
                };

                if (ex is DbEntityValidationException)
                {
                    var dbEx = ex as DbEntityValidationException;

                    var sb = new StringBuilder();

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {

                            sb.Append(string.Format("{0}-{1}", validationError.PropertyName, validationError.ErrorMessage));
                        }
                    }

                    log.ExceptionData = sb.ToString();
                }

                db.tblExceptionLogs.Add(log);

                db.SaveChanges();
            }
        }
    }
}
