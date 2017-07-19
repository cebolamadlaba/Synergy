

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('viewTradeController', ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', viewTradeController]);

    viewTradeController.$inject = ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function viewTradeController($scope, $rootScope, $state, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $scope.currentPage = 1;

        $scope.pageSize = 10;

        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        $scope.FinancialInfo = {};

        angular.merge($scope.Customer, sharedDataService.Customer);

        $scope.ConcessionList = []

        $rootScope.Title = "Trade View"


        $scope.Concession =
            {
                Customer: sharedDataService.Customer,
                ConditionList: $scope.ConditionList,
                RequestorANumber: $rootScope.user.ANumber
            }

        $scope.init = function () {
            $q.all(
             [
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId, 'Trade'),
                concessionService.getCustomerConcessions($scope.Customer.Entity.CustomerId, 'Trade', false),

             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.FinancialInfo);
                 angular.copy(collection[1].data, $scope.ConcessionList);

             })
        }

        $scope.addConcession = function()
        {
            sharedDataService.Customer = $scope.Customer;

            $state.go('TradeNew');


        }

        $scope.onRedirect = function (concessionId)
        {  
            $state.go('TradeEdit')
        }

    }
})();
