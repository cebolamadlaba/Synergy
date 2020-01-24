import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConcessionConditionModalComponent } from './concession-condition-modal.component';

describe('ConcessionConditionModalComponent', () => {
  let component: ConcessionConditionModalComponent;
  let fixture: ComponentFixture<ConcessionConditionModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConcessionConditionModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConcessionConditionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
