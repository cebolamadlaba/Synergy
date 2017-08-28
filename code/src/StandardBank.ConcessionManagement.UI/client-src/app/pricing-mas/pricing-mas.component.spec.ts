import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricingMasComponent } from './pricing-mas.component';

describe('PricingMasComponent', () => {
  let component: PricingMasComponent;
  let fixture: ComponentFixture<PricingMasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricingMasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PricingMasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
