import { TestBed, inject } from '@angular/core/testing';

import { InboxConcessionCountService } from './inbox-concession-count.service';

describe('InboxConcessionCountService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [InboxConcessionCountService]
    });
  });

  it('should be created', inject([InboxConcessionCountService], (service: InboxConcessionCountService) => {
    expect(service).toBeTruthy();
  }));
});
