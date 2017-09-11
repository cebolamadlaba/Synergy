import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { LendingService } from './lending.service';

describe('LendingService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [LendingService]
        });
    });

    it('should be created', inject([LendingService], (service: LendingService) => {
        expect(service).toBeTruthy();
    }));
});
