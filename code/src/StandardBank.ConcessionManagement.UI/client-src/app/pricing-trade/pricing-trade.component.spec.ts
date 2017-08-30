import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PricingTradeComponent } from './pricing-trade.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('PricingTradeComponent', () => {
  let component: PricingTradeComponent;
  let fixture: ComponentFixture<PricingTradeComponent>;

  beforeEach(async(() => {
      TestBed.configureTestingModule({
          imports: [RouterTestingModule],
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
