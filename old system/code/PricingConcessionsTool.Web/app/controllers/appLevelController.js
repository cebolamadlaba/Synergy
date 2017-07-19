(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('appLevelController', ['$rootScope', '$state', '$q', 'sharedDataService', 'referenceService', 'concessionService','$location', '$window', '$uibModal', appLevelController]);

    appLevelController.$inject = ['$rootScope', '$state', '$q', 'sharedDataService', 'referenceService', 'concessionService', '$location', '$window', '$uibModal'];


    function appLevelController($rootScope, $state, $q, sharedDataService, referenceService, concessionService, $location, $window, $uibModal)
    {
        var timer;

        $rootScope.Title =""

        $rootScope.HideMenu = false;

        $rootScope.ConditionAction = null;

        $rootScope.ConditionTypeList = [];

        $rootScope.ConditionTypeProductList = [];

        $rootScope.ConditionList = [];

        $rootScope.ConfirmationMessage = null;
        $rootScope.OkText = null;
        $rootScope.CancelText = null;
      
        $rootScope.$on('load', function ()
        {
            $rootScope.IsLoading = true;
        })

        $rootScope.$on('unload', function () {

           // $rootScope.IsLoading = false;
            //timer = $timeout(function () {
            //    $rootScope.IsLoading = false;
            //}, 800);
            $rootScope.IsLoading = false;
            //timer = $timeout(function () {
            //    $rootScope.IsLoading = false;
            //}, 0);
        })

        $rootScope.initLookupData = function()
        {
            $q.all(
             [
                 referenceService.getReference('GetConditionTypes')
             ])
             .then(function success(collection) {
                 angular.copy(collection[0].data, $rootScope.ConditionTypeList);
             })

        }
        referenceService.getReference('GetConditionTypes'),


        $rootScope.loadError = function () 
        {
            $rootScope.CurrentError = null;
            //$rootScope.CurrentError = $routeParams.ErrorMessage
        }

        $rootScope.goto = function (url) {
            $location.path(url);
            $rootScope.user = sharedDataService.user;
            $window.location.reload();
        }

        $rootScope.onBack = function () {
            $state.go($rootScope.previousState, $rootScope.fromParams);
        }


        $rootScope.RemoveConcession = function (concession)
        {

            $rootScope.ConfirmationMessage = " Are you sure you want to remove the concession?"
            $rootScope.OkText = "Yes"
            $rootScope.CancelText = "No"

            var modalInstance = $uibModal.open({

                backdrop: 'static',
                scope: $rootScope,
                animation: false,
                templateUrl: $rootScope.BaseUrl + "app/templates/confirmation.html",
                controller: 'ModalInstanceCtrl',
                windowClass: 'center-modal-small',

            });

            modalInstance.result.then(function () 
            {

                $rootScope.IsLoading = true;

                concession.User = $rootScope.user;

                $q.all(
             [
                 concessionService.removeConcession(concession)
             ])
             .then(function success(collection) {
                
                 var response = collection[0];
                 // Do a check here
                 $rootScope.IsLoading = false;
             })
                
                //Closed dont do anything               
            },
            function () {

                $rootScope.IsLoading = false;

                //Closed dont do anything
            });

          
        }

        $rootScope.logout = function () {
            
            var modalInstance = $uibModal.open({

                backdrop: 'static',
                animation: false,
                templateUrl: $rootScope.BaseUrl + "app/templates/logout.html",
                controller: 'ModalInstanceCtrl',
                windowClass: 'center-modal-small',

            });

            modalInstance.result.then(function () {

                $state.go('login')
                    //Closed dont do anything               
            },
            function () {


                //Closed dont do anything
            });

        }

        $rootScope.onShowConditionGrant = function (condition)
        {
            $rootScope.ConditionAction = "Add"
            $rootScope.Condition = { ConcessionConditionId: null }
            _showConditionGrant(null);
        }

        $rootScope.onEditConditionGrant = function (condition, index)
        {
            $rootScope.Condition = {}

            $rootScope.ConditionAction = "Save"

            angular.copy($rootScope.ConditionList[index], $rootScope.Condition)

            $rootScope.onConditionType(condition.ConditionType);

            _showConditionGrant(index);

            //$scope.ConditionAction.$apply();
        }


        $rootScope.onDeleteConditionOfGrant = function (condition, index) {
           
            /*$rootScope.ConfirmationMessage = " Are you sure you want to remove the condition?"
            $rootScope.OkText = "Yes"
            $rootScope.CancelText = "No"

            var modalInstance = $uibModal.open({

                backdrop: 'static',
                scope:$rootScope,
                animation: false,
                templateUrl: $rootScope.BaseUrl + "app/templates/confirmation.html",
                controller: 'ModalInstanceCtrl',
                windowClass: 'center-modal-small',

            });

            modalInstance.result.then(function () {

                $rootScope.ConditionList.splice(index, 1);
                //Closed dont do anything               
            },
            function () {


                //Closed dont do anything
            });*/
            $rootScope.ConditionList.splice(index, 1);
        }


      


        $rootScope.onConditionType = function (conditionType) {

            $rootScope.ConditionTypeProductList = []

            var promise = referenceService.getReferenceWithParams('GetConditionProducts', { ConditionTypeId: conditionType.ConditionTypeId });

            promise.then(function success(response) {
                angular.copy(response.data, $rootScope.ConditionTypeProductList);

            },
                function error(response) {
                    alert(response.data.ExceptionMessage);
                });
        }


        $rootScope.setTitle = function (concession) {
            if (concession.Type == 1) {
                $rootScope.Title = $rootScope.Title + " - New"
            }
            else if (concession.Type == 2) {
                $rootScope.Title = $rootScope.Title + " - Existing"
            } else if (concession.Type == 3) {
                $rootScope.Title = $rootScope.Title + "- Removal"
            }
        }
        
        function _showConditionGrant(index)
        {

            var modalInstance = $uibModal.open(
                {

                    backdrop: 'static',
                    animation: false,
                    templateUrl: $rootScope.BaseUrl + "app/templates/ConditionGrantDialog.html",
                    controller: 'ModalInstanceCtrl',
                    scope: $rootScope,
                    windowClass: 'center-modal',

                });

            modalInstance.result.then(function () {
                if ($rootScope.ConditionAction == "Add")
                {
                    $rootScope.ConditionList.push($rootScope.Condition);
                }
                else
                {
                    $rootScope.ConditionList[index] = $rootScope.Condition;
                }
                //Closed dont do anything               
            },
            function () {

                $rootScope.Condition = {}

                //Closed dont do anything
            });
        }
        
    };
})();



