import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { LendingAddConcessionComponent } from './lending-add-concession.component';
import { RouterTestingModule } from '@angular/router/testing';
import { RiskGroupService, MockRiskGroupService } from "../risk-group/risk-group.service";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ReviewFeeTypeService, MockReviewFeeTypeService } from "../review-fee-type/review-fee-type.service";
import { ProductTypeService, MockProductTypeService } from "../product-type/product-type.service";

describe('LendingAddConcessionComponent', () => {
    let component: LendingAddConcessionComponent;
    let fixture: ComponentFixture<LendingAddConcessionComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, ModalModule.forRoot(), RouterTestingModule, FormsModule, ReactiveFormsModule],
            declarations: [LendingAddConcessionComponent],
            providers: [
                { provide: RiskGroupService, useClass: MockRiskGroupService },
                { provide: ReviewFeeTypeService, useClass: MockReviewFeeTypeService },
                { provide: ProductTypeService, useClass: MockProductTypeService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LendingAddConcessionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
