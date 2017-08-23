import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ConditionTypeService } from './condition-type.service';

describe('ConditionTypeService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [ConditionTypeService]
        });
    });

    it('should be created', inject([ConditionTypeService], (service: ConditionTypeService) => {
        expect(service).toBeTruthy();
    }));
});
