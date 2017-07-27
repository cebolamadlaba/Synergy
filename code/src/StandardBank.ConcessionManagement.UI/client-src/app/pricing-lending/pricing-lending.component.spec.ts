﻿import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PricingLendingComponent } from './pricing-lending.component';
import { RouterTestingModule } from '@angular/router/testing';
import { LendingViewService, MockLendingViewService } from "../lending-view/lending-view.service";

describe('PricingLendingComponent', () => {
    let component: PricingLendingComponent;
    let fixture: ComponentFixture<PricingLendingComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule],
            declarations: [PricingLendingComponent],
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
