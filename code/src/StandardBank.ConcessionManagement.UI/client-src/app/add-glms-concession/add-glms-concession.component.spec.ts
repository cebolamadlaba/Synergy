import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddGlmsConcessionComponent } from './add-glms-concession.component';

describe('AddGlmsConcessionComponent', () => {
  let component: AddGlmsConcessionComponent;
  let fixture: ComponentFixture<AddGlmsConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddGlmsConcessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddGlmsConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
