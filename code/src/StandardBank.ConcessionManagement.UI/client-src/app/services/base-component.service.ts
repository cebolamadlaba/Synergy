import { Injectable } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { DecimalPipe } from '@angular/common';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';
import { UserService } from "../services/user.service";
import { User } from '../models/user';

declare var accounting: any;

@Injectable()
export class BaseComponentService {

    validationError: String[];
    aeUser: User;
    riskGroupAEUser: User;

    constructor(public router: Router, public userService: UserService) { }

    public HasDuplicateConcessionAccountProduct(concessionDetails: any[], productTypeId: number, legalEntityId: number, legalEntityAccountId: number): boolean {
        let duplicates = concessionDetails.filter((item) => {
            return item.productTypeId == productTypeId
                && item.legalEntityId == legalEntityId
                && item.legalEntityAccountId == legalEntityAccountId;
        });

        return duplicates.length > 1;
    }

    public HasDuplicateConcessionAccountChannel(concessionDetails: any[], channelTypeId: number, legalEntityId: number, legalEntityAccountId: number): boolean {
        let duplicates = concessionDetails.filter((item) => {
            return item.channelTypeId == channelTypeId
                && item.legalEntityId == legalEntityId
                && item.legalEntityAccountId == legalEntityAccountId;
        });

        return duplicates.length > 1;
    }

    public HasDuplicateConcessionAccountTransaction(concessionDetails: any[], transactionTypeId: number, legalEntityId: number, legalEntityAccountId: number): boolean {
        let duplicates = concessionDetails.filter((item) => {
            return item.transactionTypeId == transactionTypeId
                && item.legalEntityId == legalEntityId
                && item.legalEntityAccountId == legalEntityAccountId;
        });

        return duplicates.length > 1;
    }

    public HasDuplicateConcessionAccountChargeCode(concessionDetails: any[], fkChargeCodeId: number, legalEntityId: number, legalEntityAccountId: number): boolean {
        let duplicates = concessionDetails.filter((item) => {
            return item.fkChargeCodeId == fkChargeCodeId
                && item.legalEntityId == legalEntityId
                && item.legalEntityAccountId == legalEntityAccountId;
        });

        return duplicates.length > 1;
    }

    public HasDuplicateConcessionAccountTradeProduct(concessionDetails: any[], tradeProductTypeID: number, legalEntityId: number, legalEntityAccountId: number): boolean {
        let duplicates = concessionDetails.filter((item) => {
            return item.fkTradeProductId == tradeProductTypeID
                && item.legalEntityId == legalEntityId
                && item.legalEntityAccountId == legalEntityAccountId;
        });

        return duplicates.length > 1;
    }

    public unformat(itemValue: number) {
        return accounting.unformat(itemValue);
    }

    public addConcessionValidationError(validationDetail) {
        this.validationError.push(validationDetail);
    }

    public async checkForExistingConcessions(concessionListLength, url,riskGroupNumber, sapbpid) {
        this.validationError = [];

        await this.getUserRiskGroupDetails(riskGroupNumber);
        await this.getUserData();
        this.checkAEExistOnriskGroupNumber();
        
        if (concessionListLength > 0 ) {
            if (sapbpid == 0) {
                this.addConcessionValidationError("Please note that a concession already exists for the product you have selected in this Risk group. Please select the concession below and update");
            } else {
                this.addConcessionValidationError("Please note that a concession already exists for the product you have selected in this Legal Entity. Please select the concession below and update");
            }
        } else {
            if(this.validationError.length < 1) {
                this.router.navigate([url, riskGroupNumber, sapbpid]);
            }          
        }
    }

    public checkAEExistOnriskGroupNumber() {

        if (this.riskGroupAEUser.id != this.aeUser.accountExecutiveUserId) {
            this.addConcessionValidationError("The logged in user does not have access to the account in the Risk group." + this.riskGroupAEUser.firstName + " " + this.riskGroupAEUser.surname + " is the responsible person for this Risk Group, please refer this concession request to them");
        }

    }

    public formatDecimal(itemValue: number) {

        if (itemValue != null) {
            itemValue = this.unformat(itemValue);
            return new DecimalPipe('en-US').transform(itemValue, '1.2-2');
        }

        return 0.00;
    }

    public formatDecimalThree(itemValue: number) {

        if (itemValue != null) {
            itemValue = this.unformat(itemValue);
            return new DecimalPipe('en-US').transform(itemValue, '1.3-3');
        }

        return null;
    }

    public expiringDateDifferenceValidation(selectedExpiryDate: string) {

        var currentMonth = moment().month()
        var selectedExpiryDateMonth = moment(selectedExpiryDate).month();
        let monthsDifference = currentMonth - selectedExpiryDateMonth;

        if (monthsDifference < MOnthEnum.ThreeMonths) {
            return "Concession expiry date must be greater than 3 months";
        };
    }

    getUserData(): Promise<any> {
        return new Promise((resolve, reject) => {
            this.userService.getData().subscribe(user => {
            resolve(user);
            this.aeUser = user;
               
            });
        });
    }

    getUserRiskGroupDetails(riskGroupNumber): Promise<any>{
        return new Promise((resolve, reject) => {
            this.userService.getUserRiskGroupDetailsData(riskGroupNumber).subscribe(user => {
            resolve(user);
            this.riskGroupAEUser = user;
              
            });
        });
    }


}
