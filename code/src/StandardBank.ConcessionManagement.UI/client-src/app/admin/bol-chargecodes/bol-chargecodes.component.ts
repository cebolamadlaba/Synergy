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

    observableErrors: Observable<string[]>;
    observableSave: Observable<boolean>;   

    @ViewChild('addBOLChargeCodeModal') addBOLChargeCodeModal;
    @ViewChild('addBS') addBusinessCentreModal2;

    actionType: string;
    isBcmAEsLoading = false;
    canAdd = true;

    constructor(private location: Location, @Inject(LookupDataService) private lookupDataService, @Inject(BolConcessionService) private bolConcessionService) {       
        this.addBolChargeCodeModel = new BolChargeCode();
       
        this.bolchargecodetypes = [new BolChargeCodeType()];
        this.bolchargecodes = [new BolChargeCode()];
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.isLoading = true;

        Observable.forkJoin([         
            this.lookupDataService.getBOLChargeCodeTypes(),
            this.lookupDataService.getBOLChargeCodes()
        ]).subscribe(results => {
          
            this.bolchargecodetypes = <any>results[0];         

            this.bolchargecodes = <any>results[1];
            this.bolchargecodesFiltered = this.bolchargecodes;

            if (this.bolchargecodetypes.length > 0) {
                this.selectedProduct = this.bolchargecodetypes[0];

            }

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    createBOLChargeCode() {

        this.isSaving = true;     

        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;
      
        if (!this.validationError) {
            //this.bolConcessionService.postNewBolChargeCode(this.addBolChargeCodeModel).subscribe(entity => {
            //    console.log("data saved");
              
            //    this.saveMessage = "Region created successfully!";
            //    this.isLoading = false;
            //}, error => {
            //    this.errorMessage = <any>error;
            //    this.isLoading = false;
            //});

            this.saveMessage = "Region created successfully!";
             this.isLoading = false;

            this.addBOLChargeCodeModal.hide()

        } else {
            this.isLoading = false;
        }

    }


    createBusinessCentre() {
        this.isSaving = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        //this.addBusinessCentreManagementModel.isActive = true;
        //this.addBusinessCentreManagementModel.accountExecutives = this.selectedAccountExecutives;

        //this.observableErrors = this.businessCentreService.validateBusinessCentreManagementModel(this.addBusinessCentreManagementModel);
        //this.observableErrors.subscribe(errors => {
        //    if (errors != null && errors.length > 0) {
        //        this.validationError = errors;
        //        this.isSaving = false;
        //    } else {
        //        this.observableSave = this.businessCentreService.createBusinessCentreManagementModel(this.addBusinessCentreManagementModel);
        //        this.observableSave.subscribe(errors => {

        //            if (this.addBusinessCentreManagementModel.centreId != null && this.addBusinessCentreManagementModel.centreId > 0) {
        //                this.saveMessage = "Business Centre updated successfully!";
        //            } else {
        //                this.saveMessage = "Business Centre created successfully!";
        //            }
                    
        //            this.addBusinessCentreManagementModel = new BusinessCentreManagementModel();
        //            this.selectedAccountExecutive = null;
        //            this.selectedAccountExecutives = null;

        //            this.isSaving = false;

        //            //after saving reload the data
        //            this.loadData();
        //        }, error => {
        //            this.isSaving = false;
        //            this.errorMessage = <any>error;
        //        });
        //    }
        //}, error => {
        //    this.isSaving = false;
        //    this.errorMessage = <any>error;
        //});
    }

    addProduct() {
        //if (this.selectedAccountExecutive != null) {
        //    if (this.selectedAccountExecutives == null) {
        //        this.selectedAccountExecutives = [];
        //    }

        //    if (!this.selectedAccountExecutives.find(result => result.id == this.selectedAccountExecutive.id)) {
        //        this.selectedAccountExecutives.push(this.selectedAccountExecutive);
        //    }
        //}
    }

    removeAccountExecutive(index: number) {
        //this.selectedAccountExecutives.splice(index, 1);
    }

    //editBusinessCentre(businessCentreManagementModel: BusinessCentreManagementModel) {
        //this.isBcmAEsLoading = true;
        //this.addBusinessCentreModal.show();
        //this.actionType = "Edit";
        //this.addBusinessCentreManagementModel = businessCentreManagementModel;
        //this.selectedAccountExecutive = null;
        //this.selectedAccountExecutives = null;

        //this.observableSelectedAccountExecutives = this.businessCentreService.getBusinessCentreAccountExecutives(businessCentreManagementModel.centreId);
        //this.observableSelectedAccountExecutives.subscribe(result => {
        //    if (result != null && result.length > 0)
        //        this.selectedAccountExecutives = result;

        //    this.isBcmAEsLoading = false;
        //}, error => {
        //    this.addBusinessCentreModal.hide();
        //    this.isBcmAEsLoading = false;
        //    this.errorMessage = <any>error;
        //});
   // }

    addBOLChargeCode() {
        this.actionType = "Add";
        this.addBolChargeCodeModel = new BolChargeCode();

        if (this.selectedProduct != null) {

            this.addBolChargeCodeModel.fkChargeCodeTypeId = this.selectedProduct.pkChargeCodeTypeId;
        }

        this.addBolChargeCodeModel.description = "";
        this.addBolChargeCodeModel.chargeCode = "";
        this.addBolChargeCodeModel.length = 4;
       
        this.addBOLChargeCodeModal.show();
    }

    addBusinessCentre2() {

        //this.addBusinessCentreModal.hide();

        //this.actionType = "Add2";
        //this.addBusinessCentreManagementModel = new BusinessCentreManagementModel();
        //this.selectedAccountExecutive = null;
        //this.selectedAccountExecutives = null;
        //this.addBusinessCentreModal2.show();
    }

    goBack() {
        this.location.back();
    }

}
