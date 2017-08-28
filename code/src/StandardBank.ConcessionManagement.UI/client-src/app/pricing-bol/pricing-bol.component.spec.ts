import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricingBolComponent } from './pricing-bol.component';

describe('PricingBolComponent', () => {
  let component: PricingBolComponent;
  let fixture: ComponentFixture<PricingBolComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricingBolComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PricingBolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
