import { TestBed, inject } from '@angular/core/testing';
import { AaManagementService } from './aa-management.service';
import { HttpModule } from '@angular/http';

describe('AaManagementService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [AaManagementService]
        });
    });

    it('should be created', inject([AaManagementService], (service: AaManagementService) => {
        expect(service).toBeTruthy();
    }));
});
