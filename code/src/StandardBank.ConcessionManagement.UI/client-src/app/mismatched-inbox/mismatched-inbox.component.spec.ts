﻿import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MismatchedInboxComponent } from './mismatched-inbox.component';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from "../inbox-header/inbox-header.component";
import { UserConcessionsService, MockUserConcessionsService } from "../user-concessions/user-concessions.service";
import { DataTablesModule } from 'angular-datatables';

describe('MismatchedInboxComponent', () => {
    let component: MismatchedInboxComponent;
    let fixture: ComponentFixture<MismatchedInboxComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, DataTablesModule],
            declarations: [MismatchedInboxComponent, InboxHeaderComponent],
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
