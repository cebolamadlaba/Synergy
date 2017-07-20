import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MismatchedInboxComponent } from './mismatched-inbox.component';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from "../inbox-header/inbox-header.component";
import { InboxSearchBarComponent } from "../inbox-search-bar/inbox-search-bar.component";
import { InboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";
import { MockInboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";

describe('MismatchedInboxComponent', () => {
    let component: MismatchedInboxComponent;
    let fixture: ComponentFixture<MismatchedInboxComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            declarations: [MismatchedInboxComponent, InboxHeaderComponent, InboxSearchBarComponent],
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
        }).createComponent(MismatchedInboxComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
