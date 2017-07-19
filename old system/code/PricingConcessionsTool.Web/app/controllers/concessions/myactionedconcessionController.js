

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('myactionedconcessionController', ['$scope', '$rootScope', '$window', '$location', 'toastr', 'sharedDataService', 'concessionService', 'userService', myactionedconcessionController]);

    myactionedconcessionController.$inject = ['$scope', '$rootScope', '$window', '$location', 'toastr', 'sharedDataService', 'concessionService', 'userService'];

    function myactionedconcessionController($scope, $rootScope, $window, $location, toastr, sharedDataService, concessionService, userService) {

        $rootScope.Title = "My Actioned Concessions"

        $scope.currentPage = 1;

        $scope.pageSize = 10;

        $scope.ConcessionList = []

        $scope.user = {}

        $scope.ApproverANumber = null;
        
        $scope.init = function ()
        {
           
            $scope.$emit('load');
           
            $rootScope.user = sharedDataService.getUser();

            var promise = concessionService.getConcessions($rootScope.user.ANumber, $rootScope.user.RoleId, false);
            
                promise.then(
                                function success(response) {

                                    angular.copy(response.data, $scope.ConcessionList);

                                    $scope.$emit('unload');
                                },
                                function error(response) {
                                    $scope.$emit('unload');
                                    alert(response.data.ExceptionMessage);
                                });
            
        }



        $scope.onSearch = function () {
            $scope.$emit('load');

            var promiseUser = userService.getUserProfile($scope.ApproverANumber);
            promiseUser.then(
                          function success(response) {
                              var promise = concessionService.getConcessions(response.data.ANumber, response.data.RoleId, false);

                              promise.then(
                                              function success(response) {
                                                  angular.copy(response.data, $scope.ConcessionList);

                                                  $scope.$emit('unload');
                                              },
                                              function error(response) {
                                                  $scope.$emit('unload');
                                                  alert(response.data.ExceptionMessage);
                                              });

                              $scope.$emit('unload');
                          },
                          function error(response) {
                              $scope.$emit('unload');
                              alert(response.data.ExceptionMessage);
                          });
        }

    }
})();
