import { TestBed, inject } from '@angular/core/testing';

import { GlmsBaseService } from './glms-base.service';

describe('GlmsBaseService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GlmsBaseService]
    });
  });

  it('should be created', inject([GlmsBaseService], (service: GlmsBaseService) => {
    expect(service).toBeTruthy();
  }));
});
