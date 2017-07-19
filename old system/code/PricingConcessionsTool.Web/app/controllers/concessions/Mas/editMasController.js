

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('editMasController', ['$scope', '$rootScope', '$stateParams', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', editMasController]);

    editMasController.$inject = ['$scope', '$rootScope', '$stateParams', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function editMasController($scope, $rootScope, $stateParams, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.Title = "Merchant Acquiring Edit"


        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        angular.merge($scope.Customer, sharedDataService.Customer);
        
       
        $scope.Concession = {}

        $scope.FinancialInfo={}

        $scope.AccountList = [];
        
        $scope.IsOverdraft = false;

        $scope.TransactionTypeList = [];
        
        $scope.init = function () {
            $q.all(
             [
                 referenceService.getReferenceWithParams('GetTransactionTypes', { concessionType: 'Mas' }),             
                 concessionService.getConcession($stateParams.ConcessionId),

             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.TransactionTypeList);             
                 angular.copy(collection[1].data, $scope.Concession);
                
                 angular.copy($scope.Concession.ConditionList, $scope.ConditionList);
                 angular.copy($scope.Concession.AccountList, $scope.AccountList);
                 angular.copy($scope.Concession.FinancialInfo, $scope.FinancialInfo);
                 angular.copy($scope.Concession.Customer, $scope.Customer);
              
                 if ($scope.Concession.ProductType.ProductTypeId == 1)
                 {
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
