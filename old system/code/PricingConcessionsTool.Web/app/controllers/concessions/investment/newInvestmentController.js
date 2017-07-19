

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('newInvestmentController', ['$scope', '$rootScope', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', newInvestmentController]);

    newInvestmentController.$inject = ['$scope', '$rootScope', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function newInvestmentController($scope, $rootScope, $window, $state, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        $scope.Result = null;

        $scope.ConditionAction = "Add";

        $rootScope.Title = "Investment New"

        $scope.Customer=sharedDataService.Customer;
        
        $scope.ProductTypeList = []
        $scope.ProductList = []
        $scope.AccountList = [];
        $scope.ReviewFeeTypeList = [];
        $scope.ConditionOriginal = {}
        $scope.FinancialInfo={}

        $scope.Concession =
            {
                Customer: sharedDataService.Customer,
                ConditionList: $scope.ConditionList,
                RequestorANumber: $rootScope.user.ANumber
            }

        $scope.IsOverdraft = false;
        
        $scope.init = function ()
        {
            $q.all(
             [
                 referenceService.getReferenceWithParams('GetProductTypes', { concessionType: 'Investment' }),
                 referenceService.getReference('GetReviewFeeTypes'),               

            ])
             .then(function success(collection)
             {
                 angular.copy(collection[0].data, $scope.ProductTypeList);
                 angular.copy(collection[1].data, $scope.ReviewFeeTypeList);               
             })

            if ($scope.Customer.Entity.CustomerId > 0)
            {
                $scope.Concession.Customer.IsNewCustomer = false;
                angular.copy($scope.Concession.Customer.Entity.AccountList, $scope.AccountList);

                $q.all([
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId),
                ])
             .then(function success(collection)
             {
                 angular.copy(collection[0].data, $scope.FinancialInfo);
             })
            }
        }

        $scope.onProductType = function (productType)
        {

            if (productType.ProductTypeId == 1)
            {
                $scope.IsOverdraft = true;               
            }
            else
            {
                $scope.IsOverdraft = false;
                $scope.Concession.ReviewFee = null;
                $scope.Concession.UnutilizedFacilityFee = null;
            }
        }

        $scope.onSaveConcession = function (data)
        {

            $scope.$emit('load');

            $scope.Result = {};

            data.User = $rootScope.user;

            data.Customer = $scope.Customer;

            var promise = concessionService.InvestmentSave(data);

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
