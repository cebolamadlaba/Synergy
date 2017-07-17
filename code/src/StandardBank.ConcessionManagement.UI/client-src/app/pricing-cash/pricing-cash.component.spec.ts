import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricingCashComponent } from './pricing-cash.component';

describe('PricingCashComponent', () => {
  let component: PricingCashComponent;
  let fixture: ComponentFixture<PricingCashComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricingCashComponent ]
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
