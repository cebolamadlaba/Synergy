import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ConcessionConditionsService } from './concession-conditions.service';

describe('ConcessionConditionsService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [ConcessionConditionsService]
        });
    });

    it('should be created', inject([ConcessionConditionsService], (service: ConcessionConditionsService) => {
        expect(service).toBeTruthy();
    }));
});
