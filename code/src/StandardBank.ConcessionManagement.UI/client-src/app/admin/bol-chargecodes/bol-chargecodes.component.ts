import { Component, OnInit, ViewChild, Inject } from '@angular/core';

import { Location } from '@angular/common';
import { Observable } from "rxjs";
import { User } from '../../models/user';

import { BolConcessionService } from "../../services/bol-concession.service";
import { BolChargeCodeType } from "../../models/bol-chargecodetype";
import { BolChargeCode } from "../../models/bol-chargecode";
import { LookupDataService } from "../../services/lookup-data.service";

@Component({
    selector: 'app-business-centre',
    templateUrl: './bol-chargecodes.component.html',
    styleUrls: ['./bol-chargecodes.component.css']
})
export class BOLCHManagementComponent implements OnInit {

    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;
    isSaving = false;

    observable: Observable<BolChargeCodeType[]>;
    bolchargecodetypes: BolChargeCodeType[];
    selectedProduct: BolChargeCodeType;

    observableBolChargeCodes: Observable<BolChargeCode[]>;
    bolchargecodes: BolChargeCode[];
    bolchargecodesFiltered: BolChargeCode[];

    addBolChargeCodeModel: BolChargeCode;
    addBolChargeCodeTypeModel: BolChargeCodeType;

    observableErrors: Observable<string[]>;
    observableSave: Observable<boolean>;

    @ViewChild('addBOLChargeCodeModal') addBOLChargeCodeModal;
    @ViewChild('addBOLChargeCodeTypeModal') addBOLChargeCodeTypeModal;

    actionType: string;
    isBcmAEsLoading = false;
    canAdd = true;

    constructor(private location: Location, @Inject(LookupDataService) private lookupDataService, @Inject(BolConcessionService) private bolConcessionService) {
        this.addBolChargeCodeModel = new BolChargeCode();
        this.addBolChargeCodeModel.active = true;

        this.addBolChargeCodeTypeModel = new BolChargeCodeType();

        this.bolchargecodetypes = [new BolChargeCodeType()];
        this.bolchargecodes = [new BolChargeCode()];
        this.bolchargecodesFiltered = [new BolChargeCode()];
    }

    ngOnInit() {
        this.loadData(null);
    }

    loadData(productype: BolChargeCodeType) {
        this.isLoading = true;

        Observable.forkJoin([
            this.lookupDataService.getBOLChargeCodeTypes(),
            this.lookupDataService.getBOLChargeCodes()
        ]).subscribe(results => {

            this.bolchargecodetypes = <any>results[0];
            this.bolchargecodes = <any>results[1];
            this.bolchargecodesFiltered = this.bolchargecodes;

            if (this.bolchargecodetypes.length > 0) {

                if (productype == null) {
                    this.selectedProduct = this.bolchargecodetypes[0];
                }
                else {
                    this.selectedProduct = this.bolchargecodetypes.filter(c => c.pkChargeCodeTypeId == productype.pkChargeCodeTypeId)[0];

                }

                this.bolchargecodesFiltered = this.bolchargecodes.filter(re => re.fkChargeCodeTypeId == this.selectedProduct.pkChargeCodeTypeId);

            }

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    FilterBOLProducts(selection: BolChargeCodeType) {

        this.selectedProduct = selection;
        this.bolchargecodesFiltered = this.bolchargecodes.filter(re => re.fkChargeCodeTypeId == selection.pkChargeCodeTypeId);
    }

    createupdateBOLChargeCode() {

        this.isSaving = true;

        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        if (!this.validationError) {

            this.addBolChargeCodeModel.fkChargeCodeTypeId = this.selectedProduct.pkChargeCodeTypeId;
            this.addBolChargeCodeModel.active = true;


            this.bolConcessionService.createupdateBOLChargeCode(this.addBolChargeCodeModel).subscribe(entity => {
                console.log("data saved");

                if (this.actionType == "Add") {
                    this.saveMessage = "Business Online Charge Code created successfully!";
                }
                else if (this.actionType == "Edit") {
                    this.saveMessage = "Business Online Charge Code updated successfully!";
                }               

                this.isLoading = false;
                this.isSaving = false;

                this.loadData(this.selectedProduct);


            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
                this.isSaving = false;
            });

        } else {
            this.isLoading = false;
        }

        //return false;

    }


    createBOLChargeCodeType() {

        this.isSaving = true;

        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        if (!this.validationError) {
            this.bolConcessionService.postNewBolChargeCodeType(this.addBolChargeCodeTypeModel).subscribe(entity => {
                console.log("data saved");

                this.saveMessage = "Product created successfully!";
                this.isLoading = false;
                this.isSaving = false;

                this.loadData(this.selectedProduct);

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
                this.isSaving = false;
            });

        } else {
            this.isLoading = false;
        }
    }



    editBOLChargeCode(bolchargecodeOption: BolChargeCode) {

        this.addBOLChargeCodeModal.show();
        this.actionType = "Edit";
        this.addBolChargeCodeModel = bolchargecodeOption;
    }

    deleteBOLChargeCode(bolchargecodeOption: BolChargeCode) {

        this.actionType = "Delete";
        if (confirm("Are you sure you want to disable the Charge code" + bolchargecodeOption.chargeCode + " ?")) {

            bolchargecodeOption.active = false;

            this.bolConcessionService.createupdateBOLChargeCode(bolchargecodeOption).subscribe(entity => {
                console.log("data saved");

               if (this.actionType == "Delete") {
                    this.saveMessage = "Business Online Charge Code disabled successfully!";
                }

                this.isLoading = false;
                this.isSaving = false;

                this.loadData(this.selectedProduct);


            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
                this.isSaving = false;
            });

        }
    }

    addBOLChargeCode() {
        this.actionType = "Add";
        this.addBolChargeCodeModel = new BolChargeCode();
        this.addBolChargeCodeModel.active = true;

        if (this.selectedProduct != null) {

            this.addBolChargeCodeModel.fkChargeCodeTypeId = this.selectedProduct.pkChargeCodeTypeId;
        }

        this.addBolChargeCodeModel.description = "";
        this.addBolChargeCodeModel.chargeCode = "";
        this.addBolChargeCodeModel.length = 4;

        this.addBOLChargeCodeModal.show();
    }

    addBOLChargeCodeType() {

        this.actionType = "Add";
        this.addBolChargeCodeTypeModel = new BolChargeCodeType();
        this.addBolChargeCodeTypeModel.description = "";

        this.addBOLChargeCodeModal.hide();
        this.addBOLChargeCodeTypeModal.show();
    }

    closeBOLChargeCodeType() {

        this.addBOLChargeCodeTypeModal.hide();
        this.addBOLChargeCodeModal.show();
    }

    goBack() {
        this.location.back();
    }
}
