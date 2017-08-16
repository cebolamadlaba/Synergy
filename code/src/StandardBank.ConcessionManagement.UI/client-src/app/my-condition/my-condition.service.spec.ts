import { TestBed, inject } from '@angular/core/testing';

import { MyConditionService } from './my-condition.service';

describe('MyConditionService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MyConditionService]
    });
  });

  it('should be created', inject([MyConditionService], (service: MyConditionService) => {
    expect(service).toBeTruthy();
  }));
});
