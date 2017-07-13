import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from './inbox-header.component';
import { InboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";
import { MockInboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";

describe('InboxHeaderComponent', () => {
  let component: InboxHeaderComponent;
  let fixture: ComponentFixture<InboxHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpModule],
      declarations: [InboxHeaderComponent],
      providers: [{ provide: InboxConcessionCountService, useClass: MockInboxConcessionCountService }]
    }).compileComponents();
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

