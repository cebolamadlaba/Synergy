import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { PricingLendingComponent } from './pricing-lending.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { LendingService, MockLendingService } from "../services/lending.service";
import { BaseConcessionFilterPipe } from "../filters/base-concession-filter.pipe";

describe('PricingLendingComponent', () => {
    let component: PricingLendingComponent;
    let fixture: ComponentFixture<PricingLendingComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, FormsModule, HttpModule],
            declarations: [PricingLendingComponent, BaseConcessionFilterPipe],
            providers: [{ provide: LendingService, useClass: MockLendingService }]
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
