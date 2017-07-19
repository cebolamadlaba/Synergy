

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('editBolController', ['$scope', '$rootScope', '$stateParams', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService', editBolController]);

    editBolController.$inject = ['$scope', '$rootScope', '$stateParams', '$window', '$location', 'toastr', '$q', '$uibModal', 'concessionService', 'referenceService', 'sharedDataService'];

    function editBolController($scope, $rootScope, $stateParams, $window, $location, toastr, $q, $uibModal, concessionService, referenceService, sharedDataService) {

        $rootScope.user = sharedDataService.getUser();

        $rootScope.Title = "Business Online Edit";

        $scope.Customer = {};       
        
        $scope.Concession = {}

        $scope.FinancialInfo={}

        $scope.AccountList = [];

        $scope.IsReadOnly = true;

        $scope.IsOverdraft = false;

        $scope.ReviewFeeTypeList = [];

        $scope.BusinesOnlineUserList = [];

        $scope.TransactionGroupList = [];
        
        $scope.init = function ()
        {
            $q.all(
             [
                 referenceService.getReference('GetTransactionGroups'),
                 referenceService.getReference('GetBusinesOnlineUsers'),
                 concessionService.getConcession($stateParams.ConcessionId),
             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.TransactionGroupList);
                 angular.copy(collection[1].data, $scope.BusinesOnlineUserList);
                 angular.copy(collection[2].data, $scope.Concession);



                 angular.copy($scope.Concession.Customer, $scope.Customer);
                 $rootScope.ConditionList=$scope.Concession.ConditionList;

                 $scope.onTransactionGroupChange($scope.Concession.TransactionGroup);

                 if ($scope.Concession.Customer.Entity.CustomerId > 0) {
                     $scope.Concession.Customer.IsNewCustomer = false;
                     angular.copy($scope.Concession.Customer.Entity.AccountList, $scope.AccountList);

                     $q.all([
                      concessionService.getFinancialInfo($scope.Concession.Customer.Entity.CustomerId, 'Bol')
                     ])
                  .then(function success(collection) {
                      angular.copy(collection[0].data, $scope.FinancialInfo);
                  })
                 }
             })
        }

        $scope.onTransactionGroupChange = function (transactionGroup)
        {

            $scope.BusinesOnlineTransactionTypeList = []

            var promise = referenceService.getReferenceWithParams('GetBusinesOnlineTransactionTypes', { TransactionGroupId: transactionGroup.TransactionGroupId });

            promise.then(function success(response) {
                angular.copy(response.data, $scope.BusinesOnlineTransactionTypeList);

                $scope.$apply();
            },
                function error(response) {
                    alert(response.data.ExceptionMessage);
                });
        }

      
        $scope.onSaveConcession = function (data)
        {
            data.user = $rootScope.user;

            var promise = concessionService.EditConcession(data);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true)
                    {
                        toastr.success('Saved  successfully!');
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
