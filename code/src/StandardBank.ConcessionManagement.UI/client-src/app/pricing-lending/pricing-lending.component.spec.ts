import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PricingLendingComponent } from './pricing-lending.component';
import { RouterTestingModule } from '@angular/router/testing';
import { LendingViewService, MockLendingViewService } from "../services/lending-view.service";
import { FormsModule } from '@angular/forms';
import { LendingConcessionFilterPipe } from "../filters/lending-concession-filter.pipe";

describe('PricingLendingComponent', () => {
    let component: PricingLendingComponent;
    let fixture: ComponentFixture<PricingLendingComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, FormsModule],
            declarations: [PricingLendingComponent, LendingConcessionFilterPipe],
            providers: [{ provide: LendingViewService, useClass: MockLendingViewService }]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PricingLendingComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
