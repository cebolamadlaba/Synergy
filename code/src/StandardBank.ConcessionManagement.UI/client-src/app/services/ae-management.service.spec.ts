import { TestBed, inject } from '@angular/core/testing';
import { AeManagementService } from './ae-management.service';
import { HttpModule } from '@angular/http';

describe('AeManagementService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [AeManagementService]
        });
    });

    it('should be created', inject([AeManagementService], (service: AeManagementService) => {
        expect(service).toBeTruthy();
    }));
});
