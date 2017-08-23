import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { LendingViewService } from './lending-view.service';

describe('LendingViewService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [LendingViewService]
        });
    });

    it('should be created', inject([LendingViewService], (service: LendingViewService) => {
        expect(service).toBeTruthy();
    }));
});
