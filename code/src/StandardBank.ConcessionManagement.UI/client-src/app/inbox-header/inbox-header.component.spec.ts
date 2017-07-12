import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from './inbox-header.component';
import { MockBackend } from '@angular/http/testing';
import { Observable } from "rxjs";
import { ConcessionsSummary } from "../models/concessions-summary";
import { Injectable } from "@angular/core";
import { InboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";

describe('InboxHeaderComponent', () => {
    let component: InboxHeaderComponent;
    let fixture: ComponentFixture<InboxHeaderComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            declarations: [InboxHeaderComponent],
            providers: [{ provide: InboxConcessionCountService, useClass: MockInboxConcessionCountService}]
        })
            .compileComponents();

        console.log("Fuck javascript async");
    }));

    beforeEach(() => {
        fixture = TestBed.overrideComponent(InboxHeaderComponent, {
            set: {
                providers: [
                    { provide: InboxConcessionCountService, useClass: MockInboxConcessionCountService },
                ]
            }
        }).createComponent(InboxHeaderComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});

@Injectable()
export class MockInboxConcessionCountService {

    constructor() {
        console.log("Fuck javascript constructor");
    }

    model: ConcessionsSummary = new ConcessionsSummary();

    getData(): Observable<ConcessionsSummary> {
        console.log("Fuck javascript getData");
        this.model.pendingConcessions = 1;
        return Observable.of(this.model);
    }
}
