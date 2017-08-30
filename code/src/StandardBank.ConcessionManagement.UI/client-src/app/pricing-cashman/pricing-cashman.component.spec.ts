import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PricingCashmanComponent } from './pricing-cashman.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('PricingCashmanComponent', () => {
    let component: PricingCashmanComponent;
    let fixture: ComponentFixture<PricingCashmanComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule],
            declarations: [PricingCashmanComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PricingCashmanComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
