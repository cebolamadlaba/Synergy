import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricingInvestmentsComponent } from './pricing-investments.component';

describe('PricingInvestmentsComponent', () => {
  let component: PricingInvestmentsComponent;
  let fixture: ComponentFixture<PricingInvestmentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricingInvestmentsComponent ]
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
