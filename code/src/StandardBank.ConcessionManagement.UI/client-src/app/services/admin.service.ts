import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions} from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs';
import { User } from "../models/user";

@Injectable()
export class AdminService {

    constructor(private http: Http) { }
    CreateUser(user: User) {
        return this.http.post('api/admin/users', user).map(result => result.json()).catch(this.handleErrorObservable);
    }
    UpdateUser(user: User, id:number) {
        return this.http.post('api/admin/users/'+id, user).map(result => result.json()).catch(this.handleErrorObservable);
    }
    private handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }
    GetUserLookupData()
    {
        return this.http.get('api/admin/user/lookup').map(r => r.json()).catch(this.handleErrorObservable);
    }
    GetUsers()
    {
        return this.http.get('api/admin/users').map(r => r.json()).catch(this.handleErrorObservable);
    }
    GetUser(id :number) {
        return this.http.get('api/admin/users/' + id + "?random=" + new Date().getTime() ).map(r => r.json()).catch(this.handleErrorObservable);
    }
    DeleteUser(anumber) {
        return this.http.delete('api/admin/user/'+ anumber).map(r => r.json()).catch(this.handleErrorObservable);
    }

}

@Injectable()
export class MockAdminService extends AdminService {
    CreateUser(user: User) {
        return Observable.of(1);
    }

    UpdateUser(user: User, id: number) {
        return Observable.of(true);
    }

    GetUserLookupData() {
        var user = new User();
        return Observable.of(user);
    }

    GetUsers() {
        var userModels = [new User()];
        return Observable.of(userModels);
    }

    GetUser(id: number) {
        var userModel = new User();
        return Observable.of(userModel);
    }

    DeleteUser(anumber) {
        return Observable.of(1);
    }
}
