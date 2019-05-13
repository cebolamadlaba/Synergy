import { TestBed, inject } from '@angular/core/testing';

import { BolTradeAeManagementService } from './bol-trade-ae-management.service';

describe('BolTradeAeManagementService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BolTradeAeManagementService]
    });
  });

  it('should be created', inject([BolTradeAeManagementService], (service: BolTradeAeManagementService) => {
    expect(service).toBeTruthy();
  }));
});
