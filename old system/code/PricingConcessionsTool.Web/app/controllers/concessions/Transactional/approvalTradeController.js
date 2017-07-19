

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('approvalTransactionalController', ['$scope', '$rootScope', '$stateParams', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', approvalTransactionalController]);

    approvalTransactionalController.$inject = ['$scope', '$rootScope', '$stateParams', '$window', '$state', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function approvalTransactionalController($scope, $rootScope, $stateParams, $window, $state, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

               
        $rootScope.Title = "Transactional Approval"

        $scope.currentPage = 1;      

        $scope.ChannelTypeList = []

        $scope.TransactionTypeList = []
       
        $scope.init = function ()
        {
            $q.all(
             [
                 referenceService.getReferenceWithParams('GetTransactionTypes', { concessionType: 'Transactional' }),
                 referenceService.getReference('GetConditionTypes'),
                 referenceService.getReference('GetChannelTypes'),
                 concessionService.getConcession($stateParams.ConcessionId),
             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.TransactionTypeList);
                 angular.copy(collection[1].data, $scope.ConditionTypeList);
                 angular.copy(collection[2].data, $scope.ChannelTypeList);
                 angular.copy(collection[3].data, $scope.Concession);

                 $scope.onChannelTypeChange($scope.Concession.ChannelType)

                 $rootScope.setTitle($scope.Concession);
             })
                
        }

        $scope.onChannelTypeChange = function (channelType) {
            $scope.BaseRateList = []

            var promise = referenceService.getReferenceWithParams('GetBaseRates', { ChannelTypeId: channelType.ChannelTypeId });

            promise.then(function success(response) {
                angular.copy(response.data, $scope.BaseRateList);

                $scope.$apply();
            },
                function error(response) {
                    alert(response.data.ExceptionMessage);
                });

            //BaseRateList
        }

    }
})();
