

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('approvalInvestmentController', ['$scope', '$rootScope', '$stateParams', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', approvalInvestmentController]);

    approvalInvestmentController.$inject = ['$scope', '$rootScope', '$stateParams', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function approvalInvestmentController($scope, $rootScope, $stateParams, $window, $state, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {


        $rootScope.Title = "Investment Approval"
       
        $scope.TransactionTypeList = []

        $scope.AccountList=[]
       
        $scope.init = function ()
        {
            $q.all(
             [
                   referenceService.getReferenceWithParams('GetProductTypes', { concessionType: 'Investment' }),
                 referenceService.getReference('GetConditionTypes'),
                 referenceService.getReference('GetChannelTypes'),
                 concessionService.getConcession($stateParams.ConcessionId),
             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.ProductTypeList);
                 angular.copy(collection[1].data, $scope.ConditionTypeList);
                 angular.copy(collection[2].data, $scope.ChannelTypeList);
                 angular.copy(collection[3].data, $scope.Concession);

                 $rootScope.setTitle($scope.Concession);

                 if ($scope.Concession.Customer.Entity.CustomerId > 0) {
                     $scope.Concession.Customer.IsNewCustomer = false;
                     angular.copy($scope.Concession.AccountList, $scope.AccountList);

                     $q.all([
                      concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId, 'Investment')
                     ])
                  .then(function success(collection) {
                      angular.copy(collection[0].data, $scope.FinancialInfo);
                  })
                 }

             })          
                
        }   

    }
})();
