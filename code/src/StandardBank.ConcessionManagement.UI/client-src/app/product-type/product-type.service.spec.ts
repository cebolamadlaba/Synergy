import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ProductTypeService } from './product-type.service';

describe('ProductTypeService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [ProductTypeService]
        });
    });

    it('should be created', inject([ProductTypeService], (service: ProductTypeService) => {
        expect(service).toBeTruthy();
    }));
});
