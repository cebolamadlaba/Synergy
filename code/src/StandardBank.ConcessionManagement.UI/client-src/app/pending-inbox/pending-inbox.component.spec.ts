import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingInboxComponent } from './pending-inbox.component';

describe('PendingInboxComponent', () => {
  let component: PendingInboxComponent;
  let fixture: ComponentFixture<PendingInboxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PendingInboxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PendingInboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
