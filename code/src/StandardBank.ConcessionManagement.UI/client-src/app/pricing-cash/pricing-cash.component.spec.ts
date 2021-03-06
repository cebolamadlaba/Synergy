import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PricingCashComponent } from './pricing-cash.component';
import { CashConcessionService, MockCashConcessionService } from "../services/cash-concession.service";
import { HttpModule } from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { BaseConcessionFilterPipe } from "../filters/base-concession-filter.pipe";

describe('PricingCashComponent', () => {
    let component: PricingCashComponent;
    let fixture: ComponentFixture<PricingCashComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, FormsModule, HttpModule],
            declarations: [PricingCashComponent, BaseConcessionFilterPipe],
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
