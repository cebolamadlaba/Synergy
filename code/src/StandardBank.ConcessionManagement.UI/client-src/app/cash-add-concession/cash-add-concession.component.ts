import { DatePipe, Location } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from "rxjs";
import { ConcessionTypes } from '../constants/concession-types';
import { AccrualType } from "../models/accrual-type";
import { CashConcession } from "../models/cash-concession";
import { CashConcessionDetail } from "../models/cash-concession-detail";
import { ChannelType } from "../models/channel-type";
import { ClientAccount } from "../models/client-account";
import { Concession } from "../models/concession";
import { ConditionType } from "../models/condition-type";
import { LegalEntity } from "../models/legal-entity";
import { Period } from "../models/period";
import { PeriodType } from "../models/period-type";
import { RiskGroup } from "../models/risk-group";
import { TableNumber } from "../models/table-number";
import { BaseComponentService } from '../services/base-component.service';
import { CashBaseService } from '../services/cash-base.service';
import { CashConcessionService } from "../services/cash-concession.service";
import { FileService } from '../services/file.service';
import { LookupDataService } from "../services/lookup-data.service";


@Component({
    selector: 'app-cash-add-concession',
    templateUrl: './cash-add-concession.component.html',
    styleUrls: ['./cash-add-concession.component.css'],
    providers: [DatePipe]
})
export class CashAddConcessionComponent extends CashBaseService implements OnInit, OnDestroy {
    private sub: any;
    errorMessage: String;
    saveMessage: String;
    showHide = false;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    legalEntity: LegalEntity;
    sapbpid: number;

    subHeading: string;
    title: string;

    selectedConditionTypes: ConditionType[];
    isLoading = true;
    observableLatestCrsOrMrs: Observable<number>;
    latestCrsOrMrs: number = 0;

    observablePeriods: Observable<Period[]>;
    periods: Period[];

    observablePeriodTypes: Observable<PeriodType[]>;
    periodTypes: PeriodType[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];

    observableClientAccounts: Observable<ClientAccount[]>;

    observableAccrualTypes: Observable<AccrualType[]>;

    observableChannelTypes: Observable<ChannelType[]>;

    observableTableNumbers: Observable<TableNumber[]>;

    constructor(private route: ActivatedRoute,
        formBuilder: FormBuilder,
        private location: Location,
        datepipe: DatePipe,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(CashConcessionService) private cashConcessionService,
        fileService: FileService,
        private baseComponentService: BaseComponentService) {
        super();
        this.formBuilder = formBuilder;
        this.datepipe = datepipe;
        this.fileService = fileService;
        this.riskGroup = new RiskGroup();
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];
        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.clientAccounts = [new ClientAccount()];
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];
        });

        this.cashConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRowsAdd()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

        this.getInitialData();

    }

    initConditionItemRows() {
        this.selectedConditionTypes.push(new ConditionType());

        return this.formBuilder.group({
            conditionType: [''],
            conditionProduct: [''],
            interestRate: [''],
            volume: [''],
            value: [''],
            conditionComment: [''],
            periodType: [''],
            period: ['']
        });
    }

    getInitialData() {
        if (this.riskGroupNumber != null && this.riskGroupNumber != 0) {
            Observable.forkJoin([
                this.lookupDataService.getChannelTypes(),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getAccrualTypes(),
                this.lookupDataService.getTableNumbers(ConcessionTypes.Cash),
                this.lookupDataService.getRiskGroup(this.riskGroupNumber),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Cash),
                this.cashConcessionService.getlatestCrsOrMrs(this.riskGroupNumber)
            ]).subscribe(results => {
                this.setInitialData(results, true);
            },
                error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                });
        }
        else if (this.sapbpid != null && this.sapbpid != 0) {
            Observable.forkJoin([
                this.lookupDataService.getChannelTypes(),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getAccrualTypes(),
                this.lookupDataService.getTableNumbers(ConcessionTypes.Cash),
                this.lookupDataService.getLegalEntity(this.sapbpid),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Cash)
            ]).subscribe(results => {
                this.setInitialData(results, false);
            },
                error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                });
        }
    }
    setInitialData(results: {}[], isForRiskGroup: boolean) {
        this.channelTypes = <any>results[0];
        this.periods = <any>results[1];
        this.periodTypes = <any>results[2];
        this.conditionTypes = <any>results[3];
        this.accrualTypes = <any>results[4];
        this.tableNumbers = <any>results[5];
        if (isForRiskGroup) {
            this.riskGroup = <any>results[6];
            this.latestCrsOrMrs = <any>results[8];
            this.subHeading = this.riskGroup.name;
            this.title = this.riskGroup.number.toString();
        }
        else {
            this.legalEntity = <any>results[6];
            this.subHeading = this.legalEntity.customerName;
            this.title = this.legalEntity.customerNumber;
        }

        this.clientAccounts = <any>results[7];


        const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];

        if (this.channelTypes)
            control.controls[0].get('channelType').setValue(this.channelTypes[0]);


        if (this.clientAccounts)
            control.controls[0].get('accountNumber').setValue(this.clientAccounts[0]);

        if (this.accrualTypes)
            control.controls[0].get('accrualType').setValue(this.accrualTypes[0]);

        if (this.tableNumbers) {
            control.controls[0].get('tableNumber').setValue(this.tableNumbers[0]);

            this.tableNumberChanged(0);
        }

        this.isLoading = false;
    }

    addNewConditionRow() {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0)
            control.push(this.initConditionItemRows());
    }

    copyConcessionRow(index: number) {

        const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];

        var newRow = this.initConcessionItemRowsAdd();

        if (control.controls[index].get('channelType').value)
            newRow.controls['channelType'].setValue(control.controls[index].get('channelType').value);

        if (control.controls[index].get('accountNumber').value)
            newRow.controls['accountNumber'].setValue(control.controls[index].get('accountNumber').value);

        if (control.controls[index].get('accrualType').value)
            newRow.controls['accrualType'].setValue(control.controls[index].get('accrualType').value);

        if (control.controls[index].get('tableNumber').value)
            newRow.controls['tableNumber'].setValue(control.controls[index].get('tableNumber').value);

        if (control.controls[index].get('tableNumber').value.baseRate)
            newRow.controls['baseRate'].setValue(control.controls[index].get('tableNumber').value.baseRate.toFixed(2));

        if (control.controls[index].get('tableNumber').value.adValorem)
            newRow.controls['adValorem'].setValue(control.controls[index].get('tableNumber').value.adValorem.toFixed(3));

        if (control.controls[index].get('expiryDate').value)
            newRow.controls['expiryDate'].setValue(control.controls[index].get('expiryDate').value);

        control.insert(index + 1, newRow);

    }

    deleteConcessionRow(index: number) {
        if (confirm("Are you sure you want to remove this row?")) {
            const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
            control.removeAt(index);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    onExpiryDateChanged(itemrow) {

        var validationErrorMessage = this.baseComponentService.expiringDateDifferenceValidation(itemrow.controls['expiryDate'].value);
        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
        }
    }

    getCashConcession(): CashConcession {

        var cashConcession = new CashConcession();
        cashConcession.concession = new Concession();

        if (this.riskGroup)
            cashConcession.concession.riskGroupId = this.riskGroup.id;
        if (this.legalEntity)
            cashConcession.concession.legalEntityId = this.legalEntity.id;

        if (this.cashConcessionForm.controls['smtDealNumber'].value)
            cashConcession.concession.smtDealNumber = this.cashConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.cashConcessionForm.controls['motivation'].value)
            cashConcession.concession.motivation = this.cashConcessionForm.controls['motivation'].value;
        else
            cashConcession.concession.motivation = '.';

        const concessions = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];

        for (let concessionFormItem of concessions.controls) {
            if (!cashConcession.cashConcessionDetails)
                cashConcession.cashConcessionDetails = [];

            let cashConcessionDetail = new CashConcessionDetail();

            if (concessionFormItem.get('channelType').value) {
                cashConcessionDetail.channelTypeId = concessionFormItem.get('channelType').value.id;
            } else {
                this.addValidationError("Channel type not selected");
            }

            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {
                this.onExpiryDateChanged(concessionFormItem);
                cashConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                this.addValidationError("Expiry date not selected");
            }

            if (concessionFormItem.get('accountNumber').value && concessionFormItem.get('accountNumber').value.legalEntityId) {
                cashConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                cashConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
            } else {
                this.addValidationError("Client account not selected");
            }

            if (concessionFormItem.get('tableNumber').value) {
                cashConcessionDetail.tableNumberId = concessionFormItem.get('tableNumber').value.id;
                cashConcessionDetail.adValorem = concessionFormItem.get('tableNumber').value.adValorem;

                if (concessionFormItem.get('tableNumber').value.baseRate)
                    cashConcessionDetail.baseRate = concessionFormItem.get('tableNumber').value.baseRate;
            } else {
                this.addValidationError("Table Number not selected");
            }

            if (concessionFormItem.get('accrualType').value) {
                cashConcessionDetail.accrualTypeId = concessionFormItem.get('accrualType').value.id;
            } else {
                this.addValidationError("Accrual type not selected");
            }

            cashConcession.cashConcessionDetails.push(cashConcessionDetail);

            let hasDuplicates = this.baseComponentService.HasDuplicateConcessionAccountChannel(
                cashConcession.cashConcessionDetails,
                concessionFormItem.get('channelType').value.id,
                concessionFormItem.get('accountNumber').value.legalEntityId,
                concessionFormItem.get('accountNumber').value.legalEntityAccountId);

            if (hasDuplicates) {
                this.addValidationError("Duplicate Account / Channel pricing found. Please select different account.");

                break;
            }
        }

        const conditions = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];

        let concessionConditionReturnObject = this.baseComponentService.getConsessionConditionData(conditions, cashConcession.concessionConditions, this.validationError);
        cashConcession.concessionConditions = concessionConditionReturnObject.concessionConditions;
        this.validationError = concessionConditionReturnObject.validationError;

        this.checkConcessionExpiryDate(cashConcession);

        return cashConcession;
    }

    onSubmit() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;


        var cashConcession = this.getCashConcession();

        cashConcession.concession.concessionType = ConcessionTypes.Cash;
        cashConcession.concession.type = "New";
        cashConcession.concession.comments = "Created";

        if (!this.validationError) {
            this.cashConcessionService.postNewCashData(cashConcession).subscribe(entity => {
                console.log("data saved");
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    setTwoNumberDecimal($event) {
        $event.target.value = this.baseComponentService.formatDecimal($event.target.value);
    }

    disableField(index: number, fieldname: string) {
        return this.disableFieldBase(
            this.selectedConditionTypes[index],
            fieldname,
            this.saveMessage == null,
            this.saveMessage != null
        );
    }

    goBack() {
        this.location.back();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
