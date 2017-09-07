import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ActionedInboxComponent } from './actioned-inbox.component';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from "../inbox-header/inbox-header.component";
import { UserConcessionsService, MockUserConcessionsService } from "../services/user-concessions.service";
import { DataTablesModule } from 'angular-datatables';
import { RouterTestingModule } from '@angular/router/testing';

describe('ActionedInboxComponent', () => {
    let component: ActionedInboxComponent;
    let fixture: ComponentFixture<ActionedInboxComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, DataTablesModule, RouterTestingModule],
            declarations: [ActionedInboxComponent, InboxHeaderComponent],
            providers: [{ provide: UserConcessionsService, useClass: MockUserConcessionsService }]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ActionedInboxComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
