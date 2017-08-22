import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { PricingCashComponent } from './pricing-cash.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { CashConcessionService, MockCashConcessionService } from "../cash-concession/cash-concession.service";

describe('PricingCashComponent', () => {
    let component: PricingCashComponent;
    let fixture: ComponentFixture<PricingCashComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, FormsModule, HttpModule],
            declarations: [PricingCashComponent],
            providers: [{ provide: CashConcessionService, useClass: MockCashConcessionService }]
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
