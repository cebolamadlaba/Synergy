import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ChannelTypeService } from './channel-type.service';

describe('ChannelTypeService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [ChannelTypeService]
        });
    });

    it('should be created', inject([ChannelTypeService], (service: ChannelTypeService) => {
        expect(service).toBeTruthy();
    }));
});
