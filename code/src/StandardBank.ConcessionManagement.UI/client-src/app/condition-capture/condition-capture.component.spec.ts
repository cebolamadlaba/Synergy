import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConditionCaptureComponent } from './condition-capture.component';

describe('ConditionCaptureComponent', () => {
  let component: ConditionCaptureComponent;
  let fixture: ComponentFixture<ConditionCaptureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConditionCaptureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConditionCaptureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
