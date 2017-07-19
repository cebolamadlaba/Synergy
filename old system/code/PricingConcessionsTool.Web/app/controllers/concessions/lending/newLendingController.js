

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('newLendingController', ['$scope', '$rootScope', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', newLendingController]);

    newLendingController.$inject = ['$scope', '$rootScope', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function newLendingController($scope, $rootScope, $window, $state, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.user = sharedDataService.getUser();

        $scope.Customer = {};

        $scope.Result = null;

        $rootScope.Title = "Lending New"

        $scope.ConditionAction = "Add";

        $scope.Customer = sharedDataService.Customer;
        
        $scope.ProductTypeList = []
        $scope.ProductList = []
        $scope.AccountList = [];
        $scope.ReviewFeeTypeList = [];
        $scope.ConditionOriginal = {}
        $scope.FinancialInfo = {}

        $scope.AccountList = [];

        $scope.ConditionPeriodList = []
        $scope.PeriodList = []

        $scope.Concession =
            {
                SelectedEntityList: sharedDataService.Concession.SelectedEntityList,
                ConditionList: $scope.ConditionList,
                RequestorANumber: $rootScope.user.ANumber
            }

        $scope.IsOverdraft = false;
        
        $scope.init = function ()
        {
            $scope.ConditionPeriodList = []
            $scope.PeriodList = []

            $scope.ConditionPeriodList.push({ ConditionPeriodId: 1, Description: "Ongoing" })
            $scope.ConditionPeriodList.push({ ConditionPeriodId: 2, Description: "Standard" })

            $scope.PeriodList.push(3);
            $scope.PeriodList.push(6);
            $scope.PeriodList.push(9);
            $scope.PeriodList.push(12);

            var total = 1
            $scope.LendingProductList = []
            for (var i = 0; i < total; i++)
            {
                $scope.LendingProductList.push({ AccountList: [] })
            };


            $q.all(
             [
                 referenceService.getReferenceWithParams('GetProductTypes', { concessionType: 'Lending' }),
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
                 concessionService.getFinancialInfo($scope.Customer.Entity.CustomerId,'Lending'),
                ])
             .then(function success(collection)
             {
                 angular.copy(collection[0].data, $scope.FinancialInfo);
             })
            }
        }



        $scope.onAddNewRow = function ()
        {
            $scope.LendingProductList.push({ AccountList :[]})
        }

        $scope.onRemoveRow = function (index) {
            $scope.LendingProductList.splice(index, 1);
        }

        $scope.onAddNewConditionRow = function ()
        {
            $rootScope.ConditionList.push({});
        }

        $scope.onRemoveConditionRow = function (index)
        {
            $rootScope.ConditionList.splice(index, 1);
        }

        $scope.onProductType = function (c)
        {

            if (c.ProductType.ProductTypeId == 1)
            {
                $scope.IsOverdraft = true;               
            }
            else
            {
                $scope.IsOverdraft = false;
                $scope.Concession.ReviewFee = null;
                $scope.Concession.UnutilizedFacilityFee = null;
            }
          

            $q.all(
             [
                 concessionService.GetCustomerAccounts("Lending",1, 1)
             ])
             .then(function success(collection) {
                 c.AccountList = collection[0].data;                
             })

         

        }

        $scope.onConditionType = function (condition)
        {

            condition.ConditionTypeProductList =[]

            var promise = referenceService.getReferenceWithParams('GetConditionProducts', { ConditionTypeId: condition.ConditionType.ConditionTypeId });

            promise.then(function success(response)
            {
                angular.copy(response.data, condition.ConditionTypeProductList);

                $scope.$apply();
            },
                function error(response) {                   
                    alert(response.data.ExceptionMessage);
                });
        }

        $scope.onSaveConcession = function (data)
        {
            $scope.Result = {};

              $scope.$emit('load');

              data.User = $rootScope.user;

              data.Customer = $scope.Customer;

                  var input =
                  {
                      Concessions: $scope.LendingProductList,
                      Motivation: data.Motivation,
                      DealNumber: data.DealNumber,
                      CRSMRS: data.CRSMRS,
                      RequestorANumber: data.RequestorANumber,
                      Customers: $scope.Concession.SelectedEntityList,
                      ConditionList: $rootScope.ConditionList
                  }

                  var promise = concessionService.LendingSave(input);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true) {

                        toastr.success(' Sent for approval!');

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
