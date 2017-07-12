import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InboxSearchBarComponent } from './inbox-search-bar.component';

describe('InboxSearchBarComponent', () => {
  let component: InboxSearchBarComponent;
  let fixture: ComponentFixture<InboxSearchBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InboxSearchBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InboxSearchBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
