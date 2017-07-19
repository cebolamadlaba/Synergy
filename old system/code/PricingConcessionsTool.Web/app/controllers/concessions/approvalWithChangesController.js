

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('approvalWithChangesController', ['$scope', '$rootScope', '$stateParams', '$state', 'toastr', '$q', 'concessionService', 'referenceService', 'sharedDataService', approvalWithChangesController]);

    approvalWithChangesController.$inject = ['$scope', '$rootScope', '$stateParams', '$state', 'toastr', '$q', 'concessionService', 'referenceService', 'sharedDataService'];

    function approvalWithChangesController($scope, $rootScope, $stateParams, $state, toastr, $q, concessionService, referenceService, sharedDataService) {

        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        $scope.ViewCommentBox = true;

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

        $scope.ExistingConcessionList = []

        $scope.PendingConcessionList = []

        $scope.IsFormReadOnly = true;
       
        $scope.main = function ()
        {
            $q.all(
             [
                 //referenceService.getReferenceWithParams('GetProductTypes', { concessionType: 'Lending' }),
                 referenceService.getReference('GetConditionTypes'),
                 concessionService.getConcession($stateParams.ConcessionId),
                 //referenceService.getReference('GetReviewFeeTypes'),


             ])
             .then(function success(collection) {
                 //angular.copy(collection[0].data, $scope.ProductTypeList);
                 angular.copy(collection[0].data, $scope.ConditionTypeList);
                 angular.copy(collection[1].data, $scope.Concession);
                 //angular.copy(collection[3].data, $scope.ReviewFeeTypeList);
             
                 angular.copy($scope.Concession.Customer, $scope.Customer);

                 angular.copy($scope.Concession.ConditionList, $scope.ConditionList);
                 angular.copy($scope.Concession.AccountList, $scope.AccountList);                
               
                 $state.go('ApprovalWithChanges.' + $scope.Concession.ConcessionTypeCode);
              
             })
                
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

        


        $scope.onAcceptChanges = function (concession) {
            concession.User = $rootScope.user;

            var promise = concessionService.acceptChanges(concession);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true) {

                        $state.go('mypending');

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

        $scope.onDeclineChanges = function (concession) {
            concession.User = $rootScope.user;

            var promise = concessionService.declineChanges(concession);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true) {

                        $state.go('mypending');

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
    }
})();
