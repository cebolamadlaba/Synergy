

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('loginController', ['$rootScope', '$state', '$q', '$scope', '$window', '$location', 'toastr', 'sharedDataService', 'userService', 'referenceService', loginController]);

    loginController.$inject = ['$rootScope', '$state', '$q', '$scope', '$window', '$location', 'toastr', 'sharedDataService', 'userService', 'referenceService'];

    function loginController($rootScope, $state, $q, $scope, $window, $location, toastr, sharedDataService, userService, referenceService) {
        $rootScope.HideMenu = true;

        $scope.loading = false;

        $scope.IsGettingData = true;
    
        $scope.user = {}

        $rootScope.Title = ""

        $scope.ProvinceList = [];

        var promise =  userService.getAdUser();

        $scope.init = function () {
            $q.all(
             [
             userService.login(),
             referenceService.getReference('GetProvinces')
             ])
             .then(function success(collection)
             {                
                 angular.copy(collection[0].data, $scope.user);
                 angular.copy(collection[1].data, $scope.ProvinceList);

                 $scope.IsGettingData = false;
             })    
        }

        $scope.login = function ()
        {
            $scope.loading = true;

            var promise = userService.login();

            promise.then(
                              function success(response) {

                                  sharedDataService.setUser(response.data);
                                  $rootScope.HideMenu = false;
                                  $scope.loading = false;
                                  $state.go('mypending')
                                  //$location.path("/concessions/mypending")
                              },
                              function error(response) {
                                  $scope.loading = false;
                                  $scope.$emit('unload');
                                  alert(response.data.ExceptionMessage);
                              });

                     
        }
     
    }
})();
