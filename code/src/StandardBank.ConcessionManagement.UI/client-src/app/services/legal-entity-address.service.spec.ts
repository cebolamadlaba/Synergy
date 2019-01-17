import { TestBed, inject } from '@angular/core/testing';

import { LegalEntityAddressService } from './legal-entity-address.service';

describe('LegalEntityAddressService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LegalEntityAddressService]
    });
  });

  it('should be created', inject([LegalEntityAddressService], (service: LegalEntityAddressService) => {
    expect(service).toBeTruthy();
  }));
});
