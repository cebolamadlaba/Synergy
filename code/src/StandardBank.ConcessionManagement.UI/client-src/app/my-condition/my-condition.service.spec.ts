import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { MyConditionService } from './my-condition.service';

describe('MyConditionService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [MyConditionService]
        });
    });

    it('should be created', inject([MyConditionService], (service: MyConditionService) => {
        expect(service).toBeTruthy();
    }));
});
