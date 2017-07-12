import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InboxHeaderComponent } from './inbox-header.component';

describe('InboxHeaderComponent', () => {
  let component: InboxHeaderComponent;
  let fixture: ComponentFixture<InboxHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InboxHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InboxHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
