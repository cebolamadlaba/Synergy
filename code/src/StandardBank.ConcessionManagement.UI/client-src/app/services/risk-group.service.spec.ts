import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { RiskGroupService } from "./risk-group.service";

describe('RiskGroupService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [RiskGroupService]
        });
    });

    it('should be created', inject([RiskGroupService], (service: RiskGroupService) => {
        expect(service).toBeTruthy();
    }));
});
