

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('viewCashController', ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', viewCashController]);

    viewCashController.$inject = ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function viewCashController($scope, $rootScope, $state, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.Title = "Cash View"

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
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId, 'Cash'),
                concessionService.getCustomerConcessions($scope.Customer.Entity.CustomerId, 'Cash', false),

             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.FinancialInfo);
                 angular.copy(collection[1].data, $scope.ConcessionList);

             })
        }

        $scope.addConcession = function()
        {
            sharedDataService.Customer = $scope.Customer;

            $state.go('CashNew');


        }

        $scope.onRedirect = function (concessionId)
        {  
            $state.go('CashEdit')
        }

    }
})();
