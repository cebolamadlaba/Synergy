import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { LendingUpdateService } from './lending-update.service';

describe('LendingUpdateService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [LendingUpdateService]
        });
    });

    it('should be created', inject([LendingUpdateService], (service: LendingUpdateService) => {
        expect(service).toBeTruthy();
    }));
});
