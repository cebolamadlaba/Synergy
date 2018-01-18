import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RegionComponent } from './region.component';
import { HttpModule } from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { RegionService, MockRegionService } from '../../services/region.service';

describe('RegionComponent', () => {
    let component: RegionComponent;
    let fixture: ComponentFixture<RegionComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, RouterTestingModule],
            declarations: [RegionComponent],
            providers: [{ provide: RegionService, useClass: MockRegionService }]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(RegionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
