import { TestBed, inject } from '@angular/core/testing';
import { PcmManagementService } from './pcm-management.service';
import { HttpModule } from '@angular/http';

describe('PcmManagementService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [PcmManagementService]
        });
    });

    it('should be created', inject([PcmManagementService], (service: PcmManagementService) => {
        expect(service).toBeTruthy();
    }));
});
