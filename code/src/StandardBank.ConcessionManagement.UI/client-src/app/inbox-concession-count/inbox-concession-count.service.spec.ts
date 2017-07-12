import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { InboxConcessionCountService } from './inbox-concession-count.service';

describe('InboxConcessionCountService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [InboxConcessionCountService]
        });
    });

    it('should be created', inject([InboxConcessionCountService], (service: InboxConcessionCountService) => {
        expect(service).toBeTruthy();
    }));
});
