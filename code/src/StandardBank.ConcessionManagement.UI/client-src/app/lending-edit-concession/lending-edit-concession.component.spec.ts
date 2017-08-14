import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { LendingEditConcessionComponent } from './lending-edit-concession.component';
import { RouterTestingModule } from '@angular/router/testing';
import { RiskGroupService, MockRiskGroupService } from "../risk-group/risk-group.service";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ReviewFeeTypeService, MockReviewFeeTypeService } from "../review-fee-type/review-fee-type.service";
import { ProductTypeService, MockProductTypeService } from "../product-type/product-type.service";
import { PeriodService, MockPeriodService } from "../period/period.service";
import { PeriodTypeService, MockPeriodTypeService } from "../period-type/period-type.service";
import { ConditionTypeService, MockConditionTypeService } from "../condition-type/condition-type.service";
import { ClientAccountService, MockClientAccountService } from "../client-account/client-account.service";
import { LendingService, MockLendingService } from "../lending/lending.service";
import { LendingUpdateService, MockLendingUpdateService } from "../lending-update/lending-update.service";

describe('LendingEditConcessionComponent', () => {
    let component: LendingEditConcessionComponent;
    let fixture: ComponentFixture<LendingEditConcessionComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, ModalModule.forRoot(), RouterTestingModule, FormsModule, ReactiveFormsModule],
            declarations: [LendingEditConcessionComponent],
            providers: [
                { provide: RiskGroupService, useClass: MockRiskGroupService },
                { provide: ReviewFeeTypeService, useClass: MockReviewFeeTypeService },
                { provide: ProductTypeService, useClass: MockProductTypeService },
                { provide: PeriodService, useClass: MockPeriodService },
                { provide: PeriodTypeService, useClass: MockPeriodTypeService },
                { provide: ConditionTypeService, useClass: MockConditionTypeService },
                { provide: ClientAccountService, useClass: MockClientAccountService },
                { provide: LendingUpdateService, useClass: MockLendingUpdateService},
                { provide: LendingService, useClass: MockLendingService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LendingEditConcessionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', function (done) {
        expect(component).toBeTruthy();
        done();
    });
});
