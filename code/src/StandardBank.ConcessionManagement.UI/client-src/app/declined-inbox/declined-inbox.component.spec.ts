import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DeclinedInboxComponent } from './declined-inbox.component';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from "../inbox-header/inbox-header.component";
import { InboxSearchBarComponent } from "../inbox-search-bar/inbox-search-bar.component";
import { InboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";
import { MockInboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";

describe('DeclinedInboxComponent', () => {
  let component: DeclinedInboxComponent;
  let fixture: ComponentFixture<DeclinedInboxComponent>;

  beforeEach(async(() => {
      TestBed.configureTestingModule({
          imports: [HttpModule],
          declarations: [DeclinedInboxComponent, InboxHeaderComponent, InboxSearchBarComponent],
          providers: [{ provide: InboxConcessionCountService, useClass: MockInboxConcessionCountService }]
      }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.overrideComponent(InboxHeaderComponent, {
      set: {
        providers: [
          { provide: InboxConcessionCountService, useClass: MockInboxConcessionCountService },
        ]
      }
    }).createComponent(DeclinedInboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
