import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PricingBolComponent } from './pricing-bol.component';
import { HttpModule } from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { BaseConcessionFilterPipe } from "../filters/base-concession-filter.pipe";

describe('PricingBolComponent', () => {
    let component: PricingBolComponent;
    let fixture: ComponentFixture<PricingBolComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, FormsModule, HttpModule],
            declarations: [PricingBolComponent, BaseConcessionFilterPipe]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PricingBolComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
