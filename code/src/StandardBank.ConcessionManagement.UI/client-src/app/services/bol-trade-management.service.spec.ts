import { TestBed, inject } from '@angular/core/testing';

import { BolTradeManagementServiceService } from './bol-trade-management-service.service';

describe('BolTradeManagementServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BolTradeManagementServiceService]
    });
  });

  it('should be created', inject([BolTradeManagementServiceService], (service: BolTradeManagementServiceService) => {
    expect(service).toBeTruthy();
  }));
});
