

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('viewLendingController', ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', viewLendingController]);

    viewLendingController.$inject = ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function viewLendingController($scope, $rootScope, $state, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {


        $rootScope.Title = "Lending View"

        $scope.currentPage = 1;

        $scope.pageSize = 10;

        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        $scope.FinancialInfo = {};

        angular.merge($scope.Customer, sharedDataService.Customer);

        $scope.ConcessionList = []

        $scope.Concession =
            {
                Customer: sharedDataService.Customer,
                ConditionList: $scope.ConditionList,
                RequestorANumber: $rootScope.user.ANumber
            }

        $scope.init = function () {
            $q.all(
             [
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId, 'Lending'),
                 concessionService.getCustomerConcessions($scope.Customer.Entity.CustomerId, 'Lending', false),
             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.FinancialInfo);
                 angular.copy(collection[1].data, $scope.ConcessionList);
             })
        }

        $scope.addConcession = function () {
            sharedDataService.Customer = $scope.Customer;

            $state.go('lendingNew');
        }

        $scope.onRedirect = function (concessionId) {
            $state.go('lendingEdit')
        }

    }
})();
