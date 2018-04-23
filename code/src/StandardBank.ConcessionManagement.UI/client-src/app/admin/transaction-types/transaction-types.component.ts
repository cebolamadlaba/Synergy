import { Component, OnInit, ViewChild, Inject } from '@angular/core';

import { Location } from '@angular/common';
import { Observable } from "rxjs";
import { User } from '../../models/user';


import { TransactionType } from "../../models/transaction-type";
import { TransactionTableNumber } from "../../models/transaction-table-number";

import { AdminTransactionTablesService } from "../../services/admin-transaction-tables.service";

@Component({
    selector: 'app-business-centre',
    templateUrl: './transaction-types.component.html',
    styleUrls: ['./transaction-types.component.css']
})
export class TransactionTypesManagementComponent implements OnInit {

    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;
    isSaving = false;

    observable: Observable<TransactionType[]>;
    transactiontypes: TransactionType[];
    selectedProduct: TransactionType;

    observableTableNumbers: Observable<TransactionTableNumber[]>;
    tablenumbers: TransactionTableNumber[];
    tablenumbersFiltered: TransactionTableNumber[];

    addTableNumberModel: TransactionTableNumber;
    addTransactionTypeModel: TransactionType;

    observableErrors: Observable<string[]>;
    observableSave: Observable<boolean>;

    @ViewChild('addTariffTableModal') addTariffTableModal;
    @ViewChild('addTypeModal') addTypeModal;

    actionType: string;
    isBcmAEsLoading = false;
    canAdd = true;

    constructor(private location: Location, @Inject(AdminTransactionTablesService) private admintransactionService) {
        this.addTableNumberModel = new TransactionTableNumber();

        this.addTransactionTypeModel = new TransactionType();

        this.transactiontypes = [new TransactionType()];
        this.tablenumbers = [new TransactionTableNumber()];
        this.tablenumbersFiltered = [new TransactionTableNumber()];
    }

    ngOnInit() {
        this.loadData(null);
    }

    loadData(productype: TransactionType) {
        this.isLoading = true;

        Observable.forkJoin([
            this.admintransactionService.getTransactionTypes(true),
            this.admintransactionService.getTransactionTableNumbers(true)
        ]).subscribe(results => {

            this.transactiontypes = <any>results[0];
            this.tablenumbers = <any>results[1];
            this.tablenumbersFiltered = this.tablenumbers;

            if (this.transactiontypes.length > 0) {

                if (productype == null) {
                    this.selectedProduct = this.transactiontypes[0];
                }
                else {
                    this.selectedProduct = this.transactiontypes.filter(c => c.id == productype.id)[0];

                }

                this.tablenumbersFiltered = this.tablenumbers.filter(re => re.transactionTypeId == this.selectedProduct.id);

            }

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    filterDetails(selection: TransactionType) {

        this.selectedProduct = selection;
        this.tablenumbersFiltered = this.tablenumbers.filter(re => re.transactionTypeId == selection.id);
    }

    createupdateDetail() {

        this.isSaving = true;

        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        if (!this.validationError) {

            this.addTableNumberModel.transactionTypeId = this.selectedProduct.id;
            this.addTableNumberModel.isActive = true;


            this.admintransactionService.createupdateTransactionTableNumber(this.addTableNumberModel).subscribe(entity => {
                console.log("data saved");

                if (this.actionType == "Add") {
                    this.saveMessage = "Transactional Table Number created successfully!";
                }
                else if (this.actionType == "Edit") {
                    this.saveMessage = "Transactional Table Number updated successfully!";
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
            this.admintransactionService.postNewTransactionType(this.addTransactionTypeModel).subscribe(entity => {
                console.log("data saved");

                this.saveMessage = "Transaction Type created successfully!";
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

    editDetails(tablenumberOption: TransactionTableNumber) {

        this.addTariffTableModal.show();
        this.actionType = "Edit";
        this.addTableNumberModel = tablenumberOption;
    }

    deleteDetails(tablenumberOption: TransactionTableNumber) {

        this.actionType = "Delete";
        if (confirm("Are you sure you want to disable Transaction TableNumber" + tablenumberOption.tariffTable + " ?")) {

            tablenumberOption.isActive = false;

            this.admintransactionService.createupdateTransactionTableNumber(tablenumberOption).subscribe(entity => {
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

    addDetails() {
        this.actionType = "Add";
        this.addTableNumberModel = new TransactionTableNumber();
        this.addTableNumberModel.isActive = true;

        if (this.selectedProduct != null) {

            this.addTableNumberModel.transactionTypeId = this.selectedProduct.id;
        }

        this.addTableNumberModel.adValorem = 0;
        this.addTableNumberModel.fee = 0;
        this.addTableNumberModel.displayText = "";
        this.addTableNumberModel.tariffTable = 0;

        this.addTariffTableModal.show();
    }

    addType() {

        this.actionType = "Add";
        this.addTransactionTypeModel = new TransactionType();
        this.addTransactionTypeModel.description = "";

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
