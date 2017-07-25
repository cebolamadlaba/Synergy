import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { RiskGroupLegalEntitiesService } from './risk-group-legal-entities.service';

describe('RiskGroupLegalEntitiesService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [RiskGroupLegalEntitiesService]
        });
    });

    it('should be created', inject([RiskGroupLegalEntitiesService], (service: RiskGroupLegalEntitiesService) => {
        expect(service).toBeTruthy();
    }));
});
