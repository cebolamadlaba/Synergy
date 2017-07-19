

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('editInvestmentController', ['$scope', '$rootScope', '$stateParams', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', editInvestmentController]);

    editInvestmentController.$inject = ['$scope', '$rootScope', '$stateParams', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function editInvestmentController($scope, $rootScope, $stateParams, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.Title = "Investment Edit";

        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        angular.merge($scope.Customer, sharedDataService.Customer);
        
        $scope.ProductTypeList = []

        $scope.ProductList = []

        $scope.ConditionTypeList = []

        $scope.ReferenceData.ConditionTypeProductList = []

        $scope.ConditionList = []

        $scope.Concession = {}

        $scope.FinancialInfo={}

        $scope.AccountList = [];
        
        $scope.IsOverdraft = false;

        $scope.ReviewFeeTypeList = [];
        
        $scope.init = function () {
            $q.all(
             [
                 referenceService.getReferenceWithParams('GetProductTypes', { concessionType: 'Investment' }),
                 referenceService.getReference('GetReviewFeeTypes'),
                 concessionService.getConcession($stateParams.ConcessionId),
             ])
             .then(function success(collection) {

                 angular.copy(collection[0].data, $scope.ProductTypeList);
                 angular.copy(collection[1].data, $scope.ReviewFeeTypeList);
                 angular.copy(collection[2].data, $scope.Concession);
             
                 angular.copy($scope.Concession.ConditionList, $scope.ConditionList);
                 angular.copy($scope.Concession.AccountList, $scope.AccountList);
                 angular.copy($scope.Concession.FinancialInfo, $scope.FinancialInfo);
                 angular.copy($scope.Concession.Customer, $scope.Customer);


                 if ($scope.Concession.ProductType.ProductTypeId == 1) {
                     $scope.IsOverdraft = true;
                 }
                 else {
                     $scope.IsOverdraft = false;
                 }
             })
        }


        $scope.onSaveConcession = function (data) {

            data.user = $rootScope.user;


            var promise = concessionService.EditConcession(data);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true) {

                        toastr.success('Saved  successfully!');

                        //$location.path('roster/inbox');

                        $scope.$emit('unload');
                    } else {
                        alert(response.data.Message)
                        $scope.$emit('unload');
                    }

                },
                function error(response) {
                    $scope.$emit('unload');
                    alert(response.data.ExceptionMessage);
                });
        }

       
        $scope.onDeleteConditionOfGrant = function (condition) {

            alert("Not implemented yet!")
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
