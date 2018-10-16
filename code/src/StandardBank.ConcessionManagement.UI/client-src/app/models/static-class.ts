export module StaticClass {

    var _user = "TBA";
    var _ae_id;

    export function GetUser() {
        return _user;
    }

    export function GetUserID() {
        return _ae_id;
    }
    export function SetUser(user,aeid) {
        _user = user;
        _ae_id = aeid;
    }
}
