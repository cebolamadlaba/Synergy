import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DueExpiryInboxComponent } from './due-expiry-inbox.component';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from "../inbox-header/inbox-header.component";
import { InboxSearchBarComponent } from "../inbox-search-bar/inbox-search-bar.component";
import { InboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";
import { MockInboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";

describe('DueExpiryInboxComponent', () => {
  let component: DueExpiryInboxComponent;
  let fixture: ComponentFixture<DueExpiryInboxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpModule],
      declarations: [DueExpiryInboxComponent, InboxHeaderComponent, InboxSearchBarComponent],
      providers: [{ provide: InboxConcessionCountService, useClass: MockInboxConcessionCountService }]
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
