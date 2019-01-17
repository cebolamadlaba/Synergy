using Dapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Common;
using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StandardBank.ConcessionManagement.Repository
{
    public class LegalEntityAddressRepository : ILegalEntityAddressRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        public LegalEntityAddressRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            this._dbConnectionFactory = dbConnectionFactory;
            this._cacheManager = cacheManager;
        }

        public LegalEntityAddress Create(LegalEntityAddress model)
        {
            const string sql = @"INSERT INTO [dbo].[tblLegalEntityAddress] ([fkLegalEntityId],[ContactPerson],[CustomerName],[PostalAddress],[City],[PostalCode],[DateCreated],[Datemodified])
                                VALUES (@LegalEntityId, @ContactPerson, @CustomerName, @PostalAddress, @City, @PostalCode, @DateCreated, @Datemodified) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                        new
                        {
                            LegalEntityId = model.LegalEntityId,
                            ContactPerson = model.ContactPerson,
                            CustomerName = model.CustomerName,
                            PostalAddress = model.PostalAddress,
                            City = model.City,
                            PostalCode = model.PostalCode,
                            DateCreated = model.DateCreated,
                            Datemodified = model.Datemodified,
                        })
                    .Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.LegalEntityAddressRepository.ReadAll);

            return model;
        }

        public void Delete(LegalEntityAddress model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE FROM [dbo].[tblLegalEntityAddress] WHERE [pkLegalEntityAddressId] = @Id",
                    new { model.Id });
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.LegalEntityAddressRepository.ReadAll);
        }

        public IEnumerable<LegalEntityAddress> ReadAll()
        {
            Func<IEnumerable<LegalEntityAddress>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<LegalEntityAddress>(
                        "SELECT [pkLegalEntityAddressId] [Id],[fkLegalEntityId] [LegalEntityId],[ContactPerson],[CustomerName],[PostalAddress],[City],[PostalCode],[DateCreated],[Datemodified] FROM [dbo].[tblLegalEntityAddress]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.LegalEntityAddressRepository.ReadAll);
        }

        public LegalEntityAddress ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        public LegalEntityAddress ReadByLegalEntityId(int legalEntityId)
        {
            return ReadAll().FirstOrDefault(_ => _.LegalEntityId == legalEntityId);
        }

        public void Update(LegalEntityAddress model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblLegalEntityAddress]
                            SET [fkLegalEntityId] = @LegalEntityId, 
                                [ContactPerson] = @ContactPerson, 
                                [CustomerName] = @CustomerName, 
                                [PostalAddress] = @PostalAddress, 
                                [City] = @City, 
                                [PostalCode] = @PostalCode, 
                                [DateCreated] = @DateCreated, 
                                [Datemodified] = @Datemodified
                            WHERE [pkLegalEntityAddressId] = @Id",
                    new
                    {
                        Id = model.Id,
                        LegalEntityId = model.LegalEntityId,
                        ContactPerson = model.ContactPerson,
                        CustomerName = model.CustomerName,
                        PostalAddress = model.PostalAddress,
                        City = model.City,
                        PostalCode = model.PostalCode,
                        DateCreated = model.DateCreated,
                        Datemodified = model.Datemodified,
                    });
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.LegalEntityAddressRepository.ReadAll);
        }
    }
}
