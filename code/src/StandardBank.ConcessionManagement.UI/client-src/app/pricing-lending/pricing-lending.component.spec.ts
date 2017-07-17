import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricingLendingComponent } from './pricing-lending.component';

describe('PricingLendingComponent', () => {
  let component: PricingLendingComponent;
  let fixture: ComponentFixture<PricingLendingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricingLendingComponent ]
    })
    .compileComponents();
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
