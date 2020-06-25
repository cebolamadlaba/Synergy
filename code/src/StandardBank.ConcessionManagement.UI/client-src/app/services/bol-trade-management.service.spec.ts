import { TestBed, inject } from '@angular/core/testing';

import { BolTradeManagementService } from './bol-trade-management.service';

describe('BolTradeManagementServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
        providers: [BolTradeManagementService]
    });
  });

  it('should be created', inject([BolTradeManagementService], (service: BolTradeManagementService) => {
    expect(service).toBeTruthy();
  }));
});
