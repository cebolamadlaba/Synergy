import { Component, OnInit, ViewChild, Inject } from '@angular/core';

import { Location } from '@angular/common';
import { Observable } from "rxjs";
import { User } from '../../models/user';


import { ChannelType } from "../../models/channel-type";
import { TableNumber } from "../../models/table-number";
import { LookupDataService } from "../../services/lookup-data.service";

import { CashConcessionService } from "../../services/cash-concession.service";
import { ConcessionTypes } from '../../constants/concession-types';

@Component({
    selector: 'app-business-centre',
    templateUrl: './channel-types.component.html',
    styleUrls: ['./channel-types.component.css']
})
export class ChannelTypesManagementComponent implements OnInit {

    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;
    isSaving = false;

    observable: Observable<ChannelType[]>;
    channeltypes: ChannelType[];
    selectedProduct: ChannelType;

    observableTableNumbers: Observable<TableNumber[]>;
    tablenumbers: TableNumber[];
    tablenumbersFiltered: TableNumber[];

    addTableNumberModel: TableNumber;
    addChannelTypeModel: ChannelType;

    observableErrors: Observable<string[]>;
    observableSave: Observable<boolean>;

    @ViewChild('addTariffTableModal') addTariffTableModal;
    @ViewChild('addTypeModal') addTypeModal;

    actionType: string;
    isBcmAEsLoading = false;
    canAdd = true;

    constructor(private location: Location, @Inject(CashConcessionService) private cashConcessionservice, @Inject(LookupDataService) private lookupDataService) {

        this.addTableNumberModel = new TableNumber();
        this.addChannelTypeModel = new ChannelType();

        this.channeltypes = [new ChannelType()];
        this.tablenumbers = [new TableNumber()];
        this.tablenumbersFiltered = [new TableNumber()];
    }

    ngOnInit() {
        this.loadData(null);
    }

    loadData(productype: ChannelType) {
        this.isLoading = true;

        Observable.forkJoin([         

              this.lookupDataService.getAllChannelTypes(),
              this.lookupDataService.getTableNumbersAll(ConcessionTypes.Cash),

        ]).subscribe(results => {

            this.channeltypes = <any>results[0];
            this.tablenumbers = <any>results[1];
            this.tablenumbersFiltered = this.tablenumbers;

            if (this.channeltypes.length > 0) {

                if (productype == null) {
                    this.selectedProduct = this.channeltypes[0];
                }
                else {
                    this.selectedProduct = this.channeltypes.filter(c => c.id == productype.id)[0];

                }

               // this.tablenumbersFiltered = this.tablenumbers.filter(re => re. == this.selectedProduct.id);

            }

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    filterDetails(selection: ChannelType) {

        this.selectedProduct = selection;
       // this.tablenumbersFiltered = this.tablenumbers.filter(re => re.transactionTypeId == selection.id);
    }

    createupdateDetail() {

        this.isSaving = true;

        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        if (!this.validationError) {

           // this.addTableNumberModel. = ConcessionTypes.Cash;
           this.addTableNumberModel.isActive = true;


            this.cashConcessionservice.createupdateTableNumber(this.addTableNumberModel).subscribe(entity => {
                console.log("data saved");

                if (this.actionType == "Add") {
                    this.saveMessage = "Cash Table created successfully!";
                }
                else if (this.actionType == "Edit") {
                    this.saveMessage = "Cash Table updated successfully!";
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
    }

    saveType() {

        this.isSaving = true;

        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        if (!this.validationError) {
            this.cashConcessionservice.createChannelType(this.addChannelTypeModel).subscribe(entity => {
                console.log("data saved");

                this.saveMessage = "Channel Type created successfully!";
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

    editDetails(tablenumberOption: TableNumber) {

        this.addTariffTableModal.show();
        this.actionType = "Edit";
        this.addTableNumberModel = tablenumberOption;
    }

    disableDetails(tablenumberOption: TableNumber) {

        this.actionType = "Delete";
        if (confirm("Are you sure you want to disable Cash table " + tablenumberOption.tariffTable + " ?")) {

            tablenumberOption.isActive = false;

            this.cashConcessionservice.createupdateTableNumber(tablenumberOption).subscribe(entity => {
                console.log("data saved");

               if (this.actionType == "Delete") {
                    this.saveMessage = "Cash table disabled successfully!";
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

    enableDetails(tablenumberOption: TableNumber) {

        this.actionType = "Delete";
        if (confirm("Are you sure you want to enable Cash table " + tablenumberOption.tariffTable + " ?")) {

            tablenumberOption.isActive = true;

            this.cashConcessionservice.createupdateTableNumber(tablenumberOption).subscribe(entity => {
                console.log("data saved");

                if (this.actionType == "Delete") {
                    this.saveMessage = "Cash table enabled successfully!";
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

    addDetails() {
        this.actionType = "Add";
        this.addTableNumberModel = new TableNumber();
        this.addTableNumberModel.isActive = true;

        if (this.selectedProduct != null) {

           // this.addTableNumberModel.transactionTypeId = this.selectedProduct.id;
        }

        this.addTableNumberModel.adValorem = 0;
        this.addTableNumberModel.baseRate = 0;
        this.addTableNumberModel.displayText = "";
        this.addTableNumberModel.tariffTable = 0;

        this.addTariffTableModal.show();
    }

    addType() {

        this.actionType = "Add";
        this.addChannelTypeModel = new ChannelType();
        this.addChannelTypeModel.description = "";

        this.addTariffTableModal.hide();
        this.addTypeModal.show();
    }

    closeType() {

        this.addTypeModal.hide();
        this.addTariffTableModal.show();
    }

    goBack() {
        this.location.back();
    }
}
