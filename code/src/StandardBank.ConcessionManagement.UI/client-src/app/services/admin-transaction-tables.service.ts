import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

//import { ConcessionType } from "../models/concession-type";
//import { TableNumber } from "../models/table-number";

//import { ChannelType } from "../models/channel-type";
//import { TransactionType } from "../models/transaction-type";

import { TransactionType } from "../models/transaction-type";
import { TransactionTableNumber } from "../models/transaction-table-number";



@Injectable()
export class AdminTransactionTablesService {

    constructor(private http: Http) {
    }   

    //getChannelTypes(): Observable<ChannelType[]> {
    //    const url = "/api/Concession/ChannelTypes";
    //    return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    //}

    //getTransactionTypes(concessionType): Observable<TransactionType[]> {
    //    const url = "/api/Concession/TransactionTypes/" + concessionType;
    //    return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    //}

    //getTableNumbers(isActive): Observable<TableNumber[]> {
    //    const url = "/api/Concession/ActiveTableNumbers/" + isActive;
    //    return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    //}


    postNewTransactionType(transactiontype: TransactionType): Observable<TransactionType> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Transactional/CreateTransactionType";
        return this.http.post(url, transactiontype, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    createupdateTransactionTableNumber(transactiontablenumber: TransactionTableNumber): Observable<TransactionTableNumber> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Transactional/CreateupdateTransactionTableNumber";
        return this.http.post(url, transactiontablenumber, options).map(this.extractData).catch(this.handleErrorObservable);
    }   


    getTransactionTypes(isActive): Observable<TransactionType[]> {
        const url = "/api/Concession/GetTransactionTypes/" + isActive;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getTransactionTableNumbers(isActive): Observable<TransactionTableNumber[]> {
        const url = "/api/Transactional/GetTransactionTableNumbers/" + isActive;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    //getConcessionTypes(isActive): Observable<ConcessionType[]> {
    //    const url = "/api/Concession/ConcessionTypes/" + isActive;
    //    return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    //}   

    private extractData(response: Response) {
        let body = response.json();
        return body;
    }

    private handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }

}


