import { TestBed, inject } from '@angular/core/testing';

import { CashBaseService } from './cash-base.service';

describe('CashBaseService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CashBaseService]
    });
  });

  it('should be created', inject([CashBaseService], (service: CashBaseService) => {
    expect(service).toBeTruthy();
  }));
});
