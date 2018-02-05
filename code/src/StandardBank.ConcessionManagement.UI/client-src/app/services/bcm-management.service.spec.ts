import { TestBed, inject } from '@angular/core/testing';
import { BcmManagementService } from './bcm-management.service';
import { HttpModule } from '@angular/http';

describe('BcmManagementService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [BcmManagementService]
        });
    });

    it('should be created', inject([BcmManagementService], (service: BcmManagementService) => {
        expect(service).toBeTruthy();
    }));
});
