import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ClientAccountService } from './client-account.service';

describe('ClientAccountService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [ClientAccountService]
        });
    });

    it('should be created', inject([ClientAccountService], (service: ClientAccountService) => {
        expect(service).toBeTruthy();
    }));
});
