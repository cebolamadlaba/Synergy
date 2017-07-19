//approvalCashController



(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('approvalCashController', ['$scope', '$rootScope', '$stateParams', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', approvalCashController]);

    approvalCashController.$inject = ['$scope', '$rootScope', '$stateParams', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function approvalCashController($scope, $rootScope, $stateParams, $window, $state, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.Title = "Cash Approval"


        $scope.ChannelTypeList=[]
        $scope.TransactionTypeList = []
        $scope.BaseRateList = []

        $scope.init = function () {
            $q.all(
             [
                  referenceService.getReference('GetChannelTypes'),
                 referenceService.getReference('GetConditionTypes'),               
                 concessionService.getConcession($stateParams.ConcessionId),
             ])
             .then(function success(collection)
             {
                 angular.copy(collection[0].data, $scope.ChannelTypeList);
                 angular.copy(collection[1].data, $scope.ConditionTypeList);                
                 angular.copy(collection[2].data, $scope.Concession);

                 $rootScope.setTitle($scope.Concession);

                 $scope.onChannelTypeChange($scope.Concession.ChannelType);

                 if ($scope.Customer.Entity.CustomerId > 0) {
                     $scope.Concession.Customer.IsNewCustomer = false;
                     angular.copy($scope.Concession.Customer.Entity.AccountList, $scope.AccountList);

                     $q.all([
                      concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId, 'Cash'),
                     ])
                  .then(function success(collection) {
                      angular.copy(collection[0].data, $scope.FinancialInfo);
                  })
                 }
             })
        }

        $scope.onChannelTypeChange = function (channelType)
        {
            $scope.BaseRateList = []

            var promise = referenceService.getReferenceWithParams('GetBaseRates', { ChannelTypeId: channelType.ChannelTypeId });

            promise.then(function success(response) {
                angular.copy(response.data, $scope.BaseRateList);
              
            },
                function error(response) {
                    alert(response.data.ExceptionMessage);
                });

            //BaseRateList
        }
    }
})();
