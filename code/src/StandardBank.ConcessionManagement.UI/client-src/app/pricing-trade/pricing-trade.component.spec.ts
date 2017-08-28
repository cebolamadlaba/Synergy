import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricingTradeComponent } from './pricing-trade.component';

describe('PricingTradeComponent', () => {
  let component: PricingTradeComponent;
  let fixture: ComponentFixture<PricingTradeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricingTradeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PricingTradeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
