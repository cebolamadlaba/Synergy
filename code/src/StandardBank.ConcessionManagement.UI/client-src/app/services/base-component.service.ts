import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Router, RouterModule } from '@angular/router';
import { DecimalPipe } from '@angular/common';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';
import { UserService } from "../services/user.service";
import { User } from '../models/user';
import { FormArray } from '@angular/forms';
import { ConcessionCondition } from '../models/concession-condition';
import { ConcessionConditionReturnObject } from '../models/concession-condition-return-object';
import { GlmsConcessionDetail } from '../models/glms-concession-detail';

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

    // GLMS
    public HasDuplicateConcessionInterestPricingCategoryAndInterestType(glmsConcessionDetails: GlmsConcessionDetail[], interstPricingCategoryId: number, interestTypeId: number): boolean {

        let duplicates = glmsConcessionDetails.filter(item => {
            return item.interestPricingCategoryId == interstPricingCategoryId &&
                item.interestTypeId == interestTypeId;
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
            if (sapbpid == 0) {
                this.addConcessionValidationError("Please note that a concession already exists for the product you have selected in this Risk group. Please select the concession below and update");
            } else {
                this.addConcessionValidationError("Please note that a concession already exists for the product you have selected in this Legal Entity. Please select the concession below and update");
            }

        } else {
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

        return "0.00";
    }

    public formatDecimalThree(itemValue: number) {

        if (itemValue != null) {
            itemValue = this.unformat(itemValue);
            return new DecimalPipe('en-US').transform(itemValue, '1.3-3');
        }

        return "0.00";
    }

    public GetTodayDate() {
        return new Date().toISOString().split('T')[0];
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

    getConsessionConditionData(conditions: FormArray, concessionConditions: ConcessionCondition[], validationError: String[]): ConcessionConditionReturnObject {

        this.validationError = validationError;

        for (let conditionFormItem of conditions.controls) {
            if (!concessionConditions)
                concessionConditions = [];

            let concessionCondition = new ConcessionCondition();

            if (conditionFormItem.get('conditionType').value)
                concessionCondition.conditionTypeId = conditionFormItem.get('conditionType').value.id;
            else
                this.addConcessionValidationError("Condition type not selected");

            if (conditionFormItem.get('conditionProduct').value)
                concessionCondition.conditionProductId = conditionFormItem.get('conditionProduct').value.id;
            else
                this.addConcessionValidationError("Condition product not selected");

            if (conditionFormItem.get('interestRate').value)
                concessionCondition.interestRate = conditionFormItem.get('interestRate').value;

            if (conditionFormItem.get('volume').value)
                concessionCondition.conditionVolume = conditionFormItem.get('volume').value;

            if (conditionFormItem.get('value').value == null || (<string>conditionFormItem.get('value').value).length < 1) {
                var value = conditionFormItem.get('conditionType').value;
                if (value != null && value.enableConditionValue == true)
                    this.addConcessionValidationError("Conditions: 'Value' is a mandatory field");
            }
            else if (conditionFormItem.get('value').value)
                concessionCondition.conditionValue = conditionFormItem.get('value').value;

            if (conditionFormItem.get('conditionComment').value)
                concessionCondition.conditionComment = conditionFormItem.get('conditionComment').value;

            if (conditionFormItem.get('periodType').value) {
                concessionCondition.periodTypeId = conditionFormItem.get('periodType').value.id;
            } else {
                this.addConcessionValidationError("Period type not selected");
            }

            if (conditionFormItem.get('period').value) {
                concessionCondition.periodId = conditionFormItem.get('period').value.id;
            } else {
                this.addConcessionValidationError("Period not selected");
            }

            if (conditionFormItem.get('periodType').value.description == 'Once-off' && conditionFormItem.get('period').value.description == 'Monthly') {
                this.addConcessionValidationError("Conditions: The Period 'Monthly' cannot be selected for Period Type 'Once-off'");
            }

            concessionConditions.push(concessionCondition);
        }

        let concessionConditionReturnObject = new ConcessionConditionReturnObject();
        concessionConditionReturnObject.concessionConditions = concessionConditions;
        concessionConditionReturnObject.validationError = this.validationError;

        return concessionConditionReturnObject;
    }

}
