import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CashViewConcessionComponent } from './cash-view-concession.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { LookupDataService, MockLookupDataService } from "../services/lookup-data.service";
import { CashConcessionService, MockCashConcessionService } from "../services/cash-concession.service";
import { UserConcessionsService, MockUserConcessionsService } from "../services/user-concessions.service";

describe('CashViewConcessionComponent', () => {
    let component: CashViewConcessionComponent;
    let fixture: ComponentFixture<CashViewConcessionComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, ModalModule.forRoot(), RouterTestingModule, FormsModule, ReactiveFormsModule],
            declarations: [CashViewConcessionComponent],
            providers: [
                { provide: LookupDataService, useClass: MockLookupDataService },
                { provide: CashConcessionService, useClass: MockCashConcessionService },
                { provide: UserConcessionsService, useClass: MockUserConcessionsService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(CashViewConcessionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
