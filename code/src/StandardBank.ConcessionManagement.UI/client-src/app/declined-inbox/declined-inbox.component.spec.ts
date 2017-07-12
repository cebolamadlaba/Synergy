import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeclinedInboxComponent } from './declined-inbox.component';

describe('DeclinedInboxComponent', () => {
  let component: DeclinedInboxComponent;
  let fixture: ComponentFixture<DeclinedInboxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeclinedInboxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeclinedInboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
