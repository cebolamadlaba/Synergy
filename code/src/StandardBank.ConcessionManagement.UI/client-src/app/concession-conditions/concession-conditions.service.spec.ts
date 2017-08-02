import { TestBed, inject } from '@angular/core/testing';

import { ConcessionConditionsService } from './concession-conditions.service';

describe('ConcessionConditionsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConcessionConditionsService]
    });
  });

  it('should be created', inject([ConcessionConditionsService], (service: ConcessionConditionsService) => {
    expect(service).toBeTruthy();
  }));
});
