import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ReviewFeeTypeService } from './review-fee-type.service';

describe('ReviewFeeTypeService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [ReviewFeeTypeService]
        });
    });

    it('should be created', inject([ReviewFeeTypeService], (service: ReviewFeeTypeService) => {
        expect(service).toBeTruthy();
    }));
});
