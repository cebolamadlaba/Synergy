import { TestBed, inject } from '@angular/core/testing';
import { BusinessCentreService } from './business-centre.service';
import { HttpModule } from '@angular/http';

describe('BusinessCentreService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [BusinessCentreService]
        });
    });

    it('should be created', inject([BusinessCentreService], (service: BusinessCentreService) => {
        expect(service).toBeTruthy();
    }));
});
