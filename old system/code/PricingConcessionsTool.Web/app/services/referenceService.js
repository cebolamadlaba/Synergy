(function () {
    var app = angular.module('myApp');

    app.factory('referenceService', ['$http', 'AppSettings', referenceService]);

    function referenceService($http, AppSettings)
    {
        var service = {};

        service.getReference = function (method) {
            return $http({
                method: 'GET',
                url: AppSettings.apiServiceBaseUri+ 'reference/' + method
            });}

            service.getReferenceWithParams = function (method,params) {
                return $http({
                    method: 'GET',
                    url: AppSettings.apiServiceBaseUri + 'reference/' + method,
                    params: params
                });}        

        return service;
    };
})();