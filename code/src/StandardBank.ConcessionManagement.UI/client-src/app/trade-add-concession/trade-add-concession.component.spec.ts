import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TradeAddConcessionComponent } from './trade-add-concession.component';

describe('TradeAddConcessionComponent', () => {
  let component: TradeAddConcessionComponent;
  let fixture: ComponentFixture<TradeAddConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TradeAddConcessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TradeAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
