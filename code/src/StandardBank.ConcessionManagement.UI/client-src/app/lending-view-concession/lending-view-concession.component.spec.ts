import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { LendingViewConcessionComponent } from './lending-view-concession.component';
import { RouterTestingModule } from '@angular/router/testing';
import { RiskGroupService, MockRiskGroupService } from "../services/risk-group.service";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ReviewFeeTypeService, MockReviewFeeTypeService } from "../services/review-fee-type.service";
import { ProductTypeService, MockProductTypeService } from "../services/product-type.service";
import { PeriodService, MockPeriodService } from "../services/period.service";
import { PeriodTypeService, MockPeriodTypeService } from "../services/period-type.service";
import { ConditionTypeService, MockConditionTypeService } from "../services/condition-type.service";
import { ClientAccountService, MockClientAccountService } from "../services/client-account.service";
import { LendingService, MockLendingService } from "../services/lending.service";
import { LendingUpdateService, MockLendingUpdateService } from "../services/lending-update.service";

describe('LendingViewConcessionComponent', () => {
    let component: LendingViewConcessionComponent;
    let fixture: ComponentFixture<LendingViewConcessionComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, ModalModule.forRoot(), RouterTestingModule, FormsModule, ReactiveFormsModule],
            declarations: [LendingViewConcessionComponent],
            providers: [
                { provide: RiskGroupService, useClass: MockRiskGroupService },
                { provide: ReviewFeeTypeService, useClass: MockReviewFeeTypeService },
                { provide: ProductTypeService, useClass: MockProductTypeService },
                { provide: PeriodService, useClass: MockPeriodService },
                { provide: PeriodTypeService, useClass: MockPeriodTypeService },
                { provide: ConditionTypeService, useClass: MockConditionTypeService },
                { provide: ClientAccountService, useClass: MockClientAccountService },
                { provide: LendingUpdateService, useClass: MockLendingUpdateService },
                { provide: LendingService, useClass: MockLendingService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LendingViewConcessionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', function (done) {
        expect(component).toBeTruthy();
        done();
    });
});
