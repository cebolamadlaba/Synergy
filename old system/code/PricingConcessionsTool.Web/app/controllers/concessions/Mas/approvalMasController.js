

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('approvalMasController', ['$scope', '$rootScope', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', approvalMasController]);

    approvalMasController.$inject = ['$scope', '$rootScope', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function approvalMasController($scope, $rootScope, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService)
    {
        $rootScope.Title = "Merchant Acquiring Approval"

        $scope.TransactionTypeList = [];

        $scope.init = function () {
            $q.all(
             [
                 referenceService.getReferenceWithParams('GetTransactionTypes', { concessionType: 'Mas' }),
                 referenceService.getReference('GetConditionTypes'),
             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.TransactionTypeList);
                 angular.copy(collection[1].data, $scope.ConditionTypeList);

                 $rootScope.setTitle($scope.Concession);
             })            
        }

        $scope.onProductType = function (productType)
        {

            if (productType.ProductTypeId == 1) {
                $scope.IsOverdraft = true;
            }
            else {
                $scope.IsOverdraft = false;
                $scope.Concession.ReviewFee = null;
                $scope.Concession.UnutilizedFacilityFee = null;
            }
        }

        $scope.onConditionType = function (conditionType) {
            $scope.ReferenceData.ConditionTypeProductList = []

            var promise = referenceService.getReferenceWithParams('GetConditionProducts', { ConditionTypeId: conditionType.ConditionTypeId });

            promise.then(function success(response) {
                angular.copy(response.data, $scope.ReferenceData.ConditionTypeProductList);

                $scope.$apply();
            },
                function error(response) {
                    alert(response.data.ExceptionMessage);
                });
        }

        $scope.onSaveConcession = function (data) {
            $scope.Result = {};

            data.User = $rootScope.user;

            var promise = concessionService.MasSave(data);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true) {

                        toastr.success('Saved  successfully!');

                        //$location.path('roster/inbox');

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

        $scope.onShowConditionGrant = function (condition) {
            $scope.ConditionAction = "Add"
            $scope.Condition = { ConcessionConditionId: null }
            _showConditionGrant(null);
        }

        $scope.onEditConditionGrant = function (condition, index) {
            $scope.Condition = {}

            $scope.ConditionAction = "Save"

            angular.copy($scope.ConditionList[index], $scope.Condition)

            $scope.onConditionType(condition.ConditionType);

            _showConditionGrant(index);

            //$scope.ConditionAction.$apply();
        }

    }
})();
