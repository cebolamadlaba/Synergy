

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('editLendingController', ['$scope', '$rootScope', '$stateParams', '$state', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', editLendingController]);

    editLendingController.$inject = ['$scope', '$rootScope', '$stateParams', '$state', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function editLendingController($scope, $rootScope, $stateParams, $state, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.Title = "Lending Edit"


        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        angular.merge($scope.Customer, sharedDataService.Customer);
        
        $scope.ProductTypeList = []

        $scope.ProductList = []

        $scope.Concession = {}

        $scope.FinancialInfo={}

        $scope.AccountList = [];
        
        $scope.IsOverdraft = false;
       
        $scope.ReviewFeeTypeList = [];

        $scope.IsReadOnly = false;
        
        $scope.init = function ()
        {
            if ($rootScope.user.IsRequestor)
            {
                $scope.IsReadOnly = true;
            }

            $q.all(
             [
                 referenceService.getReferenceWithParams('GetProductTypes', { concessionType: 'Lending' }),           
                 concessionService.getConcession($stateParams.ConcessionId),
                 referenceService.getReference('GetReviewFeeTypes'),              

             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.ProductTypeList);              
                 angular.copy(collection[1].data, $scope.Concession);
                 angular.copy(collection[2].data, $scope.ReviewFeeTypeList);
               
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

        $scope.onConditionType = function (conditionType)
        {
            $scope.ReferenceData.ConditionTypeProductList = []

            var promise = referenceService.getReferenceWithParams('GetConditionProducts', { ConditionTypeId: conditionType.ConditionTypeId });

            promise.then(function success(response) {
                angular.copy(response.data, $scope.ReferenceData.ConditionTypeProductList);

                },
                function error(response) {                   
                    alert(response.data.ExceptionMessage);
                });
        }

        $scope.onSaveConcession = function (data) {

            data.user = $rootScope.user;


            var promise = concessionService.EditConcession(data);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true) {

                        toastr.success('Saved  successfully!');

                        //$location.path('roster/inbox');

                        $state.go('mypending');


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

        $scope.onShowConditionGrant = function (condition)
        {
            $scope.Condition = { ConcessionConditionId: null }

            if (condition != null)
            {
                angular.merge($scope.Condition, condition);

                $scope.onConditionType(condition.ConditionType);
            }
            _showConditionGrant();
        }

       
        $scope.onClear = function () {

            $scope.ConditionList = [];
        

            $scope.Concession =
           {
               Customer: sharedDataService.Customer,
               ConditionList: $scope.ConditionList
           }
        }

        function _showConditionGrant() {

            var modalInstance = $uibModal.open({

                backdrop: 'static',
                animation: false,
                templateUrl: $rootScope.BaseUrl + "app/templates/ConditionGrantDialog.html",
                controller: 'ModalInstanceCtrl',
                scope: $scope,
                windowClass: 'center-modal',

            });

            modalInstance.result.then(function () {

                $scope.ConditionList.push($scope.Condition);
                //Closed dont do anything               
            },
            function () {

                $scope.Condition = {}

                //Closed dont do anything
            });
        }

    }
})();
