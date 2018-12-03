import { TestBed, inject } from '@angular/core/testing';

import { ConcessionLetterService } from './concession-letter.service';

describe('ConcessionLetterService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConcessionLetterService]
    });
  });

  it('should be created', inject([ConcessionLetterService], (service: ConcessionLetterService) => {
    expect(service).toBeTruthy();
  }));
});
