import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DueExpiryInboxComponent } from './due-expiry-inbox.component';

describe('DueExpiryInboxComponent', () => {
  let component: DueExpiryInboxComponent;
  let fixture: ComponentFixture<DueExpiryInboxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DueExpiryInboxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DueExpiryInboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
