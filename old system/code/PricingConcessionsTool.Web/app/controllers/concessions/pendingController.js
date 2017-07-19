

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('pendingController', ['$scope', '$rootScope', '$window', '$location', 'toastr', 'sharedDataService', 'concessionService', 'userService', pendingController]);

    pendingController.$inject = ['$scope', '$rootScope', '$window', '$location', 'toastr', 'sharedDataService', 'concessionService', 'userService'];

    function pendingController($scope, $rootScope, $window, $location, toastr, sharedDataService, concessionService, userService) {

        $rootScope.Title = "Pending concessions"

        

        $scope.currentPage = 1;

        $scope.pageSize = 10;

        $scope.ConcessionList = []

        $rootScope.user = sharedDataService.getUser();

        $scope.Action = 'Approval.';

        $scope.ApproverANumber = null;

        $scope.init = function () {


            $scope.$emit('load');

            $rootScope.user = sharedDataService.getUser();

            if ($rootScope.user.IsRequestor == true) {
                $scope.Action = 'Edit';
            }

            var promise = concessionService.getConcessions($rootScope.user.ANumber, $rootScope.user.RoleId, true);


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
                              var promise = concessionService.getConcessions(response.data.ANumber, response.data.RoleId, true);

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
