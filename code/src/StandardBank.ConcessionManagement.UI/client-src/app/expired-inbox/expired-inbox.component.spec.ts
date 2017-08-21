import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ExpiredInboxComponent } from './expired-inbox.component';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from "../inbox-header/inbox-header.component";
import { UserConcessionsService, MockUserConcessionsService } from "../user-concessions/user-concessions.service";
import { DataTablesModule } from 'angular-datatables';
import { RouterTestingModule } from '@angular/router/testing';

describe('ExpiredInboxComponent', () => {
    let component: ExpiredInboxComponent;
    let fixture: ComponentFixture<ExpiredInboxComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, DataTablesModule, RouterTestingModule],
            declarations: [ExpiredInboxComponent, InboxHeaderComponent],
            providers: [{ provide: UserConcessionsService, useClass: MockUserConcessionsService }]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExpiredInboxComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
