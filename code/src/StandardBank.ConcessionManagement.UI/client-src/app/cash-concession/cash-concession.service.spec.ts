import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { CashConcessionService } from './cash-concession.service';

describe('CashConcessionService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [CashConcessionService]
        });
    });

    it('should be created', inject([CashConcessionService], (service: CashConcessionService) => {
        expect(service).toBeTruthy();
    }));
});
