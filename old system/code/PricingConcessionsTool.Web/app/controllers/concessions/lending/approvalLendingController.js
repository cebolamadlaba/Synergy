

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('approvalLendingController', ['$scope', '$rootScope', '$stateParams', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', approvalLendingController]);

    approvalLendingController.$inject = ['$scope', '$rootScope', '$stateParams', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function approvalLendingController($scope, $rootScope, $stateParams, $window, $state, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.Title = "Lending Approval"

        $scope.init = function ()
        {
            $q.all(
             [
                 referenceService.getReferenceWithParams('GetProductTypes', { concessionType: 'Lending' }),
                 referenceService.getReference('GetConditionTypes'),
                 referenceService.getReference('GetReviewFeeTypes'),
                 concessionService.getConcession($stateParams.ConcessionId),
             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.ProductTypeList);
                 angular.copy(collection[1].data, $scope.ConditionTypeList);               
                 angular.copy(collection[2].data, $scope.ReviewFeeTypeList);
                 angular.copy(collection[3].data, $scope.Concession);

                 $rootScope.setTitle($scope.Concession);
             })
                
        }   

    }
})();
