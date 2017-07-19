

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('newTransactionalController', ['$scope', '$rootScope', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', newTransactionalController]);

    newTransactionalController.$inject = ['$scope', '$rootScope', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function newTransactionalController($scope, $rootScope, $window, $state, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        $scope.Result = null;

        $scope.ConditionAction = "Add";

        $scope.Customer = sharedDataService.Customer;

        $scope.ProductTypeList = []
        $scope.ProductList = []

        $scope.AccountList = [];
        $scope.TransactionTypeList = [];
        $scope.ChannelTypeList = [];
        $scope.BaseRateList = [];
        $scope.ConditionOriginal = {}
        $scope.FinancialInfo = {}


        $rootScope.Title = "Transactional New"

        $scope.Concession =
            {
                Customer: sharedDataService.Customer,
                ConditionList: $scope.ConditionList,
                RequestorANumber: $rootScope.user.ANumber
            }

        $scope.init = function () {
            $q.all(
             [
                 referenceService.getReferenceWithParams('GetTransactionTypes', { concessionType: 'Transactional' }),
                 referenceService.getReference('GetChannelTypes'),
             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.TransactionTypeList);
                 angular.copy(collection[1].data, $scope.ChannelTypeList);
             })

            if ($scope.Customer.Entity.CustomerId > 0) {
                $scope.Concession.Customer.IsNewCustomer = false;
                angular.copy($scope.Concession.Customer.Entity.AccountList, $scope.AccountList);

                $q.all([
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId, 'Transactional'),
                ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.FinancialInfo);
             })
            }
        }

        $scope.onChannelTypeChange = function (channelType) {
            $scope.BaseRateList = []

            var promise = referenceService.getReferenceWithParams('GetBaseRates', { ChannelTypeId: channelType.ChannelTypeId });

            promise.then(function success(response) {
                angular.copy(response.data, $scope.BaseRateList);

                $scope.$apply();
            },
                function error(response) {
                    alert(response.data.ExceptionMessage);
                });

            //BaseRateList
        }


        $scope.onSaveConcession = function (data) {
            $scope.$emit('load');

            $scope.Result = {};

            data.User = $rootScope.user;

            data.Customer = $scope.Customer;

            var promise = concessionService.TransactionalSave(data);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true) {

                        toastr.success('Saved  successfully!');

                        $state.go('mypending');

                        $scope.$emit('unload');
                    } else {
                        angular.copy(response.data, $scope.Result);
                        $scope.$emit('unload');
                    }

                },
                function error(response) {
                    $scope.$emit('unload');
                    alert(response.data.ExceptionMessage);
                });
        }

        $scope.onClear = function () {

            $scope.ConditionList = [];


            $scope.Concession =
           {
               Customer: sharedDataService.Customer,
               ConditionList: $scope.ConditionList
           }
        }
    }
})();
