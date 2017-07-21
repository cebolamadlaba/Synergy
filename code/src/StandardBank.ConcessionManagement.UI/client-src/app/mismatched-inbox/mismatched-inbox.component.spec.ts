import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MismatchedInboxComponent } from './mismatched-inbox.component';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from "../inbox-header/inbox-header.component";
import { InboxSearchBarComponent } from "../inbox-search-bar/inbox-search-bar.component";
import { UserConcessionsService, MockUserConcessionsService } from "../user-concessions/user-concessions.service";

describe('MismatchedInboxComponent', () => {
    let component: MismatchedInboxComponent;
    let fixture: ComponentFixture<MismatchedInboxComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            declarations: [MismatchedInboxComponent, InboxHeaderComponent, InboxSearchBarComponent],
            providers: [{ provide: UserConcessionsService, useClass: MockUserConcessionsService }]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(MismatchedInboxComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
