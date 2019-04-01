using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    public class LegalEntityAddressManager : ILegalEntityAddressManager
    {
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        private readonly ILegalEntityAddressRepository _legalEntityAddressRepository;
        private readonly ILegalEntityRepository _legalEntityRepository;

        public LegalEntityAddressManager(ICacheManager cacheManager,
            IMapper mapper,
            IMemoryCache cache,
            ILegalEntityAddressRepository legalEntityAddressRepository,
            ILegalEntityRepository legalEntityRepository)
        {
            this._cacheManager = cacheManager;
            this._mapper = mapper;
            this._cache = cache;
            this._legalEntityAddressRepository = legalEntityAddressRepository;
            this._legalEntityRepository = legalEntityRepository;
        }

        public LegalEntityAddress CreateLegalEntityAddress(LegalEntityAddress model)
        {
            return this._legalEntityAddressRepository.Create(model);
        }

        public void DeleteLegalEntityAddress(LegalEntityAddress model)
        {
            this._legalEntityAddressRepository.Delete(model);
        }

        public IEnumerable<LegalEntityAddress> GetAllLegalEntityAddress()
        {
            return this._legalEntityAddressRepository.ReadAll();
        }

        public LegalEntityAddress GetLegalEntityAddressById(int id)
        {
            return this._legalEntityAddressRepository.ReadById(id);
        }

        public LegalEntityAddress GetLegalEntityAddressByLegalEntityId(int legalEntityId)
        {
            LegalEntityAddress legalEntityAddress = this._legalEntityAddressRepository.ReadByLegalEntityId(legalEntityId);

            return legalEntityAddress;
        }

        public LegalEntityAddress GetLegalEntityAddressFromLegalEntityRepository(int legalEntityId)
        {
            LegalEntityAddress legalEntityAddress = null;

            LegalEntity legalEntity = this._legalEntityRepository.ReadById(legalEntityId);

            if (legalEntity != null)
                legalEntityAddress = new LegalEntityAddress()
                {
                    LegalEntityId = legalEntity.Id,
                    ContactPerson = legalEntity.ContactPerson,
                    CustomerName = legalEntity.CustomerName,
                    PostalAddress = legalEntity.PostalAddress,
                    City = legalEntity.City,
                    PostalCode = legalEntity.PostalCode,
                    DateCreated = DateTime.Now,
                    Datemodified = null
                };

            return legalEntityAddress;
        }

        public void UpdateLegalEntityAddress(LegalEntityAddress model)
        {
            LegalEntityAddress dbLegalEntityAddress = this.GetLegalEntityAddressByLegalEntityId(model.LegalEntityId);

            if (dbLegalEntityAddress == null)
            {
                model.DateCreated = DateTime.Now;
                this.CreateLegalEntityAddress(model);
            }
            else
            {
                model.Id = dbLegalEntityAddress.Id;
                model.DateCreated = dbLegalEntityAddress.DateCreated;
                model.Datemodified = DateTime.Now;
                this._legalEntityAddressRepository.Update(model);
            }
        }

    }
}
