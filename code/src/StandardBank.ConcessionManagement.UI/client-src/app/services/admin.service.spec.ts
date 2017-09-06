import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { AdminService } from './admin.service';

describe('AdminService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [AdminService]
        });
    });

    it('should be created', inject([AdminService], (service: AdminService) => {
        expect(service).toBeTruthy();
    }));
});
