//approvalBolController




(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('approvalBolController', ['$scope', '$rootScope', '$stateParams', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', approvalBolController]);

    approvalBolController.$inject = ['$scope', '$rootScope', '$stateParams', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function approvalBolController($scope, $rootScope, $stateParams, $window, $state, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.Title = "Business Online Approval"
       
        $scope.TransactionGroupList=[];
        $scope.BusinesOnlineUserList = [];
        $scope.TransactionGroupList = [];
       
        $scope.init = function () {
            $q.all(
             [
                 referenceService.getReference('GetTransactionGroups'),
                 //referenceService.getReferenceWithParams('GetProductTypes', { concessionType: 'Investment' }),
                 referenceService.getReference('GetConditionTypes'),
                 referenceService.getReference('GetBusinesOnlineUsers'),
                 //referenceService.getReference('GetReviewFeeTypes'),  
                 concessionService.getConcession($stateParams.ConcessionId),

             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.TransactionGroupList);
                 angular.copy(collection[1].data, $scope.ConditionTypeList);
                 angular.copy(collection[2].data, $scope.BusinesOnlineUserList);
                 angular.copy(collection[3].data, $scope.Concession);

                 $rootScope.setTitle($scope.Concession);

                 $scope.onTransactionGroupChange($scope.Concession.TransactionGroup);

                 if ($scope.Concession.Customer.Entity.CustomerId > 0) {
                     $scope.Concession.Customer.IsNewCustomer = false;
                     angular.copy($scope.Concession.Customer.Entity.AccountList, $scope.AccountList);

                     $q.all([
                      concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId, 'Bol')
                     ])
                  .then(function success(collection) {
                      angular.copy(collection[0].data, $scope.FinancialInfo);
                  })
                 }
             })
        }

        $scope.onTransactionGroupChange = function (transactionGroup) {
            $scope.BusinesOnlineTransactionTypeList = []

            var promise = referenceService.getReferenceWithParams('GetBusinesOnlineTransactionTypes', { TransactionGroupId: transactionGroup.TransactionGroupId });

            promise.then(function success(response) {
                angular.copy(response.data, $scope.BusinesOnlineTransactionTypeList);

                $scope.$apply();
            },
                function error(response) {
                    alert(response.data.ExceptionMessage);
                });
        }


    }
})();
