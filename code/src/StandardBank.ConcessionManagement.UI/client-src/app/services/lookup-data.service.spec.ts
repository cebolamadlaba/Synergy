import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { LookupDataService } from './lookup-data.service';

describe('LookupDataService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [LookupDataService]
        });
    });

    it('should be created', inject([LookupDataService], (service: LookupDataService) => {
        expect(service).toBeTruthy();
    }));
});
