import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { AccrualTypeService } from './accrual-type.service';

describe('AccrualTypeService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [AccrualTypeService]
        });
    });

    it('should be created', inject([AccrualTypeService], (service: AccrualTypeService) => {
        expect(service).toBeTruthy();
    }));
});
