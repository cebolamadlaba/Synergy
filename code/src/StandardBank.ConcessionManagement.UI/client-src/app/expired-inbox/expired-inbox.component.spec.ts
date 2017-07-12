import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpiredInboxComponent } from './expired-inbox.component';

describe('ExpiredInboxComponent', () => {
  let component: ExpiredInboxComponent;
  let fixture: ComponentFixture<ExpiredInboxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExpiredInboxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpiredInboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
