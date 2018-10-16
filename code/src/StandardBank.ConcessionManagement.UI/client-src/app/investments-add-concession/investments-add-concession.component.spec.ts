import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InvestmentAddConcessionComponent } from './investments-add-concession.component';

describe('InvestmentAddConcessionComponent', () => {
  let component: InvestmentAddConcessionComponent;
  let fixture: ComponentFixture<InvestmentAddConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InvestmentAddConcessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InvestmentAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
