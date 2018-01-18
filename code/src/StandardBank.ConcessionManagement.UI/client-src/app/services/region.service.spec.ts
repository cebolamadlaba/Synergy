import { TestBed, inject } from '@angular/core/testing';
import { RegionService } from './region.service';
import { HttpModule } from '@angular/http';

describe('RegionService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [RegionService]
        });
    });

    it('should be created', inject([RegionService], (service: RegionService) => {
        expect(service).toBeTruthy();
    }));
});
