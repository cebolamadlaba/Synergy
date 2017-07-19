

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('viewInvestmentController', ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', viewInvestmentController]);

    viewInvestmentController.$inject = ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function viewInvestmentController($scope, $rootScope, $state, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $scope.currentPage = 1;

        $scope.pageSize = 10;

        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        $scope.FinancialInfo = {};

        angular.merge($scope.Customer, sharedDataService.Customer);

        $scope.ConcessionList = []

        $rootScope.Title = "Investment View"

        $scope.Concession =
            {
                Customer: sharedDataService.Customer,
                ConditionList: $scope.ConditionList,
                RequestorANumber: $rootScope.user.ANumber
            }

        $scope.init = function () {
            $q.all(
             [
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId,'Investment'),
                concessionService.getCustomerConcessions($scope.Customer.Entity.CustomerId, 'Investment', false),
             ])
             .then(function success(collection)
             {
                 angular.copy(collection[0].data, $scope.FinancialInfo);
                 angular.copy(collection[1].data, $scope.ConcessionList);
           
             })
        }

        $scope.addConcession = function()
        {
            sharedDataService.Customer = $scope.Customer;

            $state.go('InvestmentNew');


        }

        $scope.onRedirect = function (concessionId)
        {  
            $state.go('InvestmentEdit')
        }

    }
})();
