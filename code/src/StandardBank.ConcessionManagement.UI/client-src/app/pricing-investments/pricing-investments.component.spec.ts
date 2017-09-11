import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PricingInvestmentsComponent } from './pricing-investments.component';
import { HttpModule } from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { BaseConcessionFilterPipe } from "../filters/base-concession-filter.pipe";

describe('PricingInvestmentsComponent', () => {
    let component: PricingInvestmentsComponent;
    let fixture: ComponentFixture<PricingInvestmentsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, FormsModule, HttpModule],
            declarations: [PricingInvestmentsComponent, BaseConcessionFilterPipe]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PricingInvestmentsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
