import { TestBed, inject } from '@angular/core/testing';

import { TransactionalConcessionService } from './transactional-concession.service';

describe('TransactionalConcessionService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TransactionalConcessionService]
    });
  });

  it('should be created', inject([TransactionalConcessionService], (service: TransactionalConcessionService) => {
    expect(service).toBeTruthy();
  }));
});
