

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('newBolController', ['$scope', '$rootScope', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', newBolController]);

    newBolController.$inject = ['$scope', '$rootScope', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function newBolController($scope, $rootScope, $window, $state, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.Title = "Business Online New"

        $rootScope.user = sharedDataService.getUser();
     
        $scope.Result = null;

        $scope.ConditionAction = "Add";

        $scope.Customer = sharedDataService.Customer;
        
        $scope.ProductTypeList = []
        $scope.ProductList = []
        $scope.AccountList = [];
        $scope.TransactionTypeList = [];
        $scope.ChannelTypeList = [];
        $scope.BusinesOnlineUserList = [];
        $scope.TransactionGroupList = [];
        $scope.ConditionOriginal = {}
        $scope.FinancialInfo = {}

        $scope.BusinesOnlineTransactionTypeList=[]

        $scope.Concession =
            {
                Customer: sharedDataService.Customer,
                ConditionList: $scope.ConditionList,
                RequestorANumber: $rootScope.user.ANumber
            }
        
        $scope.init = function ()
        {
            $q.all(
             [
                 referenceService.getReference('GetTransactionGroups'),             
                 referenceService.getReference('GetBusinesOnlineUsers'),
            ])
             .then(function success(collection)
             {
                 angular.copy(collection[0].data, $scope.TransactionGroupList);
                 angular.copy(collection[1].data, $scope.BusinesOnlineUserList);
             })

            if ($scope.Customer.Entity.CustomerId > 0)
            {
                $scope.Concession.Customer.IsNewCustomer = false;
                angular.copy($scope.Concession.Customer.Entity.AccountList, $scope.AccountList);

                $q.all([
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId,'Bol')
                ])
             .then(function success(collection)
             {
                 angular.copy(collection[0].data, $scope.FinancialInfo);
             })
            }
        }

        $scope.onTransactionGroupChange = function (transactionGroup)
        {
            $scope.BusinesOnlineTransactionTypeList = []

            var promise = referenceService.getReferenceWithParams('GetBusinesOnlineTransactionTypes', { TransactionGroupId: transactionGroup.TransactionGroupId });

            promise.then(function success(response) {
                angular.copy(response.data, $scope.BusinesOnlineTransactionTypeList);
           
            },
                function error(response) {
                    alert(response.data.ExceptionMessage);
                });
        }

        $scope.onSaveConcession = function (data)
        {
            $scope.$emit('load');

            $scope.Result = {};

            data.User = $rootScope.user;

            data.Customer = $scope.Customer;

            var promise = concessionService.BolSave(data);

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
    }
})();
