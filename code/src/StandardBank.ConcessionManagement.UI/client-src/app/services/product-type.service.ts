import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { ProductType } from "../models/product-type";
import { ConcessionType } from "../models/concession-type";

@Injectable()
export class ProductTypeService {

    constructor(private http: Http) {
    }

    getData(concessionType): Observable<ProductType[]> {
        const url = "/api/Concession/ProductTypes/" + concessionType;
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
export class MockProductTypeService extends ProductTypeService {
    model = [new ProductType()];

    getData(concessionType): Observable<ProductType[]> {
        this.model[0].id = 1;
        this.model[0].description = "Product Type Test";
        this.model[0].concessionType = new ConcessionType();
        this.model[0].concessionType.id = 1;
        this.model[0].concessionType.description = concessionType;
        return Observable.of(this.model);
    }
}