import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricingResultsComponent } from './pricing-results.component';

describe('PricingResultsComponent', () => {
  let component: PricingResultsComponent;
  let fixture: ComponentFixture<PricingResultsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricingResultsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PricingResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
