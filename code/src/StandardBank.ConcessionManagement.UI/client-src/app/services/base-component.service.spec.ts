import { TestBed, inject } from '@angular/core/testing';

import { BaseComponentService } from './base-component.service';

describe('BaseComponentService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BaseComponentService]
    });
  });

  it('should be created', inject([BaseComponentService], (service: BaseComponentService) => {
    expect(service).toBeTruthy();
  }));
});
