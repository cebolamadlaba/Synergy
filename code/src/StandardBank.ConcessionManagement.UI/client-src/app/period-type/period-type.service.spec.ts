import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { PeriodTypeService } from './period-type.service';

describe('PeriodTypeService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [PeriodTypeService]
        });
    });

    it('should be created', inject([PeriodTypeService], (service: PeriodTypeService) => {
        expect(service).toBeTruthy();
    }));
});
