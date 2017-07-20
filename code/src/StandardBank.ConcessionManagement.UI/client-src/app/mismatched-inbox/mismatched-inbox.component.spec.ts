import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MismatchedInboxComponent } from './mismatched-inbox.component';

describe('MismatchedInboxComponent', () => {
  let component: MismatchedInboxComponent;
  let fixture: ComponentFixture<MismatchedInboxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MismatchedInboxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MismatchedInboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
