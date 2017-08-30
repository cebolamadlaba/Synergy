import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ProvinceService } from './province.service';

describe('ProvinceService', () => {
  beforeEach(() => {
      TestBed.configureTestingModule({
          imports: [HttpModule],
      providers: [ProvinceService]
    });
  });

  it('should be created', inject([ProvinceService], (service: ProvinceService) => {
    expect(service).toBeTruthy();
  }));
});
