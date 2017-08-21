import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PricingCashComponent } from './pricing-cash.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';

describe('PricingCashComponent', () => {
    let component: PricingCashComponent;
    let fixture: ComponentFixture<PricingCashComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, FormsModule],
            declarations: [PricingCashComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PricingCashComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
