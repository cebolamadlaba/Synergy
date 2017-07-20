(function () {
    var app = angular.module('myApp');

    app.factory('userService', ['$http', '$rootScope', userService]);

    function userService($http, $rootScope)
    {
        var service = {};
   
        service.getAdUser = function ()
        {
            return $http({
                method: 'GET',
                url: $rootScope.BaseUrl + 'api/api/user/GetUserAdUser'
            })
        }

        service.login = function () {
            return $http({
                method: 'GET',
                url: $rootScope.BaseUrl + 'api/api/user/login'
            })
        }

        service.getUserProfile = function (anumber) {
            return $http({
                method: 'GET',
                url: $rootScope.BaseUrl + 'api/api/user/GetUserProfile',
                params: { anumber: anumber }
            })
        }

        return service;
    };
})();