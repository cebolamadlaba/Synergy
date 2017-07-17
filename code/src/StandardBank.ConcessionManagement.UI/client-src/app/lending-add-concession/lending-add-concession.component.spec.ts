import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LendingAddConcessionComponent } from './lending-add-concession.component';

describe('LendingAddConcessionComponent', () => {
  let component: LendingAddConcessionComponent;
  let fixture: ComponentFixture<LendingAddConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LendingAddConcessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LendingAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
