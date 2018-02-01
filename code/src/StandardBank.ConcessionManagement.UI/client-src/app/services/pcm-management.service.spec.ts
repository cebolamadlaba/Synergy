import { TestBed, inject } from '@angular/core/testing';

import { PcmManagementService } from './pcm-management.service';

describe('PcmManagementService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PcmManagementService]
    });
  });

  it('should be created', inject([PcmManagementService], (service: PcmManagementService) => {
    expect(service).toBeTruthy();
  }));
});
