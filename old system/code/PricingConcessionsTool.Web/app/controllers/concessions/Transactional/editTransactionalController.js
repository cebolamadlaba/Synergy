

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('editTransactionalController', ['$scope', '$rootScope', '$stateParams', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', editTransactionalController]);

    editTransactionalController.$inject = ['$scope', '$rootScope', '$stateParams', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function editTransactionalController($scope, $rootScope, $stateParams, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {


        $rootScope.Title = "Transactional Edit"

        $rootScope.user = sharedDataService.getUser();

        $scope.ChannelTypeList = []

        $scope.TransactionTypeList = []

        $scope.Concession = {}

        $scope.FinancialInfo={}

        $scope.AccountList = [];       


        $scope.init = function () {
            $q.all(
             [
                     referenceService.getReferenceWithParams('GetTransactionTypes', { concessionType: 'Transactional' }),
                  referenceService.getReference('GetChannelTypes'),
                 concessionService.getConcession($stateParams.ConcessionId),
             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.TransactionTypeList);
                 angular.copy(collection[1].data, $scope.ChannelTypeList);
                 angular.copy(collection[2].data, $scope.Concession);

                 angular.copy($scope.Concession.ConditionList, $scope.ConditionList);
                 angular.copy($scope.Concession.AccountList, $scope.AccountList);
                 angular.copy($scope.Concession.FinancialInfo, $scope.FinancialInfo);
                 angular.copy($scope.Concession.Customer, $scope.Customer);


                 $scope.onChannelTypeChange($scope.Concession.ChannelType)

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
       

  

    }
})();
