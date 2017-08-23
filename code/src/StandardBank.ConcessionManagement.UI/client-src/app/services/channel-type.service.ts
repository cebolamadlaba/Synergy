import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { ChannelType } from "../models/channel-type";

@Injectable()
export class ChannelTypeService {

    constructor(private http: Http) {
    }

    getData(): Observable<ChannelType[]> {
        const url = "/api/Concession/ChannelTypes";
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
export class MockChannelTypeService extends ChannelTypeService {
    model = [new ChannelType()];

    getData(): Observable<ChannelType[]> {
        this.model[0].id = 1;
        this.model[0].description = "Test Channel Type";
        return Observable.of(this.model);
    }
}
