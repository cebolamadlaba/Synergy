import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PricingMasComponent } from './pricing-mas.component';
import { HttpModule } from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { BaseConcessionFilterPipe } from "../filters/base-concession-filter.pipe";

describe('PricingMasComponent', () => {
    let component: PricingMasComponent;
    let fixture: ComponentFixture<PricingMasComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, FormsModule, HttpModule],
            declarations: [PricingMasComponent, BaseConcessionFilterPipe]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PricingMasComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
