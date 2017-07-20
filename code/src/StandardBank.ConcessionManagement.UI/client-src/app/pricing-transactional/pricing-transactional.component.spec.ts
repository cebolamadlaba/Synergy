import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricingTransactionalComponent } from './pricing-transactional.component';

describe('PricingTransactionalComponent', () => {
  let component: PricingTransactionalComponent;
  let fixture: ComponentFixture<PricingTransactionalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricingTransactionalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PricingTransactionalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});