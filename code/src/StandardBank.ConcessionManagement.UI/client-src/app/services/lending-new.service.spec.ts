import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { LendingNewService } from './lending-new.service';

describe('LendingNewService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [LendingNewService]
        });
    });

    it('should be created', inject([LendingNewService], (service: LendingNewService) => {
        expect(service).toBeTruthy();
    }));
});
