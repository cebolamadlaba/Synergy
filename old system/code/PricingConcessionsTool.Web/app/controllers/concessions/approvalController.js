

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('approvalController', ['$scope', '$rootScope', '$stateParams', '$state', 'toastr', '$q', 'concessionService', 'referenceService', 'sharedDataService', approvalController]);

    approvalController.$inject = ['$scope', '$rootScope', '$stateParams', '$state', 'toastr', '$q','concessionService', 'referenceService', 'sharedDataService'];

    function approvalController($scope, $rootScope, $stateParams, $state, toastr, $q, concessionService, referenceService, sharedDataService) {

        $scope.currentPage = 1;

        $scope.pageSize = 10;

        $rootScope.user = sharedDataService.getUser();

        $scope.ReferenceData = referenceService;

        $scope.Customer = {};

        $scope.ViewCommentBox = true;

        angular.merge($scope.Customer, sharedDataService.Customer);
        
        $scope.ProductTypeList = []

        $scope.ProductList = []
              
        $scope.FinancialInfo={}

        $scope.AccountList = [];

        $scope.Concession = {};
        
        $scope.ExistingConcessionList = []

        $scope.PendingConcessionList = []
       
        $scope.main = function ()
        {
            $scope.$emit('load');

            $q.all(
             [
                 concessionService.getConcession($stateParams.ConcessionId)
             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $scope.Concession);
                 angular.copy($scope.Concession.Customer, $scope.Customer);
                 angular.copy($scope.Concession.ConditionList, $scope.ConditionList);
                 angular.copy($scope.Concession.AccountList, $scope.AccountList);

                 $q.all(
                      [concessionService.getCustomerConcessions($scope.Customer.Entity.CustomerId, 'NotSet', false),
                         concessionService.getCustomerConcessions($scope.Customer.Entity.CustomerId, 'NotSet', true)])
                     .then(function success(collection) {
                         angular.copy(collection[0].data, $scope.ExistingConcessionList);
                         angular.copy(collection[1].data, $scope.PendingConcessionList);

                         $scope.$emit('unload');

                     })

                 $state.go('Approval.' + $scope.Concession.ConcessionTypeCode);

             })
        }

        $scope.load = function (concessionId) {

            $scope.$emit('load');

            $q.all(
             [
                 concessionService.getConcession(concessionId),
             ])
             .then(function success(collection) {

                                 angular.copy(collection[0].data, $scope.Concession);
              

                 angular.copy($scope.Concession.Customer, $scope.Customer);

                 angular.copy($scope.Concession.ConditionList, $scope.ConditionList);
                 angular.copy($scope.Concession.AccountList, $scope.AccountList);

                 if ($scope.Concession.ProductType.ProductTypeId == 1) {
                     $scope.IsOverdraft = true;
                 }
                 else {
                     $scope.IsOverdraft = false;
                 }

                 $state.go('Approval.' + $scope.Concession.ConcessionTypeCode);

                 $scope.IsLoadingView = false;
             })

        }

       

        //$scope.IsConcessionModified = function () {

        //    var isModified = false;

        //    angular.forEach($scope.concessionForm, function (value, key) {

        //        if (key[0] != '$' && value.$dirty == true)
        //        {
        //            isModified = true;
        //        }
               
        //    });

        //    return isModified;
        //}
        
        $scope.onForward = function (concession)
        {
            concession.User = $rootScope.user;

            var promise = concessionService.forward(concession);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true)
                    {
                        if ($scope.PendingConcessionList.length == 1)
                        {
                            $state.go('mypending');
                        }
                        else 
                        {
                            var id = 0; 


                            angular.forEach($scope.PendingConcessionList, function (value, key)
                            {
                                if (id == 0) {
                                    if (value.ConcessionId != $stateParams.ConcessionId) {

                                        id = value.ConcessionId;
                                    }
                                }
                            });

                            
                            $state.go('Approval', { ConcessionId :id});
                        }

                        toastr.success('Sent to  Pricing Manager!');

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

        $scope.onApprove = function (concession)
        {
            concession.User = $rootScope.user;

            var promise = concessionService.approve(concession);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true) {

                        $state.go('mypending');

                        toastr.success('Approved successfully!');                     

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


        $scope.onApproveWithChanges = function (concession) {

            concession.User = $rootScope.user;

            var promise = concessionService.approveWithChanges(concession);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true) {

                        $state.go('mypending');

                        toastr.success('Approved successfully!');

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

        $scope.onDecline = function (concession) {

            concession.User = $rootScope.user;

            var promise = concessionService.decline(concession);

            promise.then(

                function success(response) {

                    if (response.data.IsSuccessful == true) {

                        $state.go('mypending');


                        toastr.success('Declined  successfully!');

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
