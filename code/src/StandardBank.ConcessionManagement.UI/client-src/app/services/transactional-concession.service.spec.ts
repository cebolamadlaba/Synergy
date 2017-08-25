import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { TransactionalConcessionService } from './transactional-concession.service';

describe('TransactionalConcessionService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [TransactionalConcessionService]
        });
    });

    it('should be created', inject([TransactionalConcessionService], (service: TransactionalConcessionService) => {
        expect(service).toBeTruthy();
    }));
});
