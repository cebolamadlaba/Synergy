using System;
using System.Collections.Generic;
using System.Text;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Interface.Common;
using Dapper;

using System.Collections.Generic;
using System.Linq;

namespace StandardBank.ConcessionManagement.Repository
{
    public class PrimeRateRepository: IPrimeRateRepository
    {

        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CentreRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public PrimeRateRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IEnumerable<string> PrimeRate(DateTime reftime)
        {

            using (var db = _dbConnectionFactory.Connection())
            {
                var result = db.Query<string>(
                    @"select top 1 isnull(prm_prime_rate,0) 
                    from rtblprimerate 
                    where  prm_active_from <= @reftime
                    order by prm_active_from desc",
                    new { reftime }).FirstOrDefault();
              

                return new List<string> { double.Parse(result).ToString("F2")};
            }         

        }

    }
}
