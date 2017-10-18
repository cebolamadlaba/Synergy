import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { AccrualType } from "../models/accrual-type";
import { ChannelType } from "../models/channel-type";
import { ClientAccount } from "../models/client-account";
import { ConcessionType } from "../models/concession-type";
import { ConditionType } from "../models/condition-type";
import { ConditionProduct } from "../models/condition-product";
import { Period } from "../models/period";
import { PeriodType } from "../models/period-type";
import { ProductType } from "../models/product-type";
import { ReviewFeeType } from "../models/review-fee-type";
import { RiskGroup } from "../models/risk-group";
import { TransactionType } from "../models/transaction-type";
import { TableNumber } from "../models/table-number";

@Injectable()
export class LookupDataService {

    constructor(private http: Http) {
    }

    getAccrualTypes(): Observable<AccrualType[]> {
        const url = "/api/Concession/AccrualTypes";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getChannelTypes(): Observable<ChannelType[]> {
        const url = "/api/Concession/ChannelTypes";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getClientAccounts(riskGroupNumber): Observable<ClientAccount[]> {
        const url = "/api/Concession/ClientAccounts/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getConditionTypes(): Observable<ConditionType[]> {
        const url = "/api/Condition/ConditionTypes";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getPeriods(): Observable<Period[]> {
        const url = "/api/Condition/Periods";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getPeriodTypes(): Observable<PeriodType[]> {
        const url = "/api/Condition/PeriodTypes";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getProductTypes(concessionType): Observable<ProductType[]> {
        const url = "/api/Concession/ProductTypes/" + concessionType;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getReviewFeeTypes(): Observable<ReviewFeeType[]> {
        const url = "/api/Concession/ReviewFeeTypes";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getRiskGroup(riskGroupNumber): Observable<RiskGroup> {
        const url = "/api/Pricing/RiskGroup/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getTransactionTypes(concessionType): Observable<TransactionType[]> {
        const url = "/api/Concession/TransactionTypes/" + concessionType;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getTableNumbers(concessionType): Observable<TableNumber[]> {
        const url = "/api/Concession/TableNumbers/" + concessionType;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    private extractData(response: Response) {
        let body = response.json();
        return body;
    }

    private handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }

}

@Injectable()
export class MockLookupDataService extends LookupDataService {
    accrualTypeModel = [new AccrualType()];
    channelTypeModel = [new ChannelType()];
    clientAccountModel = [new ClientAccount()];
    conditionTypeModel = [new ConditionType()];
    periodModel = [new Period()];
    periodTypeModel = [new PeriodType()];
    productTypeModel = [new ProductType()];
    reviewFeeTypeModel = [new ReviewFeeType()];
    riskGroupModel = new RiskGroup();
    transactionTypeModel = [new TransactionType()];
    tableNumberModel = [new TableNumber()];

    getAccrualTypes(): Observable<AccrualType[]> {
        this.accrualTypeModel[0].id = 1;
        this.accrualTypeModel[0].description = "Test Accrual Type";
        return Observable.of(this.accrualTypeModel);
    }

    getChannelTypes(): Observable<ChannelType[]> {
        this.channelTypeModel[0].id = 1;
        this.channelTypeModel[0].description = "Test Channel Type";
        return Observable.of(this.channelTypeModel);
    }

    getClientAccounts(riskGroupNumber): Observable<ClientAccount[]> {
        this.clientAccountModel[0].accountNumber = "Test Account Number";
        this.clientAccountModel[0].legalEntityAccountId = 1;
        this.clientAccountModel[0].legalEntityId = 1;
        this.clientAccountModel[0].riskGroupId = 1;
        this.clientAccountModel[0].customerName = "Test Customer Name";
        return Observable.of(this.clientAccountModel);
    }

    getConditionTypes(): Observable<ConditionType[]> {
        this.conditionTypeModel[0].id = 1;
        this.conditionTypeModel[0].description = "Test Condition Type";
        this.conditionTypeModel[0].conditionProducts = [new ConditionProduct()];
        return Observable.of(this.conditionTypeModel);
    }

    getPeriods(): Observable<Period[]> {
        this.periodModel[0].id = 1;
        this.periodModel[0].description = "Test Period";
        return Observable.of(this.periodModel);
    }

    getPeriodTypes(): Observable<PeriodType[]> {
        this.periodTypeModel[0].id = 1;
        this.periodTypeModel[0].description = "Test Period Type";
        return Observable.of(this.periodTypeModel);
    }

    getProductTypes(concessionType): Observable<ProductType[]> {
        this.productTypeModel[0].id = 1;
        this.productTypeModel[0].description = "Product Type Test";
        this.productTypeModel[0].concessionType = new ConcessionType();
        this.productTypeModel[0].concessionType.id = 1;
        this.productTypeModel[0].concessionType.description = concessionType;
        return Observable.of(this.productTypeModel);
    }

    getReviewFeeTypes(): Observable<ReviewFeeType[]> {
        this.reviewFeeTypeModel[0].id = 1;
        this.reviewFeeTypeModel[0].description = "Review Fee Test";
        return Observable.of(this.reviewFeeTypeModel);
    }

    getRiskGroup(riskGroupNumber): Observable<RiskGroup> {
        this.riskGroupModel.id = 1;
        this.riskGroupModel.name = "Risk Group Test";
        this.riskGroupModel.number = 1;
        return Observable.of(this.riskGroupModel);
    }

    getTransactionTypes(concessionType): Observable<TransactionType[]> {
        this.transactionTypeModel[0].id = 1;
        this.transactionTypeModel[0].description = "Transaction Type Test";
        this.transactionTypeModel[0].concessionType = "Transactional";
        this.transactionTypeModel[0].concessionTypeId = 1;
        return Observable.of(this.transactionTypeModel);
    }

    getTableNumbers(concessionType): Observable<TableNumber[]> {
        this.tableNumberModel[0].id = 1;
        this.tableNumberModel[0].adValorem = 2;
        this.tableNumberModel[0].baseRate = 3;
        this.tableNumberModel[0].tariffTable = 4;
        this.tableNumberModel[0].displayText = "Test Display Text";
        return Observable.of(this.tableNumberModel);
    }
}