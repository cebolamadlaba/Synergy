import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { UserConcessionsService } from './user-concessions.service';

describe('UserConcessionsService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [UserConcessionsService]
        });
    });

    it('should be created', inject([UserConcessionsService], (service: UserConcessionsService) => {
        expect(service).toBeTruthy();
    }));
});
