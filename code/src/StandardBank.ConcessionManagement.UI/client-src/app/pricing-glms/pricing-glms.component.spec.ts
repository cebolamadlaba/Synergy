import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricingGlmsComponent } from './pricing-glms.component';

describe('PricingGlmsComponent', () => {
  let component: PricingGlmsComponent;
  let fixture: ComponentFixture<PricingGlmsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricingGlmsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PricingGlmsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
