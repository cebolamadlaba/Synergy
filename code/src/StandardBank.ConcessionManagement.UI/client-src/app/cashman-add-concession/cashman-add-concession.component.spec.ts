import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CashmanAddConcessionComponent } from './cashman-add-concession.component';

describe('CashmanAddConcessionComponent', () => {
  let component: CashmanAddConcessionComponent;
  let fixture: ComponentFixture<CashmanAddConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CashmanAddConcessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CashmanAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
