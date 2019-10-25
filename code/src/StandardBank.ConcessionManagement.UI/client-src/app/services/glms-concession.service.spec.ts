import { TestBed, inject } from '@angular/core/testing';

import { GlmsConcessionService } from './glms-concession.service';

describe('GlmsConcessionService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GlmsConcessionService]
    });
  });

  it('should be created', inject([GlmsConcessionService], (service: GlmsConcessionService) => {
    expect(service).toBeTruthy();
  }));
});
