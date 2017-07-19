

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('viewMasController', ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', viewMasController]);

    viewMasController.$inject = ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function viewMasController($scope, $rootScope, $state, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $scope.currentPage = 1;

        $scope.pageSize = 10;

        $rootScope.user = sharedDataService.getUser();
     
        $scope.Customer = {};

        $scope.FinancialInfo = {};
        $scope.ConcessionList = [];

        angular.merge($scope.Customer, sharedDataService.Customer);

        $rootScope.Title = "Merchant Acquiring View"

        $scope.init = function () {
            $q.all(
             [
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId,'Mas'),
                concessionService.getCustomerConcessions($scope.Customer.Entity.CustomerId, 'Mas', false),

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

            $state.go('MasNew');


        }

        $scope.onRedirect = function (concessionId)
        {  
            $state.go('MasEdit')
        }

    }
})();
