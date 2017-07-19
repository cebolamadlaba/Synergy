

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('newMasController', ['$scope', '$rootScope', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', newMasController]);

    newMasController.$inject = ['$scope', '$rootScope', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function newMasController($scope, $rootScope, $window, $state, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.user = sharedDataService.getUser();
                
        $scope.Result = null;

        $rootScope.Title = "Merchant Acquiring New"

        $scope.ConditionAction = "Add";

        $scope.Customer=sharedDataService.Customer;

        $scope.ProductTypeList = []
        $scope.ProductList = []
        $scope.AccountList = [];
        $scope.TransactionTypeList = [];
        $scope.ConditionOriginal = {}
        $scope.FinancialInfo = {}

        $scope.Concession =
            {
                Customer: sharedDataService.Customer,
                ConditionList: $scope.ConditionList,
                RequestorANumber: $rootScope.user.ANumber
            }

        $scope.IsOverdraft = false;

        $scope.init = function () {
            $q.all(
             [
                 referenceService.getReferenceWithParams('GetTransactionTypes', { concessionType: 'Mas' }),
             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.TransactionTypeList);
             })

            if ($scope.Customer.Entity.CustomerId > 0)
            {
                $scope.Concession.Customer.IsNewCustomer = false;
                angular.copy($scope.Concession.Customer.Entity.AccountList, $scope.AccountList);

                $q.all([
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId,'Mas'),
                ])
             .then(function success(collection)
             {
                 angular.copy(collection[0].data, $scope.FinancialInfo);
             })
            }
        }

        $scope.onProductType = function (productType) {

            if (productType.ProductTypeId == 1) {
                $scope.IsOverdraft = true;
            }
            else {
                $scope.IsOverdraft = false;
                $scope.Concession.ReviewFee = null;
                $scope.Concession.UnutilizedFacilityFee = null;
            }
        }

        

        $scope.onSaveConcession = function (data) {

            $scope.$emit('load');

            data.Customer = $scope.Customer;

            $scope.Result = {};

            data.User = $rootScope.user;

            var promise = concessionService.MasSave(data);

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
