

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('viewBolController', ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', viewBolController]);

    viewBolController.$inject = ['$scope', '$rootScope', '$state', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function viewBolController($scope, $rootScope, $state, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

      $rootScope.Title = "Business Online View"

        $scope.currentPage = 1;

        $scope.pageSize = 10;

        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        $scope.FinancialInfo = {};

        angular.merge($scope.Customer, sharedDataService.Customer);

        $scope.ConcessionList = []

        $scope.init = function () {
            $q.all(
             [
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId, 'Bol'),
                concessionService.getCustomerConcessions($scope.Customer.Entity.CustomerId, 'Bol', false),

             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.FinancialInfo);
                 angular.copy(collection[1].data, $scope.ConcessionList);

             })
        }

        $scope.addConcession = function()
        {
            sharedDataService.Customer = $scope.Customer;

            $state.go('BolNew');
        }

        $scope.onRedirect = function (concessionId)
        {  
            $state.go('BolEdit')
        }

    }
})();
