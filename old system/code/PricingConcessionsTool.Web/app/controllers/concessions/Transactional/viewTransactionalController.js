

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('viewTransactionalController', ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', viewTransactionalController]);

    viewTransactionalController.$inject = ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function viewTransactionalController($scope, $rootScope, $state, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $scope.currentPage = 1;

        $scope.pageSize = 10;

        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        $scope.FinancialInfo = {};

        angular.merge($scope.Customer, sharedDataService.Customer);

        $scope.ConcessionList = []

        $rootScope.Title = "Transactional View"

        $scope.Concession =
            {
                Customer: sharedDataService.Customer,
                ConditionList: $scope.ConditionList,
                RequestorANumber: $rootScope.user.ANumber
            }

        $scope.init = function () {
            $q.all(
             [
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId, 'Transactional'),
                concessionService.getCustomerConcessions($scope.Customer.Entity.CustomerId, 'Transactional', false),

             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.FinancialInfo);
                 angular.copy(collection[1].data, $scope.ConcessionList);

             })
        }

       
        $scope.addConcession = function()
        {
            sharedDataService.Customer = $scope.Customer;

            $state.go('TransactionalNew');


        }

        $scope.onRedirect = function (concessionId)
        {  
            $state.go('TransactionalEdit')
        }

    }
})();
