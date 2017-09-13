import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TransactionalViewConcessionComponent } from './transactional-view-concession.component';
import { HttpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { LookupDataService, MockLookupDataService } from "../services/lookup-data.service";
import { TransactionalConcessionService, MockTransactionalConcessionService } from "../services/transactional-concession.service";
import { UserConcessionsService, MockUserConcessionsService } from "../services/user-concessions.service";

describe('TransactionalViewConcessionComponent', () => {
    let component: TransactionalViewConcessionComponent;
    let fixture: ComponentFixture<TransactionalViewConcessionComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, ModalModule.forRoot(), RouterTestingModule, FormsModule, ReactiveFormsModule],
            declarations: [TransactionalViewConcessionComponent],
            providers: [
                { provide: LookupDataService, useClass: MockLookupDataService },
                { provide: TransactionalConcessionService, useClass: MockTransactionalConcessionService },
                { provide: UserConcessionsService, useClass: MockUserConcessionsService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TransactionalViewConcessionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
