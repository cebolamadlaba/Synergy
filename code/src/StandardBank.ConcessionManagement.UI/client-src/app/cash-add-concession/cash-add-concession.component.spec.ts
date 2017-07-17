import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CashAddConcessionComponent } from './cash-add-concession.component';

describe('CashAddConcessionComponent', () => {
  let component: CashAddConcessionComponent;
  let fixture: ComponentFixture<CashAddConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CashAddConcessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CashAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
