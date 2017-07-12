import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { DeclinedInboxComponent } from './declined-inbox.component';
import { InboxHeaderComponent } from "../inbox-header/inbox-header.component";
import { InboxSearchBarComponent } from "../inbox-search-bar/inbox-search-bar.component";
import { ConcessionsSummary } from "../models/concessions-summary";
import { InboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";

describe('DeclinedInboxComponent', () => {
  let component: DeclinedInboxComponent;
  let fixture: ComponentFixture<DeclinedInboxComponent>;

  beforeEach(async(() => {
      TestBed.configureTestingModule({
          imports: [HttpModule],
          declarations: [DeclinedInboxComponent, InboxHeaderComponent, InboxSearchBarComponent],
          providers: [InboxConcessionCountService]
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
