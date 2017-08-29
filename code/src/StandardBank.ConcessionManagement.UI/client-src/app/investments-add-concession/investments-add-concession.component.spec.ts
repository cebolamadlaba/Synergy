import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InvestmentsAddConcessionComponent } from './investments-add-concession.component';

describe('InvestmentsAddConcessionComponent', () => {
  let component: InvestmentsAddConcessionComponent;
  let fixture: ComponentFixture<InvestmentsAddConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InvestmentsAddConcessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InvestmentsAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
