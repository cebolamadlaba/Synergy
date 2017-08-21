import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DeclinedInboxComponent } from './declined-inbox.component';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from "../inbox-header/inbox-header.component";
import { UserConcessionsService, MockUserConcessionsService } from "../user-concessions/user-concessions.service";
import { DataTablesModule } from 'angular-datatables';
import { RouterTestingModule } from '@angular/router/testing';

describe('DeclinedInboxComponent', () => {
    let component: DeclinedInboxComponent;
    let fixture: ComponentFixture<DeclinedInboxComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, DataTablesModule, RouterTestingModule],
            declarations: [DeclinedInboxComponent, InboxHeaderComponent],
            providers: [{ provide: UserConcessionsService, useClass: MockUserConcessionsService }]
        }).compileComponents();
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
