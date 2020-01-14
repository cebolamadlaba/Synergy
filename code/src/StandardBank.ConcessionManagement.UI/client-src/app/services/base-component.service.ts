import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
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
    isAppprovingOrDeclining: boolean = false;
    isRenewing: boolean = false;

    constructor(public router: Router, public userService: UserService) {

    }

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

    // BOL
    public HasDuplicateConcessionUserIdChargeCode(concessionDetails: any[], fkChargeCodeId: number, legalEntityBOLUserId: number): boolean {
        let duplicates = concessionDetails.filter((item) => {
            return item.fkChargeCodeId == fkChargeCodeId
                && item.fkLegalEntityBOLUserId == legalEntityBOLUserId;
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
        if (!this.validationError)
            this.validationError = [];
        this.validationError.push(validationDetail);
    }

    public async checkForExistingConcessions(concessionListLength, url, riskGroupNumber, sapbpid) {

        await this.getUserRiskGroupDetails(riskGroupNumber, sapbpid);
        await this.getUserData();
        this.checkAEExistOnriskGroupNumber();

        if (concessionListLength > 0) {

            // FOR TESTING: comment out the first part of the if-statement.
            //    if (sapbpid == 0) {
            //        this.addConcessionValidationError("Please note that a concession already exists for the product you have selected in this Risk group. Please select the concession below and update");
            //    } else {
            //        this.addConcessionValidationError("Please note that a concession already exists for the product you have selected in this Legal Entity. Please select the concession below and update");
            //    }

            //} else {
            if (this.validationError == undefined) {
                this.router.navigate([url, riskGroupNumber, sapbpid]);
            }
        }
    }

    public checkAEExistOnriskGroupNumber() {

        if (this.aeUser.accountExecutiveUserId == null && this.aeUser.isRequestor) {
            this.aeUser.accountExecutiveUserId = this.aeUser.id;
        }

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

    public GetTodayDate() {
        return new Date().toISOString().split('T')[0];
    }

    public formatDecimalThree(itemValue: number) {

        if (itemValue != null) {
            itemValue = this.unformat(itemValue);
            return new DecimalPipe('en-US').transform(itemValue, '1.3-3');
        }

        return null;
    }

    public expiringDateDifferenceValidation(selectedExpiryDate: string) {

        var currentDate = moment();
        var expDate = moment(selectedExpiryDate);
        var futureMonth1 = moment(currentDate).add(MOnthEnum.ThreeMonths, 'M').format('YYYY-MM-DD');
        var futureMonth = moment(futureMonth1);
        if (expDate <= futureMonth) {
            return "Concession expiry date should be later than " + futureMonth.format('DD-MM-YYYY');
        }
    }

    public expiringDateDifferenceValidationForView(selectedExpiryDate: string, createdDate: string) {
        var currentDate = moment(createdDate);
        var expDate = moment(selectedExpiryDate);
        var futureMonth1 = moment(currentDate).add(MOnthEnum.ThreeMonths, 'M').format('YYYY-MM-DD');
        var futureMonth = moment(futureMonth1);
        if (expDate <= futureMonth) {
            return "Concession expiry date should be later than " + futureMonth.format('DD-MM-YYYY');
        }
    }

    getUserData(): Promise<any> {
        return new Promise((resolve, reject) => {
            this.userService.getData().subscribe(user => {
                resolve(user);
                this.aeUser = user;

            });
        });
    }

    getUserRiskGroupDetails(riskGroupNumber, sapbpid): Promise<any> {

        var sapbpidOrRiskGroupNumber = riskGroupNumber == 0 ? sapbpid : riskGroupNumber;

        return new Promise((resolve, reject) => {
            this.userService.getUserRiskGroupDetailsData(sapbpidOrRiskGroupNumber).subscribe(user => {
                resolve(user);
                this.riskGroupAEUser = user;

            });
        });
    }

}
