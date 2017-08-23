import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { PeriodService } from './period.service';

describe('PeriodService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [PeriodService]
        });
    });

    it('should be created', inject([PeriodService], (service: PeriodService) => {
        expect(service).toBeTruthy();
    }));
});
