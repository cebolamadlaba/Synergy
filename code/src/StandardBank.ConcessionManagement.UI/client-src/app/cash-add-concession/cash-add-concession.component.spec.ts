import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CashAddConcessionComponent } from './cash-add-concession.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { RiskGroupService, MockRiskGroupService } from "../services/risk-group.service";
import { PeriodService, MockPeriodService } from "../services/period.service";
import { PeriodTypeService, MockPeriodTypeService } from "../services/period-type.service";
import { ConditionTypeService, MockConditionTypeService } from "../services/condition-type.service";
import { ClientAccountService, MockClientAccountService } from "../services/client-account.service";
import { AccrualTypeService, MockAccrualTypeService } from "../services/accrual-type.service";
import { ChannelTypeService, MockChannelTypeService } from "../services/channel-type.service";

describe('CashAddConcessionComponent', () => {
    let component: CashAddConcessionComponent;
    let fixture: ComponentFixture<CashAddConcessionComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, ModalModule.forRoot(), RouterTestingModule, FormsModule, ReactiveFormsModule],
            declarations: [CashAddConcessionComponent],
            providers: [
                { provide: RiskGroupService, useClass: MockRiskGroupService },
                { provide: PeriodService, useClass: MockPeriodService },
                { provide: PeriodTypeService, useClass: MockPeriodTypeService },
                { provide: ConditionTypeService, useClass: MockConditionTypeService },
                { provide: ClientAccountService, useClass: MockClientAccountService },
                { provide: AccrualTypeService, useClass: MockAccrualTypeService },
                { provide: ChannelTypeService, useClass: MockChannelTypeService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(CashAddConcessionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
