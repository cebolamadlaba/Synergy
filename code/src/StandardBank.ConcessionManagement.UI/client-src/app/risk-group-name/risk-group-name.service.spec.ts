import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { RiskGroupNameService } from './risk-group-name.service';

describe('RiskGroupNameService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [RiskGroupNameService]
        });
    });

    it('should be created', inject([RiskGroupNameService], (service: RiskGroupNameService) => {
        expect(service).toBeTruthy();
    }));
});
