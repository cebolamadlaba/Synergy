import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DueExpiryInboxComponent } from './due-expiry-inbox.component';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from "../inbox-header/inbox-header.component";
import { UserConcessionsService, MockUserConcessionsService } from "../user-concessions/user-concessions.service";
import { DataTablesModule } from 'angular-datatables';

describe('DueExpiryInboxComponent', () => {
    let component: DueExpiryInboxComponent;
    let fixture: ComponentFixture<DueExpiryInboxComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, DataTablesModule],
            declarations: [DueExpiryInboxComponent, InboxHeaderComponent],
            providers: [{ provide: UserConcessionsService, useClass: MockUserConcessionsService }]
        }).compileComponents();
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
