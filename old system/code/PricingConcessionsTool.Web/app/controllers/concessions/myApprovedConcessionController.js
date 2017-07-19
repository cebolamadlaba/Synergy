

(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('myApprovedConcessionController', ['$scope', '$rootScope', '$window', '$location', 'toastr', '$filter', 'concessionService', 'referenceService', 'sharedDataService', myApprovedConcessionController]);

    myApprovedConcessionController.$inject = ['$scope', '$rootScope', '$window', '$location', 'toastr', '$filter', 'concessionService', 'referenceService', 'sharedDataService'];

    function myApprovedConcessionController($scope, $rootScope, $window, $location, toastr, $filter, concessionService, referenceService,sharedDataService) {

        $rootScope.user = sharedDataService.getUser();

        $scope.currentPage = 1;

        $scope.pageSize = 10;

        $scope.ConcessionList = [];

        $scope.ConcessionIds = [];

        $rootScope.Title = "My Approved Concessions"
        

        $scope.init = function ()
        {
            $scope.$emit('load');           

            var promise = concessionService.getConcessions($rootScope.user.ANumber, $rootScope.user.RoleId, false);

                promise.then(
                                function success(response) {

                                    angular.copy(response.data, $scope.ConcessionList);

                                    $scope.$emit('unload');
                                },
                                function error(response) {
                                    $scope.$emit('unload');
                                    alert(response.data.ExceptionMessage);
                                });
            
        }

        $scope.onGenerateLetters = function () {

            $scope.ConcessionIds = [];

            $scope.$emit('load');

            angular.forEach($scope.ConcessionList, function (value, key) {

                if (value.IsSelected)
                {
                    $scope.ConcessionIds.push(value)

                }

                
            });


            var promise = concessionService.generateLetters($scope.ConcessionIds);

            promise.then(
                            function success(response)
                            {
                                window.open($rootScope.BaseUrl +"api/"+ response.data);
                                $scope.$emit('unload');
                            },
                            function error(response) {
                                $scope.$emit('unload');
                                alert(response.data.ExceptionMessage);
                            });
        }       
        
        $scope.onSelected = function (concession)
        {

            var d = concession;
        }      
      

        //                    $scope.ConditionList.push($scope.Condition);


        //generateLetters

    }
})();
