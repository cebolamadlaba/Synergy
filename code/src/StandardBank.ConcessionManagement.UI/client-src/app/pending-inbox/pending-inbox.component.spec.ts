import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PendingInboxComponent } from './pending-inbox.component';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from "../inbox-header/inbox-header.component";
import { UserConcessionsService, MockUserConcessionsService } from "../user-concessions/user-concessions.service";
import { DataTablesModule } from 'angular-datatables';
import { RouterTestingModule } from '@angular/router/testing';

describe('PendingInboxComponent', () => {
    let component: PendingInboxComponent;
    let fixture: ComponentFixture<PendingInboxComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, DataTablesModule, RouterTestingModule],
            declarations: [PendingInboxComponent, InboxHeaderComponent],
            providers: [{ provide: UserConcessionsService, useClass: MockUserConcessionsService }]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PendingInboxComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
