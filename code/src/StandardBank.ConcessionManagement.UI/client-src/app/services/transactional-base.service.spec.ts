import { TestBed, inject } from '@angular/core/testing';

import { TransactionalBaseService } from './transactional-base.service';

describe('TransactionalBaseService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TransactionalBaseService]
    });
  });

  it('should be created', inject([TransactionalBaseService], (service: TransactionalBaseService) => {
    expect(service).toBeTruthy();
  }));
});
